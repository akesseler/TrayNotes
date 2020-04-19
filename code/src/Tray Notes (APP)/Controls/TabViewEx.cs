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
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace plexdata.TrayNotes
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
