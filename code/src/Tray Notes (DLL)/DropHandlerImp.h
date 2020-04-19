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

