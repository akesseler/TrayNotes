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

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace plexdata.TrayNotes
{
    internal class CursorWrapper : IDisposable
    {
        public CursorWrapper(Control parent, Cursor cursor)
            : base()
        {
            try
            {
                this.Parent = null;
                this.Cursor = null;

                if (parent != null)
                {
                    this.Cursor = parent.Cursor;
                    if (parent.Cursor != cursor) { parent.Cursor = cursor; }
                    this.Parent = parent;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        public Control Parent { get; private set; }

        public Cursor Cursor { get; private set; }

        public void Dispose()
        {
            try
            {
                if (this.Parent != null && this.Parent.Cursor != this.Cursor)
                {
                    this.Parent.Cursor = this.Cursor != null ? this.Cursor : Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
    }
}
