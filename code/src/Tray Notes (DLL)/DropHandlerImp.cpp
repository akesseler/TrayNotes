/*
 * MIT License
 *
 * Copyright (c) 2020 plexdata.de
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

#include "stdafx.h"

#include <shellapi.h>

DropHandler::DropHandler()
    : m_pDropTarget( NULL )
{
}

DropHandler::~DropHandler()
{
    this->Cleanup();
}

DropHandler* DropHandler::Instance() 
{ 
    // Creates a singleton instance on first call. 
    static DropHandler instance;

    // Return with the singleton instance. 
    return &instance;  
}

HWND DropHandler::GetSystemPager()
{
    HWND hParent = ::FindWindowEx(::GetDesktopWindow(), NULL, _T("Shell_TrayWnd"), NULL);
    if(hParent != NULL)
    {
        hParent = ::FindWindowEx(hParent, NULL, _T("TrayNotifyWnd"), NULL);
        if(hParent != NULL)
        {
            return ::FindWindowEx(hParent, NULL, _T("SysPager"), NULL);
        }
    }
    return NULL;
}

HWND DropHandler::GetTrayToolbar()
{
    HWND hParent = DropHandler::GetSystemPager();
    if(hParent != NULL)
    {
        return ::FindWindowEx(hParent, NULL, _T("ToolbarWindow32"), NULL);
    }
    else
    {
        return NULL;
    }
}

HRESULT DropHandler::OnDrop(IDataObject* pDataObject)
{
    HRESULT hResult = E_FAIL;

    TRACE("DropHandler::OnDrop(): >>> pDataObject = 0x%8.8X\n", pDataObject);

    if (pDataObject != NULL) 
    {
        pDataObject->AddRef();

        __try
        {
            __try
            {
                hResult = this->UpdateReceivers(pDataObject) ? S_OK : E_FAIL;
            }
            __except( EXCEPTION_EXECUTE_HANDLER )
            {
                DWORD dwCode = ::GetExceptionCode();

                TRACE("DropTarget::OnDrop(): EXCEPTION occurred in function [code=%d, 0x%8.8X]\n", dwCode, dwCode);

                hResult = E_FAIL;
            }
        }
        __finally
        {
            pDataObject->Release();
        }
    }
    else
    {
        hResult = E_INVALIDARG;
    }

    TRACE("DropHandler::OnDrop(): <<< hResult = 0x%8.8X\n", hResult);

    return hResult;
}

bool DropHandler::Attach(HWND hReceiver, UINT nMessage)
{
    bool success = false;

    TRACE("DropHandler::Attach(): >>> hReceiver = 0x%8.8X\n", hReceiver);

    if (this->AssignReceiver(hReceiver, nMessage))
    {
        // Create drop target in case of first attach.
        if (this->m_pDropTarget == NULL)
        {
            this->m_pDropTarget = new DropTarget(this);

            if (this->m_pDropTarget != NULL)
            {
                this->m_pDropTarget->AddRef();

                HRESULT hResult = ::RegisterDragDrop(DropHandler::GetSystemPager(), this->m_pDropTarget);

                TRACE("DropHandler::Attach(): -> RegisterDragDrop() result = 0x%8.8X\n", hResult);

                if (hResult == S_OK)
                {
                    success = true;
                }
                else
                {
                    // Release all remaining instances of the drop target.
                    while (this->m_pDropTarget->Release() > 0);
                    this->m_pDropTarget = NULL;

                    ::SetLastError(hResult);
                }
            }
            else
            {
                ::SetLastError(ERROR_OUTOFMEMORY);
            }
        }
        else
        {
            success = true; // already initialized...
        }
    }

    TRACE("DropHandler::Attach(): <<< success = %s [code=%d, 0x%8.8X]\n", 
        (success ? "TRUE" : "FALSE"), ::GetLastError(), ::GetLastError());

    return success;
}

bool DropHandler::Detach(HWND hReceiver)
{
    bool success = false;

    TRACE("DropHandler::Detach(): >>> hReceiver = 0x%8.8X\n", hReceiver);

    this->RemoveReceiver(hReceiver);

    // Destroy drop target in case of last detach.
    if (this->m_pDropTarget != NULL && this->m_lstReceivers.size() == 0)
    {
        HRESULT hResult = ::RevokeDragDrop(DropHandler::GetSystemPager());

        TRACE("DropHandler::Detach(): -> RevokeDragDrop() hResult = 0x%8.8X\n", hResult);

        if (hResult == S_OK)
        {
            // Release own instance of the drop target.
            this->m_pDropTarget->Release();
            this->m_pDropTarget = NULL;

            success = true;
        }
        else
        {
            // Release all remaining instances of the drop target.
            while (this->m_pDropTarget->Release() > 0);
            this->m_pDropTarget = NULL;

            ::SetLastError(hResult);
        }
    }
    else
    {
        success = true; // already cleaned up...
    }

    TRACE("DropHandler::Detach(): <<< success = %s [code=%d, 0x%8.8X]\n", 
        (success ? "TRUE" : "FALSE"), ::GetLastError(), ::GetLastError());

    return success;
}

void DropHandler::Revoke()
{
    HRESULT hResult = S_OK;

    __try
    {
        TRACE("DropHandler::Revoke(): >>> \n");

        // hResult might be RPC_E_WRONG_THREAD! No idea how to fix it...
        // Consider usage of function CoCreateFreeThreadedMarshaler()...
        hResult = ::RevokeDragDrop(DropHandler::GetSystemPager());
    }
    __except( EXCEPTION_EXECUTE_HANDLER )
    {
        DWORD dwCode = ::GetExceptionCode();

        TRACE("DropHandler::Revoke(): EXCEPTION occurred in function [code=%d, 0x%8.8X]\n", dwCode, dwCode);

        hResult = E_FAIL;
    }

    TRACE("DropHandler::Revoke(): <<< hResult = 0x%8.8X\n", hResult);
}

bool DropHandler::UpdateReceivers(IDataObject* pDataObject)
{
    if (pDataObject != NULL && this->m_lstReceivers.size() > 0)
    {
        for(std::list<Receiver>::iterator item = this->m_lstReceivers.begin(); item != this->m_lstReceivers.end(); item++)
        {
            if(::IsWindow(item->Handle()))
            {
                LRESULT lResult = ::SendMessage(item->Handle(), item->Message(), (WPARAM)NULL, (LPARAM)pDataObject);

                TRACE("DropHandler::UpdateReceivers(): -> SendMessage(hReceiver = 0x%8.8X, nMessage = %d, wParam = NULL, lParam = 0x%8.8X) result = 0x%8.8X\n", 
                    item->Handle(), item->Message(), pDataObject, lResult);
            }
        }

        return true;
    }
    else
    {
        return false;
    }
}

bool DropHandler::AssignReceiver(HWND hReceiver, UINT nMessage)
{
    bool success = false;

    TRACE("DropHandler::AssignReceiver(): >>> hReceiver = 0x%8.8X, nMessage = %d\n", hReceiver, nMessage);

    if (hReceiver != NULL && nMessage != 0)
    {
        // Find out if given handle is already assigned.
        std::list<Receiver>::iterator found = std::find(
            this->m_lstReceivers.begin(), 
            this->m_lstReceivers.end(), 
            hReceiver);

        // If it is not yet assigned then add it to the receiver list.
        if (found == this->m_lstReceivers.end())
        {
            this->m_lstReceivers.push_back(Receiver(hReceiver, nMessage));
        }
        else
        {
            found->Message(nMessage);
        }

        success = true;
    }
    else
    {
        ::SetLastError(ERROR_INVALID_PARAMETER);
    }

    TRACE("DropHandler::AssignReceiver(): <<< success = %s\n", (success ? "true" : "false" ));

    return success;
}

void DropHandler::RemoveReceiver(HWND hReceiver)
{
    if (hReceiver != NULL)
    {
        this->m_lstReceivers.remove(hReceiver);
    }
}

void DropHandler::Cleanup()
{
    this->Revoke();

    this->m_lstReceivers.empty();

    if (this->m_pDropTarget != NULL )
    {
        // Release all remaining instances of the drop target.
        while (this->m_pDropTarget->Release() > 0);
        this->m_pDropTarget = NULL;
    }
}

