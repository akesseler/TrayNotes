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

// All dropped data are shipped to registered receivers via 
// sending a message using given message ID. The lParam of 
// the sent message contains a pointer to currently dropped 
// OLE IDataObject object. The wParam is not used.
class DropHandler : public IDropHandler
{
private:

    // Avoid public construction! 
    DropHandler();

    // Avoid copy construction! 
    DropHandler(DropHandler&); 

    // Avoid copy assignment! 
    DropHandler& operator = (const DropHandler&); 

public:

    virtual ~DropHandler();

public: // Singleton access

    static DropHandler* Instance();

public: // Public static methods

    static HWND GetSystemPager();

    static HWND GetTrayToolbar();

public: // IDropHandler implementation

    HRESULT OnDrop(IDataObject* pDataObject);

public: // Public instance methods

    // Only one message supported per receiver!
    bool Attach(HWND hReceiver, UINT nMessage);

    bool Detach(HWND hReceiver);

    void Revoke();

private: 

    bool UpdateReceivers(IDataObject* pDataObject);

    bool AssignReceiver(HWND hReceiver, UINT nMessage);

    void RemoveReceiver(HWND hReceiver);

    void Cleanup();

private: 

    std::list<Receiver> m_lstReceivers;

    DropTarget* m_pDropTarget;
};

