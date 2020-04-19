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

