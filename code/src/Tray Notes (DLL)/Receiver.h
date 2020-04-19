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

class Receiver
{
public:

    Receiver(HWND handle)
        : m_handle ( handle )
        , m_message( 0      )
    {
        assert(this->m_handle != NULL);
    }

    Receiver(HWND handle, UINT message)
        : m_handle ( handle  )
        , m_message( message )
    {
        assert(this->m_handle != NULL);
    }

    Receiver(const Receiver& other)
        : m_handle ( other.m_handle  )
        , m_message( other.m_message )
    {
        assert(this->m_handle != NULL);
    }

    virtual ~Receiver(void)
    {
    }

public:

    Receiver& operator = (const Receiver& other)
    {
        this->m_handle = other.m_handle;
        this->m_message = other.m_message;

        return *this;
    }

    bool operator == (const Receiver& other) const
    {
        return *this == other.m_handle;
    }

    bool operator == (const HWND handle) const
    {
        return this->m_handle == handle;
    }

public:

    HWND Handle() { return this->m_handle; }

    UINT Message() { return this->m_message; }

public:

    void Message(UINT message) { this->m_message = message; }

private:

    HWND m_handle;

    volatile UINT m_message;
};

