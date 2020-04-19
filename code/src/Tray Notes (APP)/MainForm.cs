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
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

using IOLEDataObject = System.Runtime.InteropServices.ComTypes.IDataObject;

namespace plexdata.TrayNotes
{
    public partial class MainForm : Form
    {
        #region Private constant declaration section.

        private const int WM_DRAGDROP = (0x0400 + 1); // WM_USER + 1

        #endregion // Private constant declaration section.

        #region Private member variable declaration section.

        private ImageList largeToolbarIcons = null;

        private ImageList smallToolbarIcons = null;

        private ToolStripItemDisplayStyle toolbarStyle = ToolStripItemDisplayStyle.Image;

        #endregion // Private member variable declaration section.

        // TODO: Multi language support...

        public MainForm()
        {
            this.InitializeComponent();

            this.Icon = Properties.Resources.TrayNotes;
            this.Text = AboutBox.Title;

            this.SetupToolbar();

            this.CanShow = false;
            this.IsDialogVisible = false;

            this.tabEditView.IsDirtyChanged += new EventHandler(this.OnEditViewIsDirtyChanged);
            this.tabEditView.SelectedIndexChanged += new EventHandler(this.OnEditViewSelectedIndexChanged);

            this.mainToolbar.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
            (this.mainToolbar.Renderer as ToolStripProfessionalRenderer).RoundedEdges = false;
            this.mainToolbarMenu.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
            this.pageHeaderMenu.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
            this.mainNotifyMenu.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
#if DEBUG
            this.CanShow = true;
#endif
        }

        #region Internal property section.

        internal bool CanShow { get; private set; }

        internal bool IsDialogVisible { get; private set; }

        internal bool UsePasteLabel { get; set; }

        internal bool UsePasteLabelDate { get; set; }

        internal bool DiscardAllDataAtExit { get; set; }

        internal Color EditBoxBackColor { get; set; }

        internal bool UseToolbarButtonText
        {
            get
            {
                return this.toolbarStyle == ToolStripItemDisplayStyle.ImageAndText;
            }
            set
            {
                if (value)
                {
                    this.toolbarStyle = ToolStripItemDisplayStyle.ImageAndText;
                }
                else
                {
                    this.toolbarStyle = ToolStripItemDisplayStyle.Image;
                }

                foreach (ToolStripItem item in this.mainToolbar.Items)
                {
                    item.DisplayStyle = this.toolbarStyle;
                }
            }
        }

        internal bool UseToolbarLargeIcons
        {
            get
            {
                return this.mainToolbar.ImageList == this.largeToolbarIcons;
            }
            set
            {
                if (value)
                {
                    this.mainToolbar.ImageList = this.largeToolbarIcons;
                }
                else
                {
                    this.mainToolbar.ImageList = this.smallToolbarIcons;
                }
            }
        }

        internal ContextMenuStrip PageHeaderMenu
        {
            get
            {
                return this.pageHeaderMenu;
            }
        }

        #endregion // Internal property section.

        #region Overridden base class method section.

        protected override void SetVisibleCore(bool value)
        {
            if (!this.CanShow)
            {
                value = false;

                // Needed if the program has started but 
                // the main window has never been shown.
                if (!this.IsHandleCreated) { this.CreateHandle(); }
            }

            base.SetVisibleCore(value);
        }

        protected override void OnHandleCreated(EventArgs args)
        {
            base.OnHandleCreated(args);

            using (new CursorWrapper(this, Cursors.AppStarting))
            {
                try
                {
                    if (!AttachDropHandler(this.Handle, WM_DRAGDROP))
                    {
                        Debug.WriteLine(String.Format("AttachDropHandler() has failed with error code " + Marshal.GetLastWin32Error()));
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }

                try
                {
                    Settings settings = new Settings();

                    // Use default settings if loading has failed.
                    if (!Settings.Load(out settings)) { settings = new Settings(); }

                    this.Size = settings.Size;
                    this.Location = settings.Location;

                    this.UsePasteLabel = settings.UsePasteLabel;
                    this.UsePasteLabelDate = settings.UsePasteLabelDate;
                    this.DiscardAllDataAtExit = settings.DiscardAllDataAtExit;
                    this.EditBoxBackColor = settings.EditBoxBackColor;
                    this.UseToolbarButtonText = settings.UseToolbarButtonText;
                    this.UseToolbarLargeIcons = settings.UseToolbarLargeIcons;

                    this.PerformNew(); // Ensure existence of at least one page.
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            }
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            if (!this.DiscardAllDataAtExit && this.tabEditView.IsDirty)
            {
                string message = String.Format("There are unsaved changes! Do you want to save them now?");

                DialogResult result = this.UserChoice(message);

                if (result == DialogResult.Yes)
                {
                    this.tbbSaveAll.PerformClick();
                }

                args.Cancel = result == DialogResult.Cancel;
            }

            base.OnClosing(args);
        }

        protected override void OnClosed(EventArgs args)
        {
            using (new CursorWrapper(this, Cursors.AppStarting))
            {
                try
                {
                    Settings settings = new Settings();

                    settings.Size = this.Size;
                    settings.Location = this.Location;

                    settings.UsePasteLabel = this.UsePasteLabel;
                    settings.UsePasteLabelDate = this.UsePasteLabelDate;
                    settings.DiscardAllDataAtExit = this.DiscardAllDataAtExit;
                    settings.EditBoxBackColor = this.EditBoxBackColor;
                    settings.UseToolbarButtonText = this.UseToolbarButtonText;
                    settings.UseToolbarLargeIcons = this.UseToolbarLargeIcons;

                    Settings.Save(settings);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }

                try
                {
                    if (!DetachDropHandler(this.Handle))
                    {
                        Debug.WriteLine(String.Format("DetachDropHandler() has failed with error code " + Marshal.GetLastWin32Error()));
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            }

            base.OnClosed(args);
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            const int SC_MINIMIZE = 0xF020;

            if (message.Msg == WM_SYSCOMMAND)
            {
                switch (message.WParam.ToInt32())
                {
                    case SC_CLOSE:
                    case SC_MINIMIZE:
                        this.PerformHide();
                        message.Result = IntPtr.Zero;
                        return; // Suppress further handling.
                }
            }
            else if (message.Msg == WM_DRAGDROP)
            {
                IntPtr S_OK = (IntPtr)0;
                IntPtr S_FALSE = (IntPtr)1;

                try
                {
                    // May cause some exceptions, especially security exceptions...
                    object oleObject = Marshal.GetTypedObjectForIUnknown(message.LParam, typeof(IOLEDataObject));
                    if (oleObject != null)
                    {
                        this.PerformPaste("Drop Content", this.FindEditBox(), new DataObject(oleObject));
                        message.Result = S_OK;

                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    message.Result = S_FALSE;
                }

                return; // No one else can handle this message.
            }

            base.WndProc(ref message);
        }

        #endregion // Overridden base class method section.

        #region Private event handler section.

        private void OnEditViewIsDirtyChanged(object sender, EventArgs args)
        {
            this.UpdateControlState();
        }

        private void OnEditViewSelectedIndexChanged(object sender, EventArgs args)
        {
            TabPageEx selected = this.tabEditView.SelectedTab as TabPageEx;
            if (selected != null)
            {
                this.UpdateControlState(selected.IsDirty);
            }
            else
            {
                this.UpdateControlState(false);
            }
        }

        private void OnEditViewDragEnter(object sender, DragEventArgs args)
        {
            try
            {
                if (this.tabEditView.TabCount == 0)
                {
                    args.Effect = DragDropEffects.Copy;
                }
                else
                {
                    args.Effect = DragDropEffects.None;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

        }

        private void OnEditViewDragDrop(object sender, DragEventArgs args)
        {
            try
            {
                if (this.tabEditView.TabCount == 0)
                {
                    TabPageEx page = this.PerformNew();
                    this.PerformPaste("Drop Content", page.EditBox, args.Data);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private void OnEditBoxSelectionChanged(object sender, EventArgs args)
        {
            this.UpdateControlState();
        }

        private void OnEditBoxDragDrop(object sender, DragEventArgs args)
        {
            try
            {
                if (sender is RichEditEx)
                {
                    this.PerformPaste("Drop Content", sender as RichEditEx, args.Data);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private void OnTrayNotifyDoubleClick(object sender, MouseEventArgs args)
        {
            if (!this.IsDialogVisible && !this.Visible)
            {
                this.PerformShow();
            }
        }

        private void OnTrayNotifyMenuOpening(object sender, CancelEventArgs args)
        {
            this.mainNotifyMenuAbout.Enabled = !this.IsDialogVisible;
            this.mainNotifyMenuSettings.Enabled = !this.IsDialogVisible;

            this.mainNotifyMenuShow.Visible = !this.IsDialogVisible && !this.Visible;
            this.mainNotifyMenuHide.Visible = !this.IsDialogVisible && this.Visible;
            this.mainNotifyMenuFore.Visible = this.IsDialogVisible;

            this.mainNotifyMenuPaste.DropDownItems.Clear();

            if (this.tabEditView.TabPages.Count > 1)
            {
                TabPage selected = this.tabEditView.SelectedTab;

                foreach (TabPage page in this.tabEditView.TabPages)
                {
                    if (page is TabPageEx)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(page.Name);
                        item.Tag = (page as TabPageEx).EditBox;
                        item.Checked = page == selected;
                        item.Click += new EventHandler(this.OnPerformPaste);
                        this.mainNotifyMenuPaste.DropDownItems.Add(item);
                    }
                }
            }
        }

        private void OnPageHeaderMenuOpening(object sender, CancelEventArgs args)
        {
            TabPageEx selected = this.tabEditView.SelectedTab as TabPageEx;
            if (selected != null)
            {
                bool dirty = selected.IsDirty;
                this.pageHeaderMenuClose.Enabled = this.CanClose(dirty);
                this.pageHeaderMenuSave.Enabled = this.CanSave(dirty);
                this.pageHeaderMenuCopy.Enabled = this.CanCopy(dirty);
                this.pageHeaderMenuClear.Enabled = this.CanClear(dirty);
            }
            else
            {
                args.Cancel = true;
            }
        }

        private void OnMainToolbarMenuOpening(object sender, CancelEventArgs args)
        {
            this.mainToolbarMenuLarge.Checked = this.UseToolbarLargeIcons;
            this.mainToolbarMenuText.Checked = this.UseToolbarButtonText;
        }

        private void OnMainToolbarMenuItemClick(object sender, EventArgs args)
        {
            if (sender == this.mainToolbarMenuLarge)
            {
                this.UseToolbarLargeIcons = this.mainToolbarMenuLarge.Checked;
            }
            else if (sender == this.mainToolbarMenuText)
            {
                this.UseToolbarButtonText = this.mainToolbarMenuText.Checked;
            }
        }

        private void OnQuitButtonClick(object sender, EventArgs args)
        {
            this.tbbQuit.ShowDropDown();
        }

        private void OnPerformShow(object sender, EventArgs args)
        {
            this.PerformShow();
        }

        private void OnPerformHide(object sender, EventArgs args)
        {
            this.PerformHide();
        }

        private void OnPerformFore(object sender, EventArgs args)
        {
            this.PerformFore();
        }

        private void OnPerformExit(object sender, EventArgs args)
        {
            Application.DoEvents();
            this.Close();
        }

        private void OnPerformNew(object sender, EventArgs args)
        {
            this.PerformNew();
        }

        private void OnPerformLoad(object sender, EventArgs args)
        {
            this.PerformLoad();
        }

        private void OnPerformSave(object sender, EventArgs args)
        {
            this.PerformSave(this.tabEditView.SelectedTab as TabPageEx);
        }

        private void OnPerformSaveAll(object sender, EventArgs args)
        {
            if (this.tabEditView.IsDirty)
            {
                foreach (TabPageEx page in this.tabEditView.DirtyPages)
                {
                    this.PerformSave(page);
                }
            }
        }

        private void OnPerformClose(object sender, EventArgs args)
        {
            this.PerformClose(this.tabEditView.SelectedTab as TabPageEx);
        }

        private void OnPerformClear(object sender, EventArgs args)
        {
            this.PerformClear(this.tabEditView.SelectedTab as TabPageEx);
        }

        private void OnPerformCopy(object sender, EventArgs args)
        {
            this.PerformCopy(this.tabEditView.SelectedTab as TabPageEx);
        }

        private void OnPerformPaste(object sender, EventArgs args)
        {
            string label = "Paste Content";

            if (sender is ToolStripButton) // Paste button of the toolbar.
            {
                this.PerformPaste(label, this.tabEditView.ActiveEditBox, Clipboard.GetDataObject());
            }
            else if (sender is ToolStripMenuItem) // Paste menu or one of its subitems.
            {
                // The Paste menu item may also send this message. But it is 
                // much easier to check the number of Paste menu subitem count 
                // instead of checking the assigned tag and other conditions.
                // On the other hand, removing and assigning the Paste menu's 
                // event handler accordingly might be an alternative.

                if (this.mainNotifyMenuPaste.DropDownItems.Count == 0)
                {
                    this.PerformPaste(label, this.tabEditView.ActiveEditBox, Clipboard.GetDataObject());
                }
                else
                {
                    this.PerformPaste(label, ((sender as ToolStripMenuItem).Tag as RichEditEx), Clipboard.GetDataObject());
                }
            }
        }

        private void OnPerformSettings(object sender, EventArgs args)
        {
            this.PerformSettings();
        }

        private void OnPerformAbout(object sender, EventArgs args)
        {
            this.PerformAbout();
        }

        #endregion // Private event handler section.

        #region Private handler method section.

        private void PerformShow()
        {
            this.CanShow = true;

            if (!this.IsDisposed)
            {
                // Show window if it is currently hidden.
                if (!this.Visible) { this.Show(); }

                if (this.IsHandleCreated)
                {
                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        const int SW_RESTORE = 0x09;

                        // Restore previous window state if it is currently minimized.
                        ShowWindow(this.Handle, SW_RESTORE);
                    }

                    // Bring main window to front.
                    SetForegroundWindow(this.Handle);
                }

                // Activate the window.
                this.Activate();
                this.Focus();
            }

        }

        private void PerformHide()
        {
            this.Hide();
        }

        private void PerformFore()
        {
            try
            {
                const int GA_ROOTOWNER = 3;
                const int SWP_NOSIZE = 0x0001;
                const int SWP_NOMOVE = 0x0002;
                const int SWP_SHOWWINDOW = 0x0040;

                if (!this.IsDisposed)
                {
                    SetWindowPos(GetLastActivePopup(GetAncestor(this.Handle, GA_ROOTOWNER)),
                        IntPtr.Zero, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private TabPageEx PerformNew()
        {
            TabPageEx page = this.tabEditView.AddPage();
            if (page != null)
            {
                // Adjust page resp. edit-box settings.
                page.EditBox.BackColor = this.EditBoxBackColor;
                page.EditBox.SelectionChanged += new EventHandler(this.OnEditBoxSelectionChanged);
                page.EditBox.DragDrop += new DragEventHandler(this.OnEditBoxDragDrop);
                page.EditBox.AllowDrop = true;
            }

            this.UpdateControlState();

            return page;
        }

        private void PerformLoad()
        {
            try
            {
                this.IsDialogVisible = true;

                this.openFileDialog.FileName = String.Empty;
                this.openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (DialogResult.OK == this.openFileDialog.ShowDialog(this))
                {
                    TabPageEx page = null;

                    if (this.tabEditView.ActiveEditBox != null && this.tabEditView.ActiveEditBox.IsEmpty)
                    {
                        page = this.tabEditView.SelectedTab as TabPageEx;
                    }
                    else
                    {
                        page = this.PerformNew();
                    }

                    using (new CursorWrapper(this, Cursors.WaitCursor))
                    {
                        page.Load(this.openFileDialog.FileName);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
                this.IsDialogVisible = false;
            }
        }

        private void PerformSave(TabPageEx page)
        {
            if (page != null && page.IsDirty && !this.IsDialogVisible)
            {
                try
                {
                    this.IsDialogVisible = true;

                    string fullname = page.FullName;
                    if (String.IsNullOrEmpty(fullname))
                    {
                        this.saveFileDialog.FileName = page.Name;
                        if (DialogResult.OK != this.saveFileDialog.ShowDialog(this))
                        {
                            return;
                        }
                        else
                        {
                            fullname = this.saveFileDialog.FileName;
                        }
                    }

                    using (new CursorWrapper(this, Cursors.WaitCursor))
                    {
                        page.Save(fullname);
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
                finally
                {
                    this.IsDialogVisible = false;
                }
            }

            this.UpdateControlState();
        }

        private void PerformClose(TabPageEx page)
        {
            if (page != null)
            {
                if (page.IsDirty)
                {
                    string message = String.Format("Document \"{0}\" is not yet saved! Do you want to save it now?", page.Name);

                    DialogResult result = this.UserChoice(message);

                    if (result == DialogResult.Yes)
                    {
                        this.PerformSave(page);
                        this.tabEditView.TabPages.Remove(page);
                    }
                    else if (result == DialogResult.No)
                    {
                        this.tabEditView.TabPages.Remove(page);
                    }
                }
                else
                {
                    this.tabEditView.TabPages.Remove(page);
                }
            }
        }

        private void PerformClear(TabPageEx page)
        {
            if (page != null) { page.EditBox.Clear(); }

            this.UpdateControlState();
        }

        private void PerformCopy(TabPageEx page)
        {
            if (page != null) { page.EditBox.Copy(); }
        }

        private void PerformPaste(string label, RichEditEx editBox, IDataObject dataObject)
        {
            try
            {
                Debug.Assert(!String.IsNullOrEmpty(label));

                if (dataObject == null)
                {
                    Debug.WriteLine("PerformPaste(): Invalid data object!");
                    return;
                }

                if (this.tabEditView.TabPages.Count == 0)
                {
                    this.PerformNew();
                    editBox = this.tabEditView.ActiveEditBox;
                }

                if (editBox == null)
                {
                    Debug.WriteLine("PerformPaste(): Invalid drop target!");
                    return;
                }

                if (editBox != null)
                {
                    // NOTE: At the moment, there is no proper way to change current cursor 
                    //       to e.g. hourglass while data dropping takes places.

                    // Move to current end of text.
                    int offset = editBox.MoveEnd();

                    // Add another new line if necessary.
                    if (offset > 0 && editBox.Text[offset - 1] != '\n')
                    {
                        editBox.AppendText(Environment.NewLine);
                        editBox.MoveEnd();
                    }

                    // Add label if required.
                    if (this.UsePasteLabel)
                    {
                        // Ensure a gap to the leading text, if necessary.
                        if (editBox.TextLength > 0)
                        {
                            editBox.AppendText(Environment.NewLine);
                            editBox.MoveEnd();
                        }

                        // Use date/time if required.
                        if (this.UsePasteLabelDate)
                        {
                            label += String.Format(" ({0})", DateTime.Now.ToString("s"));
                        }

                        // Append label text including format.
                        editBox.SelectionFont = this.GetLabelFont(editBox.Font);
                        editBox.AppendText(label);

                        // Move to new end of text.
                        editBox.MoveEnd();

                        // Add more line feeds.
                        editBox.SelectionFont = editBox.Font;
                        editBox.AppendText(String.Format("{0}{0}", Environment.NewLine));

                        // Move to current end of text.
                        editBox.MoveEnd();
                    }

                    if (dataObject.GetDataPresent(DataFormats.Rtf))
                    {
                        editBox.SelectedRtf = (string)dataObject.GetData(DataFormats.Rtf);
                    }
                    else if (dataObject.GetDataPresent(DataFormats.FileDrop))
                    {
                        string helper = String.Empty;
                        foreach (string current in (string[])dataObject.GetData(DataFormats.FileDrop))
                        {
                            helper += String.Format("{0}{1}", current, Environment.NewLine);
                        }
                        editBox.AppendText(helper);
                    }
                    else if (dataObject.GetDataPresent(DataFormats.UnicodeText))
                    {
                        editBox.AppendText((string)dataObject.GetData(DataFormats.UnicodeText));
                    }
                    else if (dataObject.GetDataPresent(DataFormats.Text))
                    {
                        editBox.AppendText((string)dataObject.GetData(DataFormats.Text));
                    }
                    else if (!editBox.PasteSpecial(dataObject))
                    {
                        Color color = editBox.SelectionColor;
                        editBox.SelectionColor = Color.Red;
                        editBox.AppendText("Could not paste data from the Clipboard!");
                        editBox.SelectionColor = color;
                    }

                    // Add another new line if necessary.
                    if (editBox.TextLength > 0 && editBox.Text[editBox.TextLength - 1] != '\n')
                    {
                        editBox.MoveEnd();
                        editBox.AppendText(Environment.NewLine);
                    }

                    editBox.MoveEnd();

                    editBox.Invalidate(true);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            this.UpdateControlState();
        }

        private void PerformSettings()
        {
            if (!this.IsDialogVisible)
            {
                try
                {
                    this.IsDialogVisible = true;

                    using (new CursorWrapper(this, Cursors.WaitCursor))
                    {
                        SettingsDialog dialog = new SettingsDialog(this);

                        dialog.Icon = Icon.FromHandle(new Bitmap(this.smallToolbarIcons.Images[this.tbbSettings.ImageKey]).GetHicon());

                        if (!this.Visible)
                        {
                            dialog.StartPosition = FormStartPosition.CenterScreen;
                        }

                        if (DialogResult.OK == dialog.ShowDialog(this))
                        {
                            this.ApplyEditBoxBackColor();
                        }
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
                finally
                {
                    this.IsDialogVisible = false;
                }
            }
        }

        private void PerformAbout()
        {
            if (!this.IsDialogVisible)
            {
                try
                {
                    this.IsDialogVisible = true;

                    using (new CursorWrapper(this, Cursors.WaitCursor))
                    {
                        AboutBox dialog = new AboutBox();
                        dialog.Icon = Icon.FromHandle(new Bitmap(this.smallToolbarIcons.Images[this.tbbAbout.ImageKey]).GetHicon());

                        if (!this.Visible)
                        {
                            dialog.StartPosition = FormStartPosition.CenterScreen;
                        }

                        dialog.ShowDialog(this);
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
                finally
                {
                    this.IsDialogVisible = false;
                }
            }
        }

        #endregion // Private handler method section.

        #region Other private methods and management functions

        private void SetupToolbar()
        {
            Debug.Assert(this.components != null);

            this.largeToolbarIcons = new ImageList(this.components);
            this.largeToolbarIcons.ColorDepth = ColorDepth.Depth32Bit;
            this.largeToolbarIcons.ImageSize = new Size(32, 32);
            this.largeToolbarIcons.Images.Add(this.tbbQuit.Name, Properties.Resources.Quit32);
            this.largeToolbarIcons.Images.Add(this.tbbNew.Name, Properties.Resources.New32);
            this.largeToolbarIcons.Images.Add(this.tbbLoad.Name, Properties.Resources.Load32);
            this.largeToolbarIcons.Images.Add(this.tbbSave.Name, Properties.Resources.Save32);
            this.largeToolbarIcons.Images.Add(this.tbbSaveAll.Name, Properties.Resources.SaveAll32);
            this.largeToolbarIcons.Images.Add(this.tbbClear.Name, Properties.Resources.Clear32);
            this.largeToolbarIcons.Images.Add(this.tbbClose.Name, Properties.Resources.Close32);
            this.largeToolbarIcons.Images.Add(this.tbbCopy.Name, Properties.Resources.Copy32);
            this.largeToolbarIcons.Images.Add(this.tbbPaste.Name, Properties.Resources.Paste32);
            this.largeToolbarIcons.Images.Add(this.tbbSettings.Name, Properties.Resources.Settings32);
            this.largeToolbarIcons.Images.Add(this.tbbAbout.Name, Properties.Resources.About32);

            this.smallToolbarIcons = new ImageList(this.components);
            this.smallToolbarIcons.ColorDepth = ColorDepth.Depth32Bit;
            this.smallToolbarIcons.ImageSize = new Size(16, 16);
            this.smallToolbarIcons.Images.Add(this.tbbQuit.Name, Properties.Resources.Quit16);
            this.smallToolbarIcons.Images.Add(this.tbbNew.Name, Properties.Resources.New16);
            this.smallToolbarIcons.Images.Add(this.tbbLoad.Name, Properties.Resources.Load16);
            this.smallToolbarIcons.Images.Add(this.tbbSave.Name, Properties.Resources.Save16);
            this.smallToolbarIcons.Images.Add(this.tbbSaveAll.Name, Properties.Resources.SaveAll16);
            this.smallToolbarIcons.Images.Add(this.tbbClear.Name, Properties.Resources.Clear16);
            this.smallToolbarIcons.Images.Add(this.tbbClose.Name, Properties.Resources.Close16);
            this.smallToolbarIcons.Images.Add(this.tbbCopy.Name, Properties.Resources.Copy16);
            this.smallToolbarIcons.Images.Add(this.tbbPaste.Name, Properties.Resources.Paste16);
            this.smallToolbarIcons.Images.Add(this.tbbSettings.Name, Properties.Resources.Settings16);
            this.smallToolbarIcons.Images.Add(this.tbbAbout.Name, Properties.Resources.About16);

            this.mainToolbar.ImageList = this.smallToolbarIcons;

            this.tbbQuit.ImageKey = this.tbbQuit.Name;
            this.tbbNew.ImageKey = this.tbbNew.Name;
            this.tbbLoad.ImageKey = this.tbbLoad.Name;
            this.tbbSave.ImageKey = this.tbbSave.Name;
            this.tbbSaveAll.ImageKey = this.tbbSaveAll.Name;
            this.tbbClear.ImageKey = this.tbbClear.Name;
            this.tbbClose.ImageKey = this.tbbClose.Name;
            this.tbbCopy.ImageKey = this.tbbCopy.Name;
            this.tbbPaste.ImageKey = this.tbbPaste.Name;
            this.tbbSettings.ImageKey = this.tbbSettings.Name;
            this.tbbAbout.ImageKey = this.tbbAbout.Name;
        }

        private void UpdateControlState()
        {
            this.UpdateControlState(this.tabEditView.IsDirty);
        }

        private void UpdateControlState(bool dirty)
        {
            this.tbbSave.Enabled = this.CanSave(dirty);
            this.tbbSaveAll.Enabled = this.CanSaveAll(dirty);
            this.tbbClear.Enabled = this.CanClear(dirty);
            this.tbbClose.Enabled = this.CanClose(dirty);
            this.tbbCopy.Enabled = this.CanCopy(dirty);
        }

        private bool CanSave(bool dirty)
        {
            return dirty && this.tabEditView.TabPages.Count > 0;
        }

        private bool CanSaveAll(bool dirty)
        {
            return this.tabEditView.TabPages.Count > 0;
        }

        private bool CanClear(bool dirty)
        {
            return dirty && this.tabEditView.TabPages.Count > 0;
        }

        private bool CanClose(bool dirty)
        {
            return this.tabEditView.TabPages.Count > 0;
        }

        private bool CanCopy(bool dirty)
        {
            return this.tabEditView.TabPages.Count > 0 && this.tabEditView.ActiveEditBox.SelectionLength > 0;
        }

        private RichEditEx FindEditBox()
        {
            if (this.tabEditView.TabPages.Count > 1)
            {
                // NOTE: Having icons on each menu item requires support of an owner-draw menu.

                // Using an instance of class ContextMenuStrip is obviously and unfortunately 
                // not possible because function TrackPopupMenu() returns error 1401 (invalid 
                // menu handle value) when trying to pass such a handle to that function.

                const int WM_NULL = 0x0000;
                const int TPM_RIGHTALIGN = 0x0008;
                const int TPM_BOTTOMALIGN = 0x0020;
                const int TPM_RETURNCMD = 0x0100;

                // For sure, this is really a hardcore hack! But 
                // necessary to have a blocking popup menu that 
                // allows users to choose the target edit box.

                Point point = Control.MousePosition;
                TabPage selected = this.tabEditView.SelectedTab;

                ContextMenu menu = new ContextMenu();
                menu.MenuItems.Add("Cancel");
                menu.MenuItems.Add("-");

                foreach (TabPage page in this.tabEditView.TabPages)
                {
                    if (page is TabPageEx)
                    {
                        MenuItem item = new MenuItem(page.Name);
                        item.Tag = (page as TabPageEx).EditBox;
                        item.RadioCheck = true;
                        item.Checked = page == selected;
                        menu.MenuItems.Add(item);
                    }
                }

                try
                {
                    // According to the MSDN help of function TrackPopupMenu() it is necessary 
                    // to bing the affected window to the foreground as first. Secondly, a task 
                    // switch is needed to work correctly and this is done by posting a message 
                    // to the affected window afterwards.

                    SetForegroundWindow(this.Handle);

                    int command = TrackPopupMenu(
                        menu.Handle, TPM_RIGHTALIGN | TPM_BOTTOMALIGN | TPM_RETURNCMD,
                        point.X, point.Y, 0, this.Handle, IntPtr.Zero);

                    PostMessage(this.Handle, WM_NULL, IntPtr.Zero, IntPtr.Zero);

                    // Extract chosen menu item index from returned command ID.
                    for (int index = 2; index < menu.MenuItems.Count; index++)
                    {
                        if (GetMenuItemID(menu.Handle, index) == command)
                        {
                            return (menu.MenuItems[index].Tag as RichEditEx);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
                finally
                {
                    menu.Dispose();
                }

                return null;
            }
            else
            {
                return this.tabEditView.ActiveEditBox;
            }
        }

        private void ApplyEditBoxBackColor()
        {
            try
            {
                // The settings UsePasteLabel, UsePasteLabelDate and DiscardAllDataAtExit 
                // are used on demand. Therefore, no need to explicitly apply them.

                // The settings UseToolbarButtonText and UseToolbarLargeIcons are set already 
                // by using the corresponding property setter. Therefore, no need to explicitly 
                // apply them.

                // Apply edit box related settings.
                foreach (TabPage page in this.tabEditView.TabPages)
                {
                    if (page is TabPageEx)
                    {
                        // Set only the background color. Font handling is done while pasting!
                        (page as TabPageEx).EditBox.BackColor = this.EditBoxBackColor;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private DialogResult UserChoice(string message)
        {
            return this.UserChoice(message, this.Text);
        }

        private DialogResult UserChoice(string message, string caption)
        {
            Debug.Assert(!String.IsNullOrEmpty(message));
            Debug.Assert(!String.IsNullOrEmpty(caption));

            return MessageBox.Show(this, message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        private Font GetLabelFont(Font font)
        {
            FontStyle style = font.Style;

            if (font.FontFamily.IsStyleAvailable(FontStyle.Bold))
            {
                style |= FontStyle.Bold;
            }

            if (font.FontFamily.IsStyleAvailable(FontStyle.Underline))
            {
                style |= FontStyle.Underline;
            }

            return new Font(font, style);
        }

        #endregion // Other private methods and management functions

        #region Win32 API function declarations.

        [DllImport("traynotes.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool AttachDropHandler(IntPtr hReceiver, int nMessage);

        [DllImport("traynotes.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool DetachDropHandler(IntPtr hReceiver);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr PostMessage(IntPtr hWnd, int nMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool ShowWindow(IntPtr hWnd, int command);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hInsert, int x, int y, int cx, int cy, int flags);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr GetAncestor(IntPtr hWnd, int flags);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern int TrackPopupMenu(IntPtr hMenu, int nFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr pIgnored);

        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern int GetMenuItemID(IntPtr hMenu, int nPos);

        #endregion // Win32 API function declarations.
    }
}
