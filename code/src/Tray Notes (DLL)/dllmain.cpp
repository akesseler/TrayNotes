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

bool g_bIsWow64Platform = false;

void HandleProcessAttach();
void HandleProcessDetach();

// DLL main
BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved)
{
    switch (dwReason)
    {
    case DLL_PROCESS_ATTACH:
        TRACE("DllMain(): DLL_PROCESS_ATTACH => %d\n", GetCurrentProcessId());
        HandleProcessAttach();
        break;
    case DLL_PROCESS_DETACH:
        TRACE("DllMain(): DLL_PROCESS_DETACH\n");
        HandleProcessDetach();
        break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
        break;
    }

    return TRUE;
}

// DLL export function
__declspec(dllexport) BOOL WINAPI AttachDropHandler(HWND hReceiver, UINT nMessage)
{
    return DropHandler::Instance()->Attach(hReceiver, nMessage) ? TRUE : FALSE;
}

// DLL export function
__declspec(dllexport) BOOL WINAPI DetachDropHandler(HWND hReceiver)
{
    return DropHandler::Instance()->Detach(hReceiver) ? TRUE : FALSE;
}

// Internal helper
void HandleProcessAttach()
{
    // Determine current platform type.
    SYSTEM_INFO si = {0};
    ::GetNativeSystemInfo(&si);

    g_bIsWow64Platform =
        si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_IA64 || 
        si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_ALPHA64 || 
        si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_AMD64 || 
        si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_IA32_ON_WIN64;

    TRACE("HandleProcessAttach(): GetNativeSystemInfo() -> Platform = %s\n", g_bIsWow64Platform ? "x64" : "x86");

    // Initialize OLE; may fail because it's already initialized.
    HRESULT hResult = ::OleInitialize(NULL);

    TRACE("HandleProcessAttach(): OleInitialize() -> hResult = 0x%8.8X\n", hResult);
}

// Internal helper
void HandleProcessDetach()
{
    ::OleUninitialize();
}

