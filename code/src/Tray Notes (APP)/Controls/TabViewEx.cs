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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Plexdata.TrayNotes
{
    internal class TabViewEx : TabControl
    {
        public event EventHandler IsDirtyChanged;

        private int pageCount = 1;

        public TabViewEx()
            : base()
        {
            this.PageContextMenu = null;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDirty
        {
            get
            {
                foreach (TabPage page in this.TabPages)
                {
                    if (page is TabPageEx)
                    {
                        if ((page as TabPageEx).IsDirty)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<TabPageEx> DirtyPages
        {
            get
            {
                foreach (TabPage page in this.TabPages)
                {
                    if (page is TabPageEx)
                    {
                        if ((page as TabPageEx).EditBox.IsDirty)
                        {
                            yield return (page as TabPageEx);
                        }
                    }
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RichEditEx ActiveEditBox
        {
            get
            {
                if (this.SelectedTab is TabPageEx)
                {
                    return (this.SelectedTab as TabPageEx).EditBox;
                }
                else
                {
                    return null;
                }
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ContextMenuStrip PageContextMenu { get; set; }

        public TabPageEx AddPage()
        {
            return this.AddPage(String.Format("Target {0}", this.pageCount++));
        }

        public TabPageEx AddPage(string name)
        {
            TabPageEx page = new TabPageEx(name);

            this.Controls.Add(page);

            page.EditBox.IsDirtyChanged += new EventHandler(this.OnEditBoxIsDirtyChanged);

            page.Activate();

            this.SelectedTab = page;

            return page;
        }

        protected override void OnMouseUp(MouseEventArgs args)
        {
            base.OnMouseUp(args);
            if (this.PageContextMenu != null && args.Button == MouseButtons.Right)
            {
                for (int index = 0; index < this.TabCount; index++)
                {
                    if (this.GetTabRect(index).Contains(args.Location))
                    {
                        this.SelectedIndex = index;
                        this.PageContextMenu.Show(this.PointToScreen(args.Location));
                        return;
                    }
                }
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs args)
        {
            base.OnSelectedIndexChanged(args);
            if (this.TabCount == 0)
            {
                this.pageCount = 1;
            }
        }

        private void OnEditBoxIsDirtyChanged(object sender, EventArgs args)
        {
            if (this.IsDirtyChanged != null)
            {
                this.IsDirtyChanged(sender, args);
            }
        }
    }
}
