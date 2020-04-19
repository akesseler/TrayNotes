/*
 * Copyright (C)  2014  Axel Kesseler
 * 
 * This software is free and you can use it for any purpose. Furthermore, 
 * you are free to copy, to modify and/or to redistribute this software.
 * 
 * In addition, this software is distributed in the hope that it will be 
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 */

#include "stdafx.h"

#include <windowsx.h>
#include <commctrl.h>

// Find other examples on the Internet under:
// http://www.codeproject.com/Articles/19907/Drag-and-Drop-on-a-Tray-Icon
// http://www.codeproject.com/Articles/814/A-generic-IDropTarget-COM-class-for-dropped-text
// http://www.codeproject.com/Articles/840/How-to-Implement-Drag-and-Drop-Between-Your-Progra

// This is just the declaration of the 64 
// bit version of the TBBUTTON structure.
typedef struct tagTBBUTTON64 {
    int              iBitmap;
    int              idCommand;
    unsigned char    fsState;
    unsigned char    fsStyle;
    unsigned char    bReserved[6];
    unsigned __int64 dwData;
    __int64          iString;
} TBBUTTON64;

// This is just the declaration of the 32 
// bit version of the TBBUTTON structure.
typedef struct tagTBBUTTON32 {
    int           iBitmap;
    int           idCommand;
    unsigned char fsState;
    unsigned char fsStyle;
    unsigned char bReserved[2];
    unsigned long dwData;
    int           iString;
} TBBUTTON32;

DropTarget::DropTarget(IDropHandler* pDropHandler)
    : m_nRefCount    ( 0            )
    , m_pDropHandler ( pDropHandler )
{
    assert(this->m_pDropHandler != NULL);
}

DropTarget::~DropTarget()
{
}

HRESULT DropTarget::QueryInterface(REFIID riid, LPVOID* ppvObject)
{
    // Validate result parameter first.
    if (ppvObject != NULL)
    {
        // Always initialize result parameter to NULL.
        *ppvObject = NULL;

        if (riid == IID_IUnknown || riid == IID_IDropTarget)
        {
            // Increment the reference count and return the pointer.
            this->AddRef();

            *ppvObject = (LPVOID)this;

            return NOERROR;
        }

        return E_NOINTERFACE;
    }
    else
    { 
        return E_INVALIDARG; 
    }
}

ULONG DropTarget::AddRef()
{
    // Increment the object's internal counter.
    ::InterlockedIncrement((LONG*)&this->m_nRefCount);

    return this->m_nRefCount;
}

ULONG DropTarget::Release()
{
    // Decrement the object's internal counter.
    ULONG nRefCount = ::InterlockedDecrement((LONG*)&this->m_nRefCount);

    if (this->m_nRefCount == 0)
    {
        delete this;
    }

    return nRefCount;
}

HRESULT DropTarget::DragEnter(IDataObject* pDataObject, DWORD dwKeyState, POINTL ptCursor, DWORD* pdwEffect)
{
    return this->DragOver(dwKeyState, ptCursor, pdwEffect);
}

HRESULT DropTarget::DragOver(DWORD dwKeyState, POINTL ptCursor, DWORD* pdwEffect)
{
    if (pdwEffect == NULL) { return E_INVALIDARG; }

    if (this->IsDragOverIcon(ptCursor))
    {
        *pdwEffect = DROPEFFECT_COPY;
    }
    else
    {
        *pdwEffect = DROPEFFECT_NONE;
    }

    return S_OK;
}

HRESULT DropTarget::DragLeave()
{
    return S_OK;
}

HRESULT DropTarget::Drop(IDataObject* pDataObject, DWORD dwKeyState, POINTL ptCursor, DWORD* pdwEffect)
{
    if (this->m_pDropHandler != NULL)
    {
        return this->m_pDropHandler->OnDrop(pDataObject);
    }
    else
    {
        return E_FAIL;
    }
}

bool DropTarget::IsDragOverIcon(POINTL ptCursor)
{
    __try
    {
        bool result = false;
        HANDLE hTrayProcess = NULL;
        LPVOID pTrayShared = NULL;

        __try
        {
            // The tray icon window runs inside the explorer's context and in a 64-bit 
            // environment the surrounding process is a 64-bit process! Unfortunately, 
            // function IsWow64Process(hTrayProcess,...) cannot be used because the handle 
            // returned by OpenProcess() seems to be a 32-bit handle.
            bool bWow64 = g_bIsWow64Platform;

            // Get necessary system tray information.
            HWND hTrayWindow = DropHandler::GetTrayToolbar();
            DWORD dwTrayProcessID = GetProcessID(hTrayWindow);
            hTrayProcess = ::OpenProcess(PROCESS_ALL_ACCESS, 0, dwTrayProcessID);

            // Get current process ID and save it for later use.
            DWORD dwCurrentProcessID = GetProcessID(NULL);

            // We want to get data from another process. But it is not possible to simply 
            // send messages like TB_GETBUTTON to get data on a locally allocated buffer. 
            // Therefore, it's necessary to allocate memory inside the system tray process. 
            // The size of this buffer is the size of the TBBUTTON structure. But the same 
            // buffer is also used to get some smaller data structures, such as RECT, and 
            // should be therefore big enough.

            TBBUTTON64 tbButton64 = {0};
            TBBUTTON32 tbButton32 = {0};

            LPVOID pTBButton = NULL;
            SIZE_T nTBButton = 0;

            if (bWow64)
            {
                pTBButton = &tbButton64;
                nTBButton = sizeof(TBBUTTON64);
            }
            else
            {
                pTBButton = &tbButton32;
                nTBButton = sizeof(TBBUTTON32);
            }

            // Allocate a shared buffer of size of 128 bytes.
            pTrayShared = ::VirtualAllocEx(hTrayProcess, NULL, 128, MEM_COMMIT, PAGE_READWRITE);
            if (pTrayShared != NULL)
            {
                // Now get total number of available buttons and iterate over each of them.
                int nCount = (int)::SendMessage(hTrayWindow, TB_BUTTONCOUNT, NULL, NULL);
                for (int iButton = 0; iButton < nCount; iButton++)
                {
                    BOOL success = FALSE;
                    SIZE_T dwBytesRead = -1;

                    // Get current TBBUTTON data.
                    success = (BOOL)::SendMessage(hTrayWindow, TB_GETBUTTON, iButton, (LPARAM)pTrayShared);
                    if (!success) { continue; }

                    success = ::ReadProcessMemory(hTrayProcess, pTrayShared, pTBButton, nTBButton, &dwBytesRead);
                    if (!success) { continue; }

                    // I did many tries to figure out what's behind dwData, but without success. Someone 
                    // states it's a structure of type "TRAYDAT" and someone else states it's a structure 
                    // of type "NOTIFYICONDATA". But nothing of this is correct. The only that is really 
                    // save is that the first four bytes contain the handle to the notify window. And this 
                    // is good enough...

                    LPVOID pData = NULL;
                    if (bWow64)
                    {
                        if ((tbButton64.fsState & TBSTATE_HIDDEN) == TBSTATE_HIDDEN)
                        {
                            // Skip current button if it is hidden, because dropping 
                            // on a hidden button is not really possible.
                            continue;
                        }
                        else
                        {
                            // Otherwise prepare access to the inner data.
                            pData = (LPVOID)tbButton64.dwData;
                        }
                    }
                    else
                    {
                        if ((tbButton32.fsState & TBSTATE_HIDDEN) == TBSTATE_HIDDEN)
                        {
                            // Skip current button if it is hidden, because dropping 
                            // on a hidden button is not really possible.
                            continue;
                        }
                        else
                        {
                            // Otherwise prepare access to the inner data.
                            pData = (LPVOID)tbButton32.dwData;
                        }
                    }

                    // Get icon owner window handle (the notify window). 
                    // It is the first entry of returned button data!
                    HWND hNotifyWindow = NULL;
                    success = ::ReadProcessMemory(hTrayProcess, pData, (LPVOID)&hNotifyWindow, sizeof(HWND), &dwBytesRead);
                    if (!success || hNotifyWindow == NULL) { continue; }

                    // Check if current process ID is equal to current window's process ID.
                    // If so then we found the belonging window.
                    DWORD dwNotifyProcessID = GetProcessID(hNotifyWindow);
                    if (dwNotifyProcessID != dwCurrentProcessID) { continue; }

                    // Get current button's rectangle.
                    success = (BOOL)::SendMessage(hTrayWindow, TB_GETITEMRECT, iButton, (LPARAM)pTrayShared);
                    if (!success) { continue; }

                    RECT rect = {0};
                    success = ::ReadProcessMemory(hTrayProcess, pTrayShared, &rect, sizeof(RECT), &dwBytesRead);
                    if (!success) { continue; }

                    ::MapWindowRect(hTrayWindow, NULL, &rect);
                    if (ptCursor.x >= rect.left && ptCursor.x <= rect.right &&
                        ptCursor.y >= rect.top && ptCursor.y <= rect.bottom)
                    {
                        result = true;
                        break;
                    }
                }
            }
        }
        __finally
        {
            if (hTrayProcess != NULL)
            {
                if (pTrayShared != NULL)
                {
                    ::VirtualFreeEx(hTrayProcess, pTrayShared, NULL, MEM_RELEASE);
                }

                ::CloseHandle(hTrayProcess);
            }
        }

        return result;
    }
    __except( EXCEPTION_EXECUTE_HANDLER )
    {
        DWORD dwCode = ::GetExceptionCode();
        TRACE("DropTarget::IsDragOverIcon(): EXCEPTION occurred in function [code=%d, 0x%8.8X]\n", dwCode, dwCode);
        return false;
    }
}
