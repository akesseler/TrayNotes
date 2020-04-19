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

#include "DropHandlerInf.h"

class DropTarget : public IDropTarget  
{
private:

    // Avoid copy construction! 
    DropTarget(DropTarget&); 

    // Avoid copy assignment! 
    DropTarget& operator = (const DropTarget&); 

public:

    DropTarget(IDropHandler* pDropHandler);

    virtual ~DropTarget();

public: // IUnknown	

    HRESULT STDMETHODCALLTYPE QueryInterface(REFIID riid, LPVOID* ppvObject);

    ULONG STDMETHODCALLTYPE AddRef();

    ULONG STDMETHODCALLTYPE Release();

public: // IDropTarget

    HRESULT STDMETHODCALLTYPE DragEnter(IDataObject* pDataObject, DWORD dwKeyState, POINTL ptCursor, DWORD* pdwEffect);

    HRESULT STDMETHODCALLTYPE DragOver(DWORD dwKeyState, POINTL ptCursor, DWORD* pdwEffect);

    HRESULT STDMETHODCALLTYPE DragLeave();

    HRESULT STDMETHODCALLTYPE Drop(IDataObject* pDataObject, DWORD dwKeyState, POINTL ptCursor, DWORD* pdwEffect);

private:

    bool IsDragOverIcon(POINTL ptCursor);

private:

    volatile ULONG m_nRefCount;

    IDropHandler* m_pDropHandler;
};

