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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Plexdata.TrayNotes
{
    public class RichEditEx : RichTextBox
    {
        // SEE: ms-help://MS.VSCC.v90/MS.MSDNQTR.v90.en/shellcc/platform/commctls/richedit/richeditcontrols.htm

        #region Public event section.

        public event EventHandler IsDirtyChanged;

        public event EventHandler TextMarginChanged;

        #endregion // Public event section.

        #region Construction...

        public RichEditEx()
            : base()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        #endregion // Construction...

        #region Public property section.

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                if (this.isDirty != value)
                {
                    this.isDirty = value;
                    this.OnIsDirtyChanged(EventArgs.Empty);
                }
            }
        }
        private bool isDirty = false;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEmpty
        {
            get
            {
                return !this.IsDirty && this.TextLength == 0;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Padding TextMargin
        {
            get
            {
                return this.textMargin;
            }
            set
            {
                if (this.textMargin != value)
                {
                    this.textMargin = value;

                    this.DoFixMargin();

                    this.OnTextMarginChanged(EventArgs.Empty);
                }
            }
        }
        private Padding textMargin = new Padding();

        // TODO: Check if there is a better way to determine if the scrollbars have changed.
        public new RichTextBoxScrollBars ScrollBars
        {
            get
            {
                return base.ScrollBars;
            }
            set
            {
                base.ScrollBars = value;
                this.DoFixBorder(); // Changing scrollbars resets the border...
                this.DoFixMargin(); // Changing scrollbars resets the margin...
            }
        }

        #endregion // Public property section.

        #region Protected property section.

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle &= ~(WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);
                cp.Style &= ~WS_BORDER;

                switch (base.BorderStyle)
                {
                    case BorderStyle.Fixed3D:
                        cp.ExStyle |= WS_EX_STATICEDGE;
                        break;
                    case BorderStyle.FixedSingle:
                        cp.Style |= WS_BORDER;
                        break;
                }
                return cp;
            }
        }

        #endregion // Protected property section.

        #region Public method section.

        public bool IsScrollBarVisible(ScrollBars which)
        {
            if (this.IsHandleCreated)
            {
                int style = GetWindowLong(this.Handle, GWL_STYLE);

                switch (which)
                {
                    case System.Windows.Forms.ScrollBars.None:
                        return ((style & WS_VSCROLL) != WS_VSCROLL) && ((style & WS_HSCROLL) != WS_HSCROLL);
                    case System.Windows.Forms.ScrollBars.Both:
                        return ((style & (WS_VSCROLL | WS_HSCROLL)) == (WS_VSCROLL | WS_HSCROLL));
                    case System.Windows.Forms.ScrollBars.Vertical:
                        return ((style & WS_VSCROLL) == WS_VSCROLL);
                    case System.Windows.Forms.ScrollBars.Horizontal:
                        return ((style & WS_HSCROLL) == WS_HSCROLL);
                }
            }
            return false;
        }

        public bool PasteSpecial(IDataObject dataObject)
        {
            bool pasted = false;
            try
            {
                if (dataObject != null)
                {
                    foreach (string format in dataObject.GetFormats())
                    {
                        if (this.CanPaste(DataFormats.GetFormat(format)))
                        {
                            // NOTE: Calling method OnDragDrop() was a nice try but without any success. 
                            //       Either a recursion has occurred or the data were not pasted. Therefore, 
                            //       there is no other way (at moment) to teach the RichTextBox to accept 
                            //       an IDataObject without using the Windows Clipboard!

                            // Save current Clipboard data.
                            var oldDataObject = Clipboard.GetDataObject();

                            // Try pasting new data from Clipboard.
                            Clipboard.SetDataObject(dataObject);
                            base.Paste(DataFormats.GetFormat(format));

                            // Try restoring of previous Clipboard data.
                            if (oldDataObject != null) { Clipboard.SetDataObject(oldDataObject); }

                            pasted = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            return pasted;
        }

        public int MoveTop()
        {
            return this.MoveTo(0);
        }

        public int MoveTo(int position)
        {
            if (position < 0) { position = 0; }
            if (position > this.TextLength) { position = this.TextLength; }

            this.Select(position, 0);
            this.ScrollToCaret();

            return this.SelectionStart;
        }

        public int MoveEnd()
        {
            return this.MoveTo(this.TextLength);
        }

        #endregion // Public method section.

        #region Protected method section.

        protected override void OnHandleCreated(EventArgs args)
        {
            base.OnHandleCreated(args);
            this.DoFixBorder();
            this.DoFixMargin(); // Changing border resets the margin...
        }

        protected override void OnBorderStyleChanged(EventArgs args)
        {
            base.OnBorderStyleChanged(args);
            this.DoFixBorder();
            this.DoFixMargin(); // Changing border resets the margin...
        }

        protected override void OnTextChanged(EventArgs args)
        {
            base.OnTextChanged(args);
            this.IsDirty = true && this.TextLength > 0;

            // Restore to default font.
            if (this.TextLength == 0) { this.SelectionFont = this.Font; }
        }

        protected virtual void OnIsDirtyChanged(EventArgs args)
        {
            if (this.IsDirtyChanged != null)
            {
                this.IsDirtyChanged(this, args);
            }
        }

        protected virtual void OnTextMarginChanged(EventArgs args)
        {
            if (this.TextMarginChanged != null)
            {
                this.TextMarginChanged(this, args);
            }
        }

        #endregion // Protected method section.

        #region Private method section.

        private void DoFixBorder()
        {
            if (this.IsHandleCreated)
            {
                int style = GetWindowLong(this.Handle, GWL_EXSTYLE);

                style &= ~WS_EX_CLIENTEDGE;

                SetWindowLong(this.Handle, GWL_EXSTYLE, style);

                SetWindowPos(this.Handle, IntPtr.Zero, this.Left, this.Top, this.Width, this.Height, SWP_FRAMECHANGED);
            }
            else
            {
                Debug.WriteLine("DoFixBorder(): Handle not yet created...");
            }
        }

        private void DoFixMargin()
        {
            if (this.IsHandleCreated)
            {
                const int EM_SETRECT = 0x00B3;

                IntPtr wParam = IntPtr.Zero;
                IntPtr lParam = IntPtr.Zero;

                Rectangle client = this.ClientRectangle;

                RECT margin = new RECT()
                {
                    left = client.Left + this.textMargin.Left,
                    top = client.Top + this.textMargin.Top,
                    right = client.Right - this.textMargin.Right,
                    bottom = client.Bottom - this.textMargin.Bottom,
                };

                try
                {
                    lParam = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RECT)));

                    Marshal.StructureToPtr(margin, lParam, false);

                    SendMessage(this.Handle, EM_SETRECT, wParam, lParam);
                }
                finally
                {
                    if (lParam != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(lParam);
                    }
                }
            }
            else
            {
                Debug.WriteLine("DoFixMargin(): Handle not yet created...");
            }
        }

        #endregion // Private method section.

        #region Win32 API functions and constants declaration.

        private const int GWL_STYLE = -16;
        private const int WS_VSCROLL = 0x00200000;
        private const int WS_HSCROLL = 0x00100000;
        private const int WS_BORDER = 0x00800000;

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_STATICEDGE = 0x00020000;
        private const int WS_EX_CLIENTEDGE = 0x00000200;
        private const int WS_EX_LEFTSCROLLBAR = 0x00004000;
        private const int WS_EX_RTLREADING = 0x00002000;

        private const int SWP_FRAMECHANGED = 0x0020;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int nMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int nValue);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hInsert, int x, int y, int cx, int cy, int flags);

        #endregion // Win32 API functions and constants declaration.
    }
}
