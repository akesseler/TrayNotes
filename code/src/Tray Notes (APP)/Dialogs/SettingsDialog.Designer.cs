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

namespace plexdata.TrayNotes
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpBehaviour = new System.Windows.Forms.GroupBox();
            this.valDiscardData = new System.Windows.Forms.CheckBox();
            this.valPasteDate = new System.Windows.Forms.CheckBox();
            this.valPasteLabel = new System.Windows.Forms.CheckBox();
            this.grpAppearance = new System.Windows.Forms.GroupBox();
            this.valButtonText = new System.Windows.Forms.CheckBox();
            this.valLargeIcons = new System.Windows.Forms.CheckBox();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpShortcuts = new System.Windows.Forms.GroupBox();
            this.valAddStartup = new System.Windows.Forms.CheckBox();
            this.valAddDesktop = new System.Windows.Forms.CheckBox();
            this.valBackColor = new plexdata.Controls.ColorComboBox();
            this.grpBehaviour.SuspendLayout();
            this.grpAppearance.SuspendLayout();
            this.grpShortcuts.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
            this.btnAccept.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAccept.Location = new System.Drawing.Point(69, 287);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 25);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "&OK";
            this.btnAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccept.Click += new System.EventHandler(this.OnAcceptButtonClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(150, 287);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // grpBehaviour
            // 
            this.grpBehaviour.Controls.Add(this.valDiscardData);
            this.grpBehaviour.Controls.Add(this.valPasteDate);
            this.grpBehaviour.Controls.Add(this.valPasteLabel);
            this.grpBehaviour.Location = new System.Drawing.Point(12, 12);
            this.grpBehaviour.Name = "grpBehaviour";
            this.grpBehaviour.Size = new System.Drawing.Size(213, 92);
            this.grpBehaviour.TabIndex = 0;
            this.grpBehaviour.TabStop = false;
            this.grpBehaviour.Text = "Behaviour";
            // 
            // valDiscardData
            // 
            this.valDiscardData.AutoSize = true;
            this.valDiscardData.Location = new System.Drawing.Point(6, 69);
            this.valDiscardData.Name = "valDiscardData";
            this.valDiscardData.Size = new System.Drawing.Size(130, 17);
            this.valDiscardData.TabIndex = 2;
            this.valDiscardData.Text = "Discard all data at exit";
            this.toolTip.SetToolTip(this.valDiscardData, "Enable to allow discarding of all not saved changes as soon as \r\nthe program term" +
                    "inates. This option is very useful especially \r\nduring a system shutdown.");
            this.valDiscardData.UseVisualStyleBackColor = true;
            // 
            // valPasteDate
            // 
            this.valPasteDate.AutoSize = true;
            this.valPasteDate.Location = new System.Drawing.Point(6, 46);
            this.valPasteDate.Name = "valPasteDate";
            this.valPasteDate.Size = new System.Drawing.Size(201, 17);
            this.valPasteDate.TabIndex = 1;
            this.valPasteDate.Text = "Include date and time in pasting label";
            this.toolTip.SetToolTip(this.valPasteDate, "Enable to include date and time information in the pasting label.\r\nDisable to sup" +
                    "press adding of date and time information.");
            this.valPasteDate.UseVisualStyleBackColor = true;
            // 
            // valPasteLabel
            // 
            this.valPasteLabel.AutoSize = true;
            this.valPasteLabel.Location = new System.Drawing.Point(6, 21);
            this.valPasteLabel.Name = "valPasteLabel";
            this.valPasteLabel.Size = new System.Drawing.Size(136, 17);
            this.valPasteLabel.TabIndex = 0;
            this.valPasteLabel.Text = "Use label when pasting";
            this.toolTip.SetToolTip(this.valPasteLabel, "Enable to add an additional label as soon as data pasting \r\ntakes place. Disable " +
                    "to suppress this additional label.");
            this.valPasteLabel.UseVisualStyleBackColor = true;
            this.valPasteLabel.CheckedChanged += new System.EventHandler(this.OnPasteLabelCheckedChanged);
            // 
            // grpAppearance
            // 
            this.grpAppearance.Controls.Add(this.valButtonText);
            this.grpAppearance.Controls.Add(this.valLargeIcons);
            this.grpAppearance.Controls.Add(this.lblBackColor);
            this.grpAppearance.Controls.Add(this.valBackColor);
            this.grpAppearance.Location = new System.Drawing.Point(12, 110);
            this.grpAppearance.Name = "grpAppearance";
            this.grpAppearance.Size = new System.Drawing.Size(213, 92);
            this.grpAppearance.TabIndex = 1;
            this.grpAppearance.TabStop = false;
            this.grpAppearance.Text = "Appearance";
            // 
            // valButtonText
            // 
            this.valButtonText.AutoSize = true;
            this.valButtonText.Location = new System.Drawing.Point(9, 69);
            this.valButtonText.Name = "valButtonText";
            this.valButtonText.Size = new System.Drawing.Size(141, 17);
            this.valButtonText.TabIndex = 3;
            this.valButtonText.Text = "Show toolbar button text";
            this.toolTip.SetToolTip(this.valButtonText, "Enable to display the text of each toolbar button. This option \r\ncan also be adju" +
                    "sted by right-clicking the toolbar.");
            this.valButtonText.UseVisualStyleBackColor = true;
            // 
            // valLargeIcons
            // 
            this.valLargeIcons.AutoSize = true;
            this.valLargeIcons.Location = new System.Drawing.Point(9, 46);
            this.valLargeIcons.Name = "valLargeIcons";
            this.valLargeIcons.Size = new System.Drawing.Size(134, 17);
            this.valLargeIcons.TabIndex = 2;
            this.valLargeIcons.Text = "Use toolbar large icons";
            this.toolTip.SetToolTip(this.valLargeIcons, "Enable to use large toolbar icons and disable to use small icons \r\ninstead. This " +
                    "option can also be adjusted by right-clicking the \r\ntoolbar.");
            this.valLargeIcons.UseVisualStyleBackColor = true;
            // 
            // lblBackColor
            // 
            this.lblBackColor.AutoSize = true;
            this.lblBackColor.Location = new System.Drawing.Point(6, 22);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(68, 13);
            this.lblBackColor.TabIndex = 0;
            this.lblBackColor.Text = "Background:";
            // 
            // grpShortcuts
            // 
            this.grpShortcuts.Controls.Add(this.valAddDesktop);
            this.grpShortcuts.Controls.Add(this.valAddStartup);
            this.grpShortcuts.Location = new System.Drawing.Point(12, 208);
            this.grpShortcuts.Name = "grpShortcuts";
            this.grpShortcuts.Size = new System.Drawing.Size(213, 73);
            this.grpShortcuts.TabIndex = 2;
            this.grpShortcuts.TabStop = false;
            this.grpShortcuts.Text = "Shortcuts";
            // 
            // valAddStartup
            // 
            this.valAddStartup.AutoSize = true;
            this.valAddStartup.Location = new System.Drawing.Point(6, 19);
            this.valAddStartup.Name = "valAddStartup";
            this.valAddStartup.Size = new System.Drawing.Size(182, 17);
            this.valAddStartup.TabIndex = 0;
            this.valAddStartup.Text = "Add shortcut to the Startup menu";
            this.toolTip.SetToolTip(this.valAddStartup, "Add a program shortcut to the Startup menu so that an \r\nautomatic starting on Win" +
                    "dows startup becomes possible.");
            this.valAddStartup.UseVisualStyleBackColor = true;
            this.valAddStartup.CheckedChanged += new System.EventHandler(this.OnStartupShortcutCheckedChanged);
            // 
            // valAddDesktop
            // 
            this.valAddDesktop.AutoSize = true;
            this.valAddDesktop.Location = new System.Drawing.Point(6, 42);
            this.valAddDesktop.Name = "valAddDesktop";
            this.valAddDesktop.Size = new System.Drawing.Size(159, 17);
            this.valAddDesktop.TabIndex = 1;
            this.valAddDesktop.Text = "Add shortcut to the Desktop";
            this.toolTip.SetToolTip(this.valAddDesktop, "Add a program shortcut to your Desktop \r\nso that the program can easily be starte" +
                    "d.");
            this.valAddDesktop.UseVisualStyleBackColor = true;
            this.valAddDesktop.CheckedChanged += new System.EventHandler(this.OnDesktopShortcutCheckedChanged);
            // 
            // valBackColor
            // 
            this.valBackColor.FormattingEnabled = true;
            this.valBackColor.Location = new System.Drawing.Point(80, 19);
            this.valBackColor.Name = "valBackColor";
            this.valBackColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.valBackColor.Size = new System.Drawing.Size(127, 21);
            this.valBackColor.TabIndex = 1;
            this.toolTip.SetToolTip(this.valBackColor, "Choose a proper color to be used as text box background.");
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(237, 324);
            this.Controls.Add(this.grpShortcuts);
            this.Controls.Add(this.grpAppearance);
            this.Controls.Add(this.grpBehaviour);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.grpBehaviour.ResumeLayout(false);
            this.grpBehaviour.PerformLayout();
            this.grpAppearance.ResumeLayout(false);
            this.grpAppearance.PerformLayout();
            this.grpShortcuts.ResumeLayout(false);
            this.grpShortcuts.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private plexdata.Controls.ColorComboBox valBackColor;
        private System.Windows.Forms.GroupBox grpBehaviour;
        private System.Windows.Forms.GroupBox grpAppearance;
        private System.Windows.Forms.CheckBox valDiscardData;
        private System.Windows.Forms.CheckBox valPasteDate;
        private System.Windows.Forms.CheckBox valPasteLabel;
        private System.Windows.Forms.Label lblBackColor;
        private System.Windows.Forms.CheckBox valButtonText;
        private System.Windows.Forms.CheckBox valLargeIcons;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox grpShortcuts;
        private System.Windows.Forms.CheckBox valAddDesktop;
        private System.Windows.Forms.CheckBox valAddStartup;
    }
}