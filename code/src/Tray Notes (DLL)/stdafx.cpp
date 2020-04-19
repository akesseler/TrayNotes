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

DWORD GetProcessID(HWND hWnd)
{
    if (hWnd == NULL)
    {
        return ::GetCurrentProcessId();
    }
    else
    {
        DWORD dwProcessID = -1;
        ::GetWindowThreadProcessId(hWnd, &dwProcessID);
        return dwProcessID;
    }
}
