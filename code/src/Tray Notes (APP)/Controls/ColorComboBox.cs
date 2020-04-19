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
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace plexdata.Controls
{
    public class ColorComboBox : ComboBox
    {
        private const string DefaultSelectorText = "More...";

        private int previousIndex = -1;
        private bool lockSelecting = false;
        private Dictionary<int, string> defaultColors = null;

        public ColorComboBox()
            : base()
        {
            base.DoubleBuffered = true;
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DropDownStyle = ComboBoxStyle.DropDownList;

            // NOTE: It is unfortunately strictly necessary to deal with RGB values 
            //       instead of Colors. Otherwise we get in trouble with comparing 
            //       colors. More clearly, this means that a color's equals-operator 
            //       firstly compares the name and secondly it compares the RGB value. 
            this.defaultColors = new Dictionary<int, string>();
            this.defaultColors.Add(Color.Black.ToArgb(), "01");
            this.defaultColors.Add(Color.White.ToArgb(), "02");
            this.defaultColors.Add(Color.Silver.ToArgb(), "03");
            this.defaultColors.Add(Color.Gray.ToArgb(), "04");
            this.defaultColors.Add(Color.Fuchsia.ToArgb(), "05");
            this.defaultColors.Add(Color.Purple.ToArgb(), "06");
            this.defaultColors.Add(Color.Blue.ToArgb(), "07");
            this.defaultColors.Add(Color.Navy.ToArgb(), "08");
            this.defaultColors.Add(Color.Aqua.ToArgb(), "09");
            this.defaultColors.Add(Color.Teal.ToArgb(), "10");
            this.defaultColors.Add(Color.Lime.ToArgb(), "11");
            this.defaultColors.Add(Color.Green.ToArgb(), "12");
            this.defaultColors.Add(Color.Yellow.ToArgb(), "13");
            this.defaultColors.Add(Color.Olive.ToArgb(), "14");
            this.defaultColors.Add(Color.Red.ToArgb(), "15");
            this.defaultColors.Add(Color.Maroon.ToArgb(), "16");

            foreach (int color in this.defaultColors.Keys)
            {
                base.Items.Add(color);
            }

            this.SelectorText = ColorComboBox.DefaultSelectorText;
            base.SelectedIndex = 0; // Select Black color.
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ComboBox.ObjectCollection Items { get { return base.Items; } }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DrawMode DrawMode { get { return base.DrawMode; } set { ;} }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ComboBoxStyle DropDownStyle { get { return base.DropDownStyle; } set { ;} }

        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Determines whether the label of the default colors is shown or not.")]
        public bool ShowLabels
        {
            get
            {
                return this.showLabels;
            }
            set
            {
                if (this.showLabels != value)
                {
                    this.showLabels = value;
                    this.Invalidate();
                }
            }
        }
        private bool showLabels = true;

        [Browsable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [DefaultValue(ColorComboBox.DefaultSelectorText)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Defines the text used as selector to open the external color selection dialog. If this " +
                     "text is null or empty the usage of the external color selection dialog is disabled.")]
        public string SelectorText
        {
            get
            {
                int count = this.Items.Count;
                if (count > 0 && this.Items[count - 1] is string)
                {
                    return this.Items[count - 1] as string;
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                int count = this.Items.Count;
                if (String.IsNullOrEmpty(value))
                {
                    if (count > 0 && this.Items[count - 1] is string)
                    {
                        this.Items.RemoveAt(count - 1);
                    }
                }
                else
                {
                    if (count > 0 && this.Items[count - 1] is string)
                    {
                        this.Items[count - 1] = value;
                    }
                    else
                    {
                        this.Items.Add(value);
                    }
                    this.Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Black")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Gets or sets the color currently selected in the control. " +
                     "If given color does not exist then it is added automatically.")]
        public Color SelectedColor
        {
            get
            {
                if (this.SelectedIndex != -1 && this.Items[this.SelectedIndex] is int)
                {
                    return Color.FromArgb((int)this.Items[this.SelectedIndex]);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("SelectedColor");
                }
            }
            set
            {
                if (!this.defaultColors.ContainsKey(value.ToArgb()) && !this.Items.Contains(value.ToArgb()))
                {
                    // BUGFIX: Value type must be an integer!
                    this.Items.Insert(0, value.ToArgb());
                }
                this.SelectedItem = value.ToArgb();
            }
        }

        protected override void OnMeasureItem(MeasureItemEventArgs args)
        {
            args.ItemHeight = this.ItemHeight;
            args.ItemWidth = this.ClientSize.Width;

            base.OnMeasureItem(args);
        }

        protected override void OnDrawItem(DrawItemEventArgs args)
        {
            base.OnDrawItem(args);

            args.DrawBackground();

            try
            {
                if (args.Index < this.Items.Count)
                {
                    if (this.Items[args.Index] is int)
                    {
                        TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding;
                        int color = (int)this.Items[args.Index];
                        Rectangle bounds = Rectangle.Inflate(args.Bounds, -1, -1);

                        using (Brush brush = new SolidBrush(Color.FromArgb(color)))
                        {
                            int cx = 0;
                            if (this.defaultColors.ContainsKey(color) && this.showLabels)
                            {
                                string label = this.defaultColors[color];

                                cx = TextRenderer.MeasureText(
                                    args.Graphics, label, args.Font, Size.Empty, flags).Width + 4;

                                TextRenderer.DrawText(
                                    args.Graphics, label, args.Font, bounds, args.ForeColor, flags);
                            }

                            bounds.X += cx;
                            bounds.Width -= cx;

                            args.Graphics.FillRectangle(brush, bounds);

                            bounds.Width -= 1;
                            bounds.Height -= 1;

                            args.Graphics.DrawRectangle(Pens.Black, bounds);
                        }
                    }
                    else if (this.Items[args.Index] is string)
                    {
                        TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.SingleLine;
                        if (args.Index == this.Items.Count - 1)
                        {
                            // Check if given font supports style Underline.
                            FontStyle style = args.Font.FontFamily.IsStyleAvailable(FontStyle.Underline) ? FontStyle.Underline : args.Font.Style;

                            using (Font font = new Font(args.Font, style))
                            {
                                Color color = Color.Blue; // Use typical link color as default.
                                if ((args.State & DrawItemState.Selected) == DrawItemState.Selected)
                                {
                                    color = args.ForeColor;
                                }

                                TextRenderer.DrawText(
                                    args.Graphics, this.Items[args.Index] as string,
                                    font, args.Bounds, color, flags);
                            }
                        }
                    }
                    // TODO: Paint other strings or objects.

                    if ((args.State & DrawItemState.Selected) != 0 && (args.State & DrawItemState.ComboBoxEdit) == 0)
                    {
                        ControlPaint.DrawFocusRectangle(args.Graphics, args.Bounds, this.BackColor, this.ForeColor);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs args)
        {
            if (this.lockSelecting) { return; }

            if (this.SelectedIndex == this.Items.Count - 1 && this.Items[this.SelectedIndex] is string)
            {
                try
                {
                    this.lockSelecting = true;
                    this.SelectedIndex = this.previousIndex;

                    ColorDialog dialog = new ColorDialog();
                    if (this.Items[this.SelectedIndex] is int)
                    {
                        dialog.Color = Color.FromArgb((int)this.Items[this.SelectedIndex]);
                    }

                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        if (!this.defaultColors.ContainsKey(dialog.Color.ToArgb()) && !this.Items.Contains(dialog.Color.ToArgb()))
                        {
                            this.Items.Insert(0, dialog.Color.ToArgb());
                        }

                        this.SelectedItem = dialog.Color.ToArgb();
                        this.previousIndex = this.SelectedIndex;
                        base.OnSelectedIndexChanged(args);
                    }
                }
                finally
                {
                    this.lockSelecting = false;
                }
            }
            else
            {
                this.previousIndex = this.SelectedIndex;
                base.OnSelectedIndexChanged(args);
            }
        }
    }
}