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

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers

// Windows Header Files:
#include <windows.h>

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS      // some CString constructors will be explicit

#include <atlbase.h>
#include <atlstr.h>
#include <atltrace.h>

#ifndef TRACE
#define TRACE ATLTRACE
#endif

#include <ole2.h>

#include <assert.h>

#include <list>
#include <algorithm>

#include "Receiver.h"
#include "DropTarget.h"
#include "DropHandlerInf.h"
#include "DropHandlerImp.h"

extern bool g_bIsWow64Platform;

DWORD GetProcessID(HWND hWnd);


