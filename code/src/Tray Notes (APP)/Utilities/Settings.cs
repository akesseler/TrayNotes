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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Plexdata.TrayNotes
{
    public class Settings
    {
        #region Construction.

        public Settings()
            : base()
        {
            this.Defaults();
        }

        #endregion // Construction.

        #region Public static property implementation.

        public static string Filename
        {
            get
            {
                // Win7:  C:\Users\<user>\AppData\Roaming\<company>\<product>\settings.xml
                // WinXP: C:\Documents and Settings\<user>\Application Data\<company>\<product>\settings.xml"
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    Path.Combine(
                        Path.Combine(
                            Application.CompanyName,
                            Application.ProductName),
                        "settings.xml"));
            }
        }

        #endregion // Public static property implementation.

        #region Public static member implementation.

        public static bool Save(Settings settings)
        {
            return Settings.Save(Settings.Filename, settings);
        }

        public static bool Save(string filename, Settings settings)
        {
            bool success = false;

            try
            {
                if (!String.IsNullOrEmpty(filename) && settings != null)
                {
                    if (!Directory.Exists(Path.GetDirectoryName(filename)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filename));
                    }

                    using (TextWriter writer = new StreamWriter(filename))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Settings));

                        serializer.Serialize(writer, settings);

                        success = true; // No exception? Fine!
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return success;
        }

        public static bool Load(out Settings result)
        {
            return Settings.Load(Settings.Filename, out result);
        }

        public static bool Load(string filename, out Settings result)
        {
            result = null;

            try
            {
                if (!String.IsNullOrEmpty(filename) && File.Exists(filename))
                {
                    using (TextReader reader = new StreamReader(filename))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Settings));

                        result = (Settings)serializer.Deserialize(reader);

                        if (result != null) { result.EnsureScreenLocation(); }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return result != null;
        }

        #endregion // Public static member implementation.

        #region Main window related settings.

        public Size Size { get; set; }

        public Point Location { get; set; }

        #endregion // Main window related settings.

        #region Configurable settings.

        public bool UsePasteLabel { get; set; }

        public bool UsePasteLabelDate { get; set; }

        public bool DiscardAllDataAtExit { get; set; }

        [XmlIgnore]
        public Color EditBoxBackColor { get; set; }

        [XmlElement("EditBoxBackColor")]
        public string EditBoxBackColorXML
        {
            get
            {
                return ColorTranslator.ToHtml(this.EditBoxBackColor);
            }
            set
            {
                try
                {
                    this.EditBoxBackColor = ColorTranslator.FromHtml(value);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    this.EditBoxBackColor = Color.White;
                }
            }
        }

        public bool UseToolbarButtonText { get; set; }

        public bool UseToolbarLargeIcons { get; set; }

        #endregion // Configurable settings.

        #region Public member implementation.

        public void Defaults()
        {
            this.Size = new Size(550, 400);

            // Move window to right/lower location by default.
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(
                screen.Right - (this.Size.Width + 10),
                screen.Bottom - (this.Size.Height + 10));

            this.UsePasteLabel = true;
            this.UsePasteLabelDate = true;
            this.DiscardAllDataAtExit = true;
            this.EditBoxBackColor = Color.White;
            this.UseToolbarButtonText = true;
            this.UseToolbarLargeIcons = false;
        }

        #endregion // Public member implementation.

        #region Private member implementation.

        private void EnsureScreenLocation()
        {
            Rectangle bounds = Screen.PrimaryScreen.WorkingArea;
            Point location = this.Location;
            Size size = this.Size;

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.Contains(new Rectangle(location, size)))
                {
                    return; // Location is still within bounds.
                }
            }

            // Adjust X location to be on the primary screen!

            int x = 0;
            if (location.X < bounds.Left)
            {
                x = bounds.Left;
            }
            else if (location.X + size.Width > bounds.Left + bounds.Right)
            {
                x = bounds.Right - size.Width;
            }
            else
            {
                x = location.X;
            }

            // Adjust Y location to be on the primary screen!

            int y = 0;
            if (location.Y < bounds.Top)
            {
                y = bounds.Top;
            }
            else if (location.Y + size.Height > bounds.Top + bounds.Bottom)
            {
                y = bounds.Bottom - size.Height;
            }
            else
            {
                y = location.Y;
            }

            this.Location = new Point(x, y);
        }

        #endregion // Private member implementation.
    }
}
