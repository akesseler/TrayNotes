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
    public partial class SettingsDialog : Form
    {
        private static string ShourtcutName = AboutBox.Title;

        private bool locked = false;

        public SettingsDialog()
            : base()
        {
            this.MainForm = null;
            this.InitializeComponent();
        }

        public SettingsDialog(MainForm parent)
            : this()
        {
            Debug.Assert(parent != null);
            this.MainForm = parent;
        }

        public MainForm MainForm { get; set; }

        protected override void OnLoad(EventArgs args)
        {
            Debug.Assert(this.MainForm != null);

            base.OnLoad(args);

            if (this.MainForm != null)
            {
                try
                {
                    using (new CursorWrapper(this, Cursors.AppStarting))
                    {
                        this.valPasteLabel.Checked = this.MainForm.UsePasteLabel;
                        this.valPasteDate.Checked = this.MainForm.UsePasteLabelDate;
                        this.valDiscardData.Checked = this.MainForm.DiscardAllDataAtExit;

                        this.valBackColor.SelectedColor = this.MainForm.EditBoxBackColor;
                        this.valLargeIcons.Checked = this.MainForm.UseToolbarLargeIcons;
                        this.valButtonText.Checked = this.MainForm.UseToolbarButtonText;

                        // Checked change may not happen...
                        this.valPasteDate.Enabled = this.valPasteLabel.Checked;

                        this.valAddStartup.Checked = plexdata.Shell.Shortcut.IsStartupShortcut(ShourtcutName);
                        this.valAddDesktop.Checked = plexdata.Shell.Shortcut.IsDesktopShortcut(ShourtcutName);
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            }
        }

        private void OnAcceptButtonClick(object sender, EventArgs args)
        {
            if (this.MainForm != null)
            {
                try
                {
                    using (new CursorWrapper(this, Cursors.AppStarting))
                    {
                        this.MainForm.UsePasteLabel = this.valPasteLabel.Checked;
                        this.MainForm.UsePasteLabelDate = this.valPasteDate.Checked;
                        this.MainForm.DiscardAllDataAtExit = this.valDiscardData.Checked;

                        this.MainForm.EditBoxBackColor = this.valBackColor.SelectedColor;
                        this.MainForm.UseToolbarLargeIcons = this.valLargeIcons.Checked;
                        this.MainForm.UseToolbarButtonText = this.valButtonText.Checked;
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            }
        }

        private void OnCancelButtonClick(object sender, EventArgs args)
        {
            // Nothing else to do...
        }

        private void OnPasteLabelCheckedChanged(object sender, EventArgs args)
        {
            this.valPasteDate.Enabled = this.valPasteLabel.Checked;
        }

        private void OnStartupShortcutCheckedChanged(object sender, EventArgs args)
        {
            if (this.locked) { return; }

            this.locked = true;

            try
            {
                using (new CursorWrapper(this, Cursors.AppStarting))
                {
                    if (this.valAddStartup.Checked)
                    {
                        plexdata.Shell.Shortcut.CreateStartupShortcut(ShourtcutName);
                    }
                    else
                    {
                        plexdata.Shell.Shortcut.RemoveStartupShortcut(ShourtcutName);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
                // Reflect current state. This might be interesting under error conditions. If current 
                // checked state and shortcut availability is the same the "checked changed" event is 
                // not fired again. The other way round, in case of a difference the second execution 
                // is locked, see above.
                this.valAddStartup.Checked = plexdata.Shell.Shortcut.IsStartupShortcut(ShourtcutName);
                this.locked = false;
            }
        }

        private void OnDesktopShortcutCheckedChanged(object sender, EventArgs args)
        {
            if (this.locked) { return; }

            this.locked = true;

            try
            {
                using (new CursorWrapper(this, Cursors.AppStarting))
                {
                    if (this.valAddDesktop.Checked)
                    {
                        plexdata.Shell.Shortcut.CreateDesktopShortcut(ShourtcutName);
                    }
                    else
                    {
                        plexdata.Shell.Shortcut.RemoveDesktopShortcut(ShourtcutName);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
                // Reflect current state. This might be interesting under error conditions. If current 
                // checked state and shortcut availability is the same the "checked changed" event is 
                // not fired again. The other way round, in case of a difference the second execution 
                // is locked, see above.
                this.valAddDesktop.Checked = plexdata.Shell.Shortcut.IsDesktopShortcut(ShourtcutName);
                this.locked = false;
            }
        }
    }
}
