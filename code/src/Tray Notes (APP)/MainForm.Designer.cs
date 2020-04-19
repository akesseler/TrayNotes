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

namespace Plexdata.TrayNotes
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainNotifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainNotifyMenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mainNotifyMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainNotifyMenuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.mainNotifyMenuHide = new System.Windows.Forms.ToolStripMenuItem();
            this.mainNotifyMenuFore = new System.Windows.Forms.ToolStripMenuItem();
            this.mainNotifyMenuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mainNotifyMenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mainNotifyMenuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mainNotifyMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolbar = new System.Windows.Forms.ToolStrip();
            this.mainToolbarMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainToolbarMenuLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolbarMenuText = new System.Windows.Forms.ToolStripMenuItem();
            this.tbbQuit = new System.Windows.Forms.ToolStripSplitButton();
            this.tbbQuitHide = new System.Windows.Forms.ToolStripMenuItem();
            this.tbbQuitExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tbsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbNew = new System.Windows.Forms.ToolStripButton();
            this.tbbLoad = new System.Windows.Forms.ToolStripButton();
            this.tbbSave = new System.Windows.Forms.ToolStripButton();
            this.tbbSaveAll = new System.Windows.Forms.ToolStripButton();
            this.tbbClear = new System.Windows.Forms.ToolStripButton();
            this.tbbClose = new System.Windows.Forms.ToolStripButton();
            this.tbsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbCopy = new System.Windows.Forms.ToolStripButton();
            this.tbbPaste = new System.Windows.Forms.ToolStripButton();
            this.tbsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbSettings = new System.Windows.Forms.ToolStripButton();
            this.tbbAbout = new System.Windows.Forms.ToolStripButton();
            this.pageHeaderMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pageHeaderMenuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.pageHeaderMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.pageHeaderMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.pageHeaderMenuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.pageHeaderMenuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusBar = new System.Windows.Forms.StatusStrip();
            this.mainToolbarContainer = new System.Windows.Forms.ToolStripContainer();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabEditView = new Plexdata.TrayNotes.TabViewEx();
            this.pageHeaderMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mainNotifyMenu.SuspendLayout();
            this.mainToolbar.SuspendLayout();
            this.mainToolbarMenu.SuspendLayout();
            this.pageHeaderMenu.SuspendLayout();
            this.mainToolbarContainer.ContentPanel.SuspendLayout();
            this.mainToolbarContainer.TopToolStripPanel.SuspendLayout();
            this.mainToolbarContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainNotifyIcon
            // 
            this.mainNotifyIcon.ContextMenuStrip = this.mainNotifyMenu;
            this.mainNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("mainNotifyIcon.Icon")));
            this.mainNotifyIcon.Text = "TrayNotes";
            this.mainNotifyIcon.Visible = true;
            this.mainNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnTrayNotifyDoubleClick);
            // 
            // mainNotifyMenu
            // 
            this.mainNotifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainNotifyMenuAbout,
            this.mainNotifyMenuSep1,
            this.mainNotifyMenuShow,
            this.mainNotifyMenuHide,
            this.mainNotifyMenuFore,
            this.mainNotifyMenuPaste,
            this.mainNotifyMenuSettings,
            this.mainNotifyMenuSep2,
            this.mainNotifyMenuExit});
            this.mainNotifyMenu.Name = "mainNotifyMenu";
            this.mainNotifyMenu.Size = new System.Drawing.Size(138, 170);
            this.mainNotifyMenu.Opening += new System.ComponentModel.CancelEventHandler(this.OnTrayNotifyMenuOpening);
            // 
            // mainNotifyMenuAbout
            // 
            this.mainNotifyMenuAbout.Image = global::Plexdata.TrayNotes.Properties.Resources.About16;
            this.mainNotifyMenuAbout.Name = "mainNotifyMenuAbout";
            this.mainNotifyMenuAbout.Size = new System.Drawing.Size(137, 22);
            this.mainNotifyMenuAbout.Text = "About...";
            this.mainNotifyMenuAbout.Click += new System.EventHandler(this.OnPerformAbout);
            // 
            // mainNotifyMenuSep1
            // 
            this.mainNotifyMenuSep1.Name = "mainNotifyMenuSep1";
            this.mainNotifyMenuSep1.Size = new System.Drawing.Size(134, 6);
            // 
            // mainNotifyMenuShow
            // 
            this.mainNotifyMenuShow.Image = ((System.Drawing.Image)(resources.GetObject("mainNotifyMenuShow.Image")));
            this.mainNotifyMenuShow.Name = "mainNotifyMenuShow";
            this.mainNotifyMenuShow.Size = new System.Drawing.Size(137, 22);
            this.mainNotifyMenuShow.Text = "Show...";
            this.mainNotifyMenuShow.Click += new System.EventHandler(this.OnPerformShow);
            // 
            // mainNotifyMenuHide
            // 
            this.mainNotifyMenuHide.Image = ((System.Drawing.Image)(resources.GetObject("mainNotifyMenuHide.Image")));
            this.mainNotifyMenuHide.Name = "mainNotifyMenuHide";
            this.mainNotifyMenuHide.Size = new System.Drawing.Size(137, 22);
            this.mainNotifyMenuHide.Text = "Hide";
            this.mainNotifyMenuHide.Visible = false;
            this.mainNotifyMenuHide.Click += new System.EventHandler(this.OnPerformHide);
            // 
            // mainNotifyMenuFore
            // 
            this.mainNotifyMenuFore.Image = global::Plexdata.TrayNotes.Properties.Resources.Edit16;
            this.mainNotifyMenuFore.Name = "mainNotifyMenuFore";
            this.mainNotifyMenuFore.Size = new System.Drawing.Size(137, 22);
            this.mainNotifyMenuFore.Text = "Activate...";
            this.mainNotifyMenuFore.Visible = false;
            this.mainNotifyMenuFore.Click += new System.EventHandler(this.OnPerformFore);
            // 
            // mainNotifyMenuPaste
            // 
            this.mainNotifyMenuPaste.Image = ((System.Drawing.Image)(resources.GetObject("mainNotifyMenuPaste.Image")));
            this.mainNotifyMenuPaste.Name = "mainNotifyMenuPaste";
            this.mainNotifyMenuPaste.Size = new System.Drawing.Size(137, 22);
            this.mainNotifyMenuPaste.Text = "Paste";
            this.mainNotifyMenuPaste.Click += new System.EventHandler(this.OnPerformPaste);
            // 
            // mainNotifyMenuSettings
            // 
            this.mainNotifyMenuSettings.Image = ((System.Drawing.Image)(resources.GetObject("mainNotifyMenuSettings.Image")));
            this.mainNotifyMenuSettings.Name = "mainNotifyMenuSettings";
            this.mainNotifyMenuSettings.Size = new System.Drawing.Size(137, 22);
            this.mainNotifyMenuSettings.Text = "Settings...";
            this.mainNotifyMenuSettings.Click += new System.EventHandler(this.OnPerformSettings);
            // 
            // mainNotifyMenuSep2
            // 
            this.mainNotifyMenuSep2.Name = "mainNotifyMenuSep2";
            this.mainNotifyMenuSep2.Size = new System.Drawing.Size(134, 6);
            // 
            // mainNotifyMenuExit
            // 
            this.mainNotifyMenuExit.Image = ((System.Drawing.Image)(resources.GetObject("mainNotifyMenuExit.Image")));
            this.mainNotifyMenuExit.Name = "mainNotifyMenuExit";
            this.mainNotifyMenuExit.Size = new System.Drawing.Size(137, 22);
            this.mainNotifyMenuExit.Text = "Exit";
            this.mainNotifyMenuExit.Click += new System.EventHandler(this.OnPerformExit);
            // 
            // mainToolbar
            // 
            this.mainToolbar.ContextMenuStrip = this.mainToolbarMenu;
            this.mainToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.mainToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbQuit,
            this.tbsSep1,
            this.tbbNew,
            this.tbbLoad,
            this.tbbSave,
            this.tbbSaveAll,
            this.tbbClear,
            this.tbbClose,
            this.tbsSep2,
            this.tbbCopy,
            this.tbbPaste,
            this.tbsSep3,
            this.tbbSettings,
            this.tbbAbout});
            this.mainToolbar.Location = new System.Drawing.Point(0, 0);
            this.mainToolbar.Name = "mainToolbar";
            this.mainToolbar.Padding = new System.Windows.Forms.Padding(2, 2, 2, 1);
            this.mainToolbar.Size = new System.Drawing.Size(359, 27);
            this.mainToolbar.Stretch = true;
            this.mainToolbar.TabIndex = 0;
            // 
            // mainToolbarMenu
            // 
            this.mainToolbarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolbarMenuLarge,
            this.mainToolbarMenuText});
            this.mainToolbarMenu.Name = "mainToolbarMenu";
            this.mainToolbarMenu.Size = new System.Drawing.Size(172, 48);
            this.mainToolbarMenu.Opening += new System.ComponentModel.CancelEventHandler(this.OnMainToolbarMenuOpening);
            // 
            // mainToolbarMenuLarge
            // 
            this.mainToolbarMenuLarge.CheckOnClick = true;
            this.mainToolbarMenuLarge.Name = "mainToolbarMenuLarge";
            this.mainToolbarMenuLarge.Size = new System.Drawing.Size(171, 22);
            this.mainToolbarMenuLarge.Text = "Use large icons";
            this.mainToolbarMenuLarge.Click += new System.EventHandler(this.OnMainToolbarMenuItemClick);
            // 
            // mainToolbarMenuText
            // 
            this.mainToolbarMenuText.CheckOnClick = true;
            this.mainToolbarMenuText.Name = "mainToolbarMenuText";
            this.mainToolbarMenuText.Size = new System.Drawing.Size(171, 22);
            this.mainToolbarMenuText.Text = "Show button text";
            this.mainToolbarMenuText.Click += new System.EventHandler(this.OnMainToolbarMenuItemClick);
            // 
            // tbbQuit
            // 
            this.tbbQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbQuit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbQuitHide,
            this.tbbQuitExit});
            this.tbbQuit.Image = global::Plexdata.TrayNotes.Properties.Resources.Quit16;
            this.tbbQuit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbQuit.Margin = new System.Windows.Forms.Padding(1);
            this.tbbQuit.Name = "tbbQuit";
            this.tbbQuit.Padding = new System.Windows.Forms.Padding(1);
            this.tbbQuit.Size = new System.Drawing.Size(34, 22);
            this.tbbQuit.Text = "Quit";
            this.tbbQuit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbQuit.ToolTipText = "Hide the program\'s main window \r\nor close the application at all.";
            this.tbbQuit.Click += new System.EventHandler(this.OnQuitButtonClick);
            // 
            // tbbQuitHide
            // 
            this.tbbQuitHide.Image = ((System.Drawing.Image)(resources.GetObject("tbbQuitHide.Image")));
            this.tbbQuitHide.Name = "tbbQuitHide";
            this.tbbQuitHide.Size = new System.Drawing.Size(106, 22);
            this.tbbQuitHide.Text = "Hide";
            this.tbbQuitHide.Click += new System.EventHandler(this.OnPerformHide);
            // 
            // tbbQuitExit
            // 
            this.tbbQuitExit.Image = ((System.Drawing.Image)(resources.GetObject("tbbQuitExit.Image")));
            this.tbbQuitExit.Name = "tbbQuitExit";
            this.tbbQuitExit.Size = new System.Drawing.Size(106, 22);
            this.tbbQuitExit.Text = "Exit";
            this.tbbQuitExit.Click += new System.EventHandler(this.OnPerformExit);
            // 
            // tbsSep1
            // 
            this.tbsSep1.Name = "tbsSep1";
            this.tbsSep1.Size = new System.Drawing.Size(6, 24);
            // 
            // tbbNew
            // 
            this.tbbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbNew.Image = ((System.Drawing.Image)(resources.GetObject("tbbNew.Image")));
            this.tbbNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbNew.Margin = new System.Windows.Forms.Padding(1);
            this.tbbNew.Name = "tbbNew";
            this.tbbNew.Padding = new System.Windows.Forms.Padding(1);
            this.tbbNew.Size = new System.Drawing.Size(23, 22);
            this.tbbNew.Text = "New";
            this.tbbNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbNew.ToolTipText = "Add an additional drop target tab page.";
            this.tbbNew.Click += new System.EventHandler(this.OnPerformNew);
            // 
            // tbbLoad
            // 
            this.tbbLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbLoad.Image = global::Plexdata.TrayNotes.Properties.Resources.Load16;
            this.tbbLoad.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbLoad.Margin = new System.Windows.Forms.Padding(1);
            this.tbbLoad.Name = "tbbLoad";
            this.tbbLoad.Padding = new System.Windows.Forms.Padding(1);
            this.tbbLoad.Size = new System.Drawing.Size(23, 22);
            this.tbbLoad.Text = "Load";
            this.tbbLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbLoad.ToolTipText = "Load a previously saved drop data file. But be \r\naware to choose only files that " +
                "can be changed.";
            this.tbbLoad.Click += new System.EventHandler(this.OnPerformLoad);
            // 
            // tbbSave
            // 
            this.tbbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSave.Enabled = false;
            this.tbbSave.Image = ((System.Drawing.Image)(resources.GetObject("tbbSave.Image")));
            this.tbbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSave.Margin = new System.Windows.Forms.Padding(1);
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.Padding = new System.Windows.Forms.Padding(1);
            this.tbbSave.Size = new System.Drawing.Size(23, 22);
            this.tbbSave.Text = "Save";
            this.tbbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbSave.ToolTipText = "Save current drop data tab page into an external file.";
            this.tbbSave.Click += new System.EventHandler(this.OnPerformSave);
            // 
            // tbbSaveAll
            // 
            this.tbbSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("tbbSaveAll.Image")));
            this.tbbSaveAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSaveAll.Margin = new System.Windows.Forms.Padding(1);
            this.tbbSaveAll.Name = "tbbSaveAll";
            this.tbbSaveAll.Padding = new System.Windows.Forms.Padding(1);
            this.tbbSaveAll.Size = new System.Drawing.Size(23, 22);
            this.tbbSaveAll.Text = "Save All";
            this.tbbSaveAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbSaveAll.ToolTipText = "Save all currently modified drop data pages into external files.";
            this.tbbSaveAll.Click += new System.EventHandler(this.OnPerformSaveAll);
            // 
            // tbbClear
            // 
            this.tbbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbClear.Enabled = false;
            this.tbbClear.Image = global::Plexdata.TrayNotes.Properties.Resources.Clear16;
            this.tbbClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbClear.Margin = new System.Windows.Forms.Padding(1);
            this.tbbClear.Name = "tbbClear";
            this.tbbClear.Padding = new System.Windows.Forms.Padding(1);
            this.tbbClear.Size = new System.Drawing.Size(23, 22);
            this.tbbClear.Text = "Clear";
            this.tbbClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbClear.ToolTipText = "Clear content of current drop data tab page.";
            this.tbbClear.Click += new System.EventHandler(this.OnPerformClear);
            // 
            // tbbClose
            // 
            this.tbbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbClose.Image = ((System.Drawing.Image)(resources.GetObject("tbbClose.Image")));
            this.tbbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbClose.Margin = new System.Windows.Forms.Padding(1);
            this.tbbClose.Name = "tbbClose";
            this.tbbClose.Padding = new System.Windows.Forms.Padding(1);
            this.tbbClose.Size = new System.Drawing.Size(23, 22);
            this.tbbClose.Text = "Close";
            this.tbbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbClose.ToolTipText = "Close current drop data tab page. Unsaved \r\nchanges may saved into an external fi" +
                "le.";
            this.tbbClose.Click += new System.EventHandler(this.OnPerformClose);
            // 
            // tbsSep2
            // 
            this.tbsSep2.Name = "tbsSep2";
            this.tbsSep2.Size = new System.Drawing.Size(6, 24);
            // 
            // tbbCopy
            // 
            this.tbbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbCopy.Enabled = false;
            this.tbbCopy.Image = global::Plexdata.TrayNotes.Properties.Resources.Copy16;
            this.tbbCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbCopy.Margin = new System.Windows.Forms.Padding(1);
            this.tbbCopy.Name = "tbbCopy";
            this.tbbCopy.Padding = new System.Windows.Forms.Padding(1);
            this.tbbCopy.Size = new System.Drawing.Size(23, 22);
            this.tbbCopy.Text = "Copy";
            this.tbbCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbCopy.ToolTipText = "Copy current selected to the Clipboard.";
            this.tbbCopy.Click += new System.EventHandler(this.OnPerformCopy);
            // 
            // tbbPaste
            // 
            this.tbbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbPaste.Image = ((System.Drawing.Image)(resources.GetObject("tbbPaste.Image")));
            this.tbbPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbPaste.Margin = new System.Windows.Forms.Padding(1);
            this.tbbPaste.Name = "tbbPaste";
            this.tbbPaste.Padding = new System.Windows.Forms.Padding(1);
            this.tbbPaste.Size = new System.Drawing.Size(23, 22);
            this.tbbPaste.Text = "Paste";
            this.tbbPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbPaste.ToolTipText = "Paste current Clipboard content into active drop target tab page.";
            this.tbbPaste.Click += new System.EventHandler(this.OnPerformPaste);
            // 
            // tbsSep3
            // 
            this.tbsSep3.Name = "tbsSep3";
            this.tbsSep3.Size = new System.Drawing.Size(6, 24);
            // 
            // tbbSettings
            // 
            this.tbbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tbbSettings.Image")));
            this.tbbSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSettings.Margin = new System.Windows.Forms.Padding(1);
            this.tbbSettings.Name = "tbbSettings";
            this.tbbSettings.Padding = new System.Windows.Forms.Padding(1);
            this.tbbSettings.Size = new System.Drawing.Size(23, 22);
            this.tbbSettings.Text = "Settings";
            this.tbbSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbSettings.ToolTipText = "Open the settings dialog.";
            this.tbbSettings.Click += new System.EventHandler(this.OnPerformSettings);
            // 
            // tbbAbout
            // 
            this.tbbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbAbout.Image = global::Plexdata.TrayNotes.Properties.Resources.About16;
            this.tbbAbout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbAbout.Margin = new System.Windows.Forms.Padding(1);
            this.tbbAbout.Name = "tbbAbout";
            this.tbbAbout.Padding = new System.Windows.Forms.Padding(1);
            this.tbbAbout.Size = new System.Drawing.Size(23, 22);
            this.tbbAbout.Text = "About";
            this.tbbAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbAbout.ToolTipText = "Show the about box.";
            this.tbbAbout.Click += new System.EventHandler(this.OnPerformAbout);
            // 
            // pageHeaderMenu
            // 
            this.pageHeaderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pageHeaderMenuClose,
            this.pageHeaderMenuSep1,
            this.pageHeaderMenuSave,
            this.pageHeaderMenuCopy,
            this.pageHeaderMenuPaste,
            this.pageHeaderMenuClear});
            this.pageHeaderMenu.Name = "pageContextMenu";
            this.pageHeaderMenu.Size = new System.Drawing.Size(153, 142);
            this.pageHeaderMenu.Opening += new System.ComponentModel.CancelEventHandler(this.OnPageHeaderMenuOpening);
            // 
            // pageHeaderMenuClose
            // 
            this.pageHeaderMenuClose.Image = ((System.Drawing.Image)(resources.GetObject("pageHeaderMenuClose.Image")));
            this.pageHeaderMenuClose.Name = "pageHeaderMenuClose";
            this.pageHeaderMenuClose.Size = new System.Drawing.Size(152, 22);
            this.pageHeaderMenuClose.Text = "Close";
            this.pageHeaderMenuClose.Click += new System.EventHandler(this.OnPerformClose);
            // 
            // pageHeaderMenuSep1
            // 
            this.pageHeaderMenuSep1.Name = "pageHeaderMenuSep1";
            this.pageHeaderMenuSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // pageHeaderMenuSave
            // 
            this.pageHeaderMenuSave.Image = ((System.Drawing.Image)(resources.GetObject("pageHeaderMenuSave.Image")));
            this.pageHeaderMenuSave.Name = "pageHeaderMenuSave";
            this.pageHeaderMenuSave.Size = new System.Drawing.Size(152, 22);
            this.pageHeaderMenuSave.Text = "Save";
            this.pageHeaderMenuSave.Click += new System.EventHandler(this.OnPerformSave);
            // 
            // pageHeaderMenuPaste
            // 
            this.pageHeaderMenuPaste.Image = ((System.Drawing.Image)(resources.GetObject("pageHeaderMenuPaste.Image")));
            this.pageHeaderMenuPaste.Name = "pageHeaderMenuPaste";
            this.pageHeaderMenuPaste.Size = new System.Drawing.Size(152, 22);
            this.pageHeaderMenuPaste.Text = "Paste";
            this.pageHeaderMenuPaste.Click += new System.EventHandler(this.OnPerformPaste);
            // 
            // pageHeaderMenuClear
            // 
            this.pageHeaderMenuClear.Image = global::Plexdata.TrayNotes.Properties.Resources.Clear16;
            this.pageHeaderMenuClear.Name = "pageHeaderMenuClear";
            this.pageHeaderMenuClear.Size = new System.Drawing.Size(152, 22);
            this.pageHeaderMenuClear.Text = "Clear";
            this.pageHeaderMenuClear.Click += new System.EventHandler(this.OnPerformClear);
            // 
            // mainStatusBar
            // 
            this.mainStatusBar.Location = new System.Drawing.Point(0, 201);
            this.mainStatusBar.Name = "mainStatusBar";
            this.mainStatusBar.Size = new System.Drawing.Size(359, 22);
            this.mainStatusBar.TabIndex = 1;
            // 
            // mainToolbarContainer
            // 
            this.mainToolbarContainer.BottomToolStripPanelVisible = false;
            // 
            // mainToolbarContainer.ContentPanel
            // 
            this.mainToolbarContainer.ContentPanel.Controls.Add(this.tabEditView);
            this.mainToolbarContainer.ContentPanel.Padding = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.mainToolbarContainer.ContentPanel.Size = new System.Drawing.Size(359, 174);
            this.mainToolbarContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainToolbarContainer.LeftToolStripPanelVisible = false;
            this.mainToolbarContainer.Location = new System.Drawing.Point(0, 0);
            this.mainToolbarContainer.Name = "mainToolbarContainer";
            this.mainToolbarContainer.RightToolStripPanelVisible = false;
            this.mainToolbarContainer.Size = new System.Drawing.Size(359, 201);
            this.mainToolbarContainer.TabIndex = 0;
            this.mainToolbarContainer.Text = "mainToolbarContainer";
            // 
            // mainToolbarContainer.TopToolStripPanel
            // 
            this.mainToolbarContainer.TopToolStripPanel.Controls.Add(this.mainToolbar);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "rtf";
            this.saveFileDialog.Filter = "Rich text files (*.rtf)|*.rtf|Plain text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.saveFileDialog.FilterIndex = 3;
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "rtf";
            this.openFileDialog.Filter = "Rich text files (*.rtf)|*.rtf|Plain text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.openFileDialog.FilterIndex = 3;
            this.openFileDialog.RestoreDirectory = true;
            // 
            // tabEditView
            // 
            this.tabEditView.AllowDrop = true;
            this.tabEditView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEditView.ItemSize = new System.Drawing.Size(0, 20);
            this.tabEditView.Location = new System.Drawing.Point(2, 2);
            this.tabEditView.Multiline = true;
            this.tabEditView.Name = "tabEditView";
            this.tabEditView.Padding = new System.Drawing.Point(20, 3);
            this.tabEditView.PageContextMenu = this.pageHeaderMenu;
            this.tabEditView.SelectedIndex = 0;
            this.tabEditView.Size = new System.Drawing.Size(357, 172);
            this.tabEditView.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabEditView.TabIndex = 0;
            this.tabEditView.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnEditViewDragDrop);
            this.tabEditView.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnEditViewDragEnter);
            // 
            // pageHeaderMenuCopy
            // 
            this.pageHeaderMenuCopy.Image = ((System.Drawing.Image)(resources.GetObject("pageHeaderMenuCopy.Image")));
            this.pageHeaderMenuCopy.Name = "pageHeaderMenuCopy";
            this.pageHeaderMenuCopy.Size = new System.Drawing.Size(152, 22);
            this.pageHeaderMenuCopy.Text = "Copy";
            this.pageHeaderMenuCopy.Click += new System.EventHandler(this.OnPerformCopy);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 223);
            this.Controls.Add(this.mainToolbarContainer);
            this.Controls.Add(this.mainStatusBar);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tray Notes";
            this.mainNotifyMenu.ResumeLayout(false);
            this.mainToolbar.ResumeLayout(false);
            this.mainToolbar.PerformLayout();
            this.mainToolbarMenu.ResumeLayout(false);
            this.pageHeaderMenu.ResumeLayout(false);
            this.mainToolbarContainer.ContentPanel.ResumeLayout(false);
            this.mainToolbarContainer.TopToolStripPanel.ResumeLayout(false);
            this.mainToolbarContainer.TopToolStripPanel.PerformLayout();
            this.mainToolbarContainer.ResumeLayout(false);
            this.mainToolbarContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon mainNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip mainNotifyMenu;
        private System.Windows.Forms.ToolStripMenuItem mainNotifyMenuShow;
        private System.Windows.Forms.ToolStripMenuItem mainNotifyMenuHide;
        private System.Windows.Forms.ToolStripMenuItem mainNotifyMenuPaste;
        private System.Windows.Forms.ToolStripMenuItem mainNotifyMenuSettings;
        private System.Windows.Forms.ToolStripSeparator mainNotifyMenuSep1;
        private System.Windows.Forms.ToolStripMenuItem mainNotifyMenuExit;
        private Plexdata.TrayNotes.TabViewEx tabEditView;
        private System.Windows.Forms.ToolStrip mainToolbar;
        private System.Windows.Forms.ToolStripSeparator tbsSep1;
        private System.Windows.Forms.ToolStripButton tbbNew;
        private System.Windows.Forms.ToolStripButton tbbSave;
        private System.Windows.Forms.ToolStripButton tbbSaveAll;
        private System.Windows.Forms.ToolStripButton tbbClose;
        private System.Windows.Forms.ContextMenuStrip pageHeaderMenu;
        private System.Windows.Forms.ToolStripMenuItem pageHeaderMenuSave;
        private System.Windows.Forms.ToolStripMenuItem pageHeaderMenuClose;
        private System.Windows.Forms.ToolStripSplitButton tbbQuit;
        private System.Windows.Forms.ToolStripMenuItem tbbQuitHide;
        private System.Windows.Forms.ToolStripMenuItem tbbQuitExit;
        private System.Windows.Forms.ToolStripButton tbbPaste;
        private System.Windows.Forms.ToolStripSeparator tbsSep2;
        private System.Windows.Forms.ToolStripSeparator tbsSep3;
        private System.Windows.Forms.ToolStripButton tbbSettings;
        private System.Windows.Forms.ToolStripMenuItem pageHeaderMenuPaste;
        private System.Windows.Forms.ToolStripMenuItem pageHeaderMenuClear;
        private System.Windows.Forms.ToolStripButton tbbClear;
        private System.Windows.Forms.ToolStripSeparator pageHeaderMenuSep1;
        private System.Windows.Forms.StatusStrip mainStatusBar;
        private System.Windows.Forms.ToolStripContainer mainToolbarContainer;
        private System.Windows.Forms.ContextMenuStrip mainToolbarMenu;
        private System.Windows.Forms.ToolStripMenuItem mainToolbarMenuLarge;
        private System.Windows.Forms.ToolStripMenuItem mainToolbarMenuText;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripButton tbbAbout;
        private System.Windows.Forms.ToolStripMenuItem mainNotifyMenuAbout;
        private System.Windows.Forms.ToolStripSeparator mainNotifyMenuSep2;
        private System.Windows.Forms.ToolStripMenuItem mainNotifyMenuFore;
        private System.Windows.Forms.ToolStripButton tbbLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripButton tbbCopy;
        private System.Windows.Forms.ToolStripMenuItem pageHeaderMenuCopy;
    }
}

