﻿/*
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

using System.Drawing;
using System.Windows.Forms;

namespace Plexdata.TrayNotes
{
    internal class CustomColorTable : ProfessionalColorTable
    {
        public CustomColorTable()
            : base()
        {
        }

        public override Color ImageMarginGradientBegin
        {
            get { return SystemColors.ControlDark; }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return SystemColors.Control; }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return SystemColors.Control; }
        }

        public override Color CheckBackground
        {
            get { return Color.NavajoWhite; }
        }

        public override Color CheckPressedBackground
        {
            get { return Color.DeepSkyBlue; }
        }

        public override Color CheckSelectedBackground
        {
            get { return Color.SkyBlue; }
        }

        public override Color MenuItemBorder
        {
            get { return Color.DarkBlue; }
        }

        public override Color MenuItemSelected
        {
            get { return Color.LightBlue; }
        }

        public override Color ToolStripBorder
        {
            get { return SystemColors.ControlDarkDark; }
        }

        public override Color ToolStripGradientBegin
        {
            get { return SystemColors.ControlLight; }
        }

        public override Color ToolStripGradientMiddle
        {
            get { return SystemColors.Control; }
        }

        public override Color ToolStripGradientEnd
        {
            get { return SystemColors.ControlDark; }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.NavajoWhite; }
        }

        public override Color MenuItemPressedGradientMiddle
        {
            get { return Color.NavajoWhite; }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.NavajoWhite; }
        }

        public override Color ButtonSelectedGradientBegin
        {
            get { return Color.LightBlue; }
        }

        public override Color ButtonSelectedGradientMiddle
        {
            get { return Color.LightBlue; }
        }

        public override Color ButtonSelectedGradientEnd
        {
            get { return Color.LightBlue; }
        }

        public override Color ButtonPressedGradientBegin
        {
            get { return Color.NavajoWhite; }
        }

        public override Color ButtonPressedGradientMiddle
        {
            get { return Color.NavajoWhite; }
        }

        public override Color ButtonPressedGradientEnd
        {
            get { return Color.NavajoWhite; }
        }
    }
}
