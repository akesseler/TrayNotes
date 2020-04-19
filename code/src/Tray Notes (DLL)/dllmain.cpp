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

