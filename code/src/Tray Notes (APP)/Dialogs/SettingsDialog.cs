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

                        this.valAddStartup.Checked = Plexdata.Shell.Shortcut.IsStartupShortcut(ShourtcutName);
                        this.valAddDesktop.Checked = Plexdata.Shell.Shortcut.IsDesktopShortcut(ShourtcutName);
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
                        Plexdata.Shell.Shortcut.CreateStartupShortcut(ShourtcutName);
                    }
                    else
                    {
                        Plexdata.Shell.Shortcut.RemoveStartupShortcut(ShourtcutName);
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
                this.valAddStartup.Checked = Plexdata.Shell.Shortcut.IsStartupShortcut(ShourtcutName);
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
                        Plexdata.Shell.Shortcut.CreateDesktopShortcut(ShourtcutName);
                    }
                    else
                    {
                        Plexdata.Shell.Shortcut.RemoveDesktopShortcut(ShourtcutName);
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
                this.valAddDesktop.Checked = Plexdata.Shell.Shortcut.IsDesktopShortcut(ShourtcutName);
                this.locked = false;
            }
        }
    }
}
