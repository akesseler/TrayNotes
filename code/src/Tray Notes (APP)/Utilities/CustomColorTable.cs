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

namespace plexdata.TrayNotes
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
