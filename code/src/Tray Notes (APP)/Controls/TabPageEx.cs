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
using System.IO;
using System.Windows.Forms;

namespace Plexdata.TrayNotes
{
    internal class TabPageEx : TabPage
    {
        private RichEditEx editBox = null;

        public TabPageEx(string name)
            : base()
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            this.editBox = new RichEditEx();

            if (this.editBox == null)
            {
                throw new OutOfMemoryException();
            }

            this.Text = name;
            this.Name = name;
            this.Padding = new Padding(0, 2, 2, 2);
            this.DoubleBuffered = true;
            this.UseVisualStyleBackColor = true;

            this.Controls.Add(this.editBox);

            this.editBox.TextMargin = new Padding(5);
            this.editBox.BorderStyle = BorderStyle.FixedSingle;

            this.editBox.Dock = DockStyle.Fill;
            this.editBox.Name = String.Format("{0}_EditBox", this.Text);
            this.editBox.WordWrap = false;
            this.editBox.TabIndex = 0;

            this.editBox.IsDirtyChanged += new EventHandler(this.OnEditBoxIsDirtyChanged);
            this.editBox.MouseDown += new MouseEventHandler(this.OnEditBoxMouseDown);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FullName
        {
            get
            {
                return this.fullname;
            }
            set
            {
                if (this.fullname != value)
                {
                    this.fullname = value;
                }
            }
        }
        private string fullname = String.Empty;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDirty
        {
            get
            {
                return this.editBox.IsDirty;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RichEditEx EditBox
        {
            get
            {
                return this.editBox;
            }
        }

        public void Activate()
        {
            this.editBox.Select();
        }

        public bool Load(string fullname)
        {
            bool success = false;

            try
            {
                if (!this.editBox.IsDirty && File.Exists(fullname))
                {
                    this.Name = Path.GetFileName(fullname);
                    this.Text = Path.GetFileName(fullname);

                    this.FullName = fullname;

                    // After spending some hours it came up that there is no easy or simple way to 
                    // load an unspecific file type. The only file type that can be distinguished 
                    // reliably is RTF (surprise surprise). Therefore, keep it simple and try loading 
                    // any file type, even if it means that binaries can be loaded as well.

                    if (this.IsRichTextFile(fullname))
                    {
                        this.editBox.LoadFile(fullname, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        if (this.IsUnicodeFile(fullname))
                        {
                            this.editBox.LoadFile(fullname, RichTextBoxStreamType.UnicodePlainText);
                        }
                        else
                        {
                            this.editBox.LoadFile(fullname, RichTextBoxStreamType.PlainText);
                        }
                    }

                    success = true;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);

                this.SetErrorText(String.Format(
                    "Loading file \"{0}\" has failed.{1}{1}{2}{1}",
                    (!String.IsNullOrEmpty(fullname) ? fullname : "???"),
                    Environment.NewLine, exception.Message));
            }
            finally
            {
                this.editBox.IsDirty = false;
            }

            return success;
        }

        public bool Save(string fullname)
        {
            bool success = false;

            try
            {
                if (this.editBox.IsDirty)
                {
                    this.Name = Path.GetFileName(fullname);
                    this.Text = Path.GetFileName(fullname);

                    this.FullName = fullname;

                    if (Path.GetExtension(fullname).ToLower() == ".rtf")
                    {
                        this.editBox.SaveFile(fullname, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        this.editBox.SaveFile(fullname, RichTextBoxStreamType.PlainText);
                    }

                    success = true;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);

                this.SetErrorText(String.Format(
                    "Saving file \"{0}\" has failed.{1}{1}{2}{1}",
                    (!String.IsNullOrEmpty(fullname) ? fullname : "???"),
                    Environment.NewLine, exception.Message));
            }
            finally
            {
                this.editBox.IsDirty = false;
            }

            return success;
        }

        private void OnEditBoxIsDirtyChanged(object sender, EventArgs args)
        {
            this.Text = this.Name + (this.editBox.IsDirty ? " *" : "");
        }

        private void OnEditBoxMouseDown(object sender, MouseEventArgs args)
        {
            // Try to obtain the context-menu for the edit-box right 
            // from the main window. All handling is done there...
            if (args.Button == MouseButtons.Right)
            {
                MainForm form = this.FindForm() as MainForm;
                if (form != null && form.PageHeaderMenu != null)
                {
                    form.PageHeaderMenu.Show(this, args.Location);
                }
            }
        }

        private bool IsRichTextFile(string fullname)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fullname))
                {
                    string rtf = "{\\rtf";
                    char[] tmp = new char[rtf.Length];
                    if (rtf.Length == reader.Read(tmp, 0, tmp.Length))
                    {
                        return (new string(tmp)).ToLower().StartsWith(rtf, StringComparison.InvariantCultureIgnoreCase);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return false;
        }

        private bool IsUnicodeFile(string fullname)
        {
            try
            {
                bool match = true;

                // Given file may contain big-endian Unicode, but don't care.
                using (StreamReader reader = new StreamReader(fullname))
                {
                    // Read first 100 characters and check if every second character is zero. 
                    // If so, then stream reader was able to load the file with Unicode encoding.
                    // Stop scanning either if not matching or the stream end has been reached.
                    for (int index = 0; match && index < 100 && !reader.EndOfStream; index++)
                    {
                        int value = reader.Read();
                        if (index > 0 && (index % 2) == 1)
                        {
                            match &= value == 0;
                        }
                    }
                }

                return match;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return false;
        }

        private void SetErrorText(string message)
        {
            try
            {
                Color color = this.editBox.SelectionColor;
                this.editBox.MoveEnd();
                this.editBox.SelectionColor = Color.Red;
                this.editBox.AppendText(message);
                this.editBox.SelectionColor = color;
                this.editBox.MoveEnd();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
    }
}
