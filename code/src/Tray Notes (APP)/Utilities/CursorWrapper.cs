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

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Plexdata.TrayNotes
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
