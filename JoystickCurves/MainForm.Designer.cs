namespace JoystickCurves
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
            this.comboProfiles = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabAxis = new System.Windows.Forms.TabControl();
            this.contextMenuTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteAxis = new System.Windows.Forms.ToolStripMenuItem();
            this.tabAddNew = new System.Windows.Forms.TabPage();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.buttonHotKey = new System.Windows.Forms.Button();
            this.checkHoldActivate = new System.Windows.Forms.CheckBox();
            this.comboPhysPitch = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboPhysYaw = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboVirtYaw = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboVirtPitch = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.copyCurveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joystickTester = new JoystickCurves.JoystickTester(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelYawPercent = new System.Windows.Forms.Label();
            this.labelRollPercent = new System.Windows.Forms.Label();
            this.labelPitchPercent = new System.Windows.Forms.Label();
            this.tabAxis.SuspendLayout();
            this.contextMenuTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.joystickTester)).BeginInit();
            this.joystickTester.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboProfiles
            // 
            this.comboProfiles.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JoystickCurves.Properties.Settings.Default, "CurrentProfile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.comboProfiles.Enabled = false;
            this.comboProfiles.FormattingEnabled = true;
            this.comboProfiles.Items.AddRange(new object[] {
            "<New profile...>",
            "Default"});
            this.comboProfiles.Location = new System.Drawing.Point(54, 6);
            this.comboProfiles.Name = "comboProfiles";
            this.comboProfiles.Size = new System.Drawing.Size(269, 21);
            this.comboProfiles.TabIndex = 4;
            this.comboProfiles.Text = global::JoystickCurves.Properties.Settings.Default.CurrentProfile;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Profile:";
            // 
            // tabAxis
            // 
            this.tabAxis.ContextMenuStrip = this.contextMenuTabPage;
            this.tabAxis.Controls.Add(this.tabAddNew);
            this.tabAxis.ImageList = this.imageList;
            this.tabAxis.Location = new System.Drawing.Point(12, 31);
            this.tabAxis.Name = "tabAxis";
            this.tabAxis.SelectedIndex = 0;
            this.tabAxis.Size = new System.Drawing.Size(607, 457);
            this.tabAxis.TabIndex = 7;
            this.tabAxis.SelectedIndexChanged += new System.EventHandler(this.tabAxis_SelectedIndexChanged);
            // 
            // contextMenuTabPage
            // 
            this.contextMenuTabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCurveToToolStripMenuItem,
            this.deleteAxis});
            this.contextMenuTabPage.Name = "contextMenuTabPage";
            this.contextMenuTabPage.Size = new System.Drawing.Size(158, 48);
            this.contextMenuTabPage.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuTabPage_ItemClicked);
            // 
            // deleteAxis
            // 
            this.deleteAxis.Name = "deleteAxis";
            this.deleteAxis.Size = new System.Drawing.Size(157, 22);
            this.deleteAxis.Text = "Delete tab";
            // 
            // tabAddNew
            // 
            this.tabAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabAddNew.ImageIndex = 0;
            this.tabAddNew.Location = new System.Drawing.Point(4, 23);
            this.tabAddNew.Name = "tabAddNew";
            this.tabAddNew.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddNew.Size = new System.Drawing.Size(599, 430);
            this.tabAddNew.TabIndex = 1;
            this.tabAddNew.Text = "Add new";
            this.tabAddNew.UseVisualStyleBackColor = true;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.White;
            this.imageList.Images.SetKeyName(0, "plus.png");
            // 
            // buttonHotKey
            // 
            this.buttonHotKey.Enabled = false;
            this.buttonHotKey.Location = new System.Drawing.Point(329, 4);
            this.buttonHotKey.Name = "buttonHotKey";
            this.buttonHotKey.Size = new System.Drawing.Size(75, 23);
            this.buttonHotKey.TabIndex = 8;
            this.buttonHotKey.Text = "Hot key";
            this.buttonHotKey.UseVisualStyleBackColor = true;
            // 
            // checkHoldActivate
            // 
            this.checkHoldActivate.AutoSize = true;
            this.checkHoldActivate.Enabled = false;
            this.checkHoldActivate.Location = new System.Drawing.Point(410, 9);
            this.checkHoldActivate.Name = "checkHoldActivate";
            this.checkHoldActivate.Size = new System.Drawing.Size(101, 17);
            this.checkHoldActivate.TabIndex = 10;
            this.checkHoldActivate.Text = "Hold to activate";
            this.checkHoldActivate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkHoldActivate.UseVisualStyleBackColor = true;
            // 
            // comboPhysPitch
            // 
            this.comboPhysPitch.DropDownWidth = 400;
            this.comboPhysPitch.FormattingEnabled = true;
            this.comboPhysPitch.Location = new System.Drawing.Point(68, 19);
            this.comboPhysPitch.Name = "comboPhysPitch";
            this.comboPhysPitch.Size = new System.Drawing.Size(99, 21);
            this.comboPhysPitch.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Pitch/Roll:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboPhysYaw);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboPhysPitch);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(625, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 76);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Physical tester";
            // 
            // comboPhysYaw
            // 
            this.comboPhysYaw.DropDownWidth = 300;
            this.comboPhysYaw.FormattingEnabled = true;
            this.comboPhysYaw.Location = new System.Drawing.Point(68, 46);
            this.comboPhysYaw.Name = "comboPhysYaw";
            this.comboPhysYaw.Size = new System.Drawing.Size(99, 21);
            this.comboPhysYaw.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Yaw:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboVirtYaw);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.comboVirtPitch);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(808, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 76);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Virtual tester";
            // 
            // comboVirtYaw
            // 
            this.comboVirtYaw.DropDownWidth = 300;
            this.comboVirtYaw.FormattingEnabled = true;
            this.comboVirtYaw.Location = new System.Drawing.Point(68, 46);
            this.comboVirtYaw.Name = "comboVirtYaw";
            this.comboVirtYaw.Size = new System.Drawing.Size(99, 21);
            this.comboVirtYaw.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Yaw:";
            // 
            // comboVirtPitch
            // 
            this.comboVirtPitch.DropDownWidth = 300;
            this.comboVirtPitch.FormattingEnabled = true;
            this.comboVirtPitch.Location = new System.Drawing.Point(68, 19);
            this.comboVirtPitch.Name = "comboVirtPitch";
            this.comboVirtPitch.Size = new System.Drawing.Size(99, 21);
            this.comboVirtPitch.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Pitch/Roll:";
            // 
            // copyCurveToToolStripMenuItem
            // 
            this.copyCurveToToolStripMenuItem.Name = "copyCurveToToolStripMenuItem";
            this.copyCurveToToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.copyCurveToToolStripMenuItem.Text = "Copy curve to...";
            this.copyCurveToToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.copyCurveToToolStripMenuItem_DropDownItemClicked);
            // 
            // joystickTester
            // 
            this.joystickTester.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.joystickTester.Controls.Add(this.label4);
            this.joystickTester.Controls.Add(this.label3);
            this.joystickTester.Controls.Add(this.label1);
            this.joystickTester.Controls.Add(this.labelYawPercent);
            this.joystickTester.Controls.Add(this.labelRollPercent);
            this.joystickTester.Controls.Add(this.labelPitchPercent);
            this.joystickTester.HandleBounds = new System.Drawing.Rectangle(30, 15, 281, 281);
            this.joystickTester.Location = new System.Drawing.Point(625, 132);
            this.joystickTester.Margin = new System.Windows.Forms.Padding(40, 25, 20, 50);
            this.joystickTester.Name = "joystickTester";
            this.joystickTester.PhysicalHandleLocation = new System.Drawing.Point(170, 155);
            this.joystickTester.PhysicalRudderLocation = new System.Drawing.Point(170, 326);
            this.joystickTester.RudderBounds = new System.Drawing.Rectangle(30, 326, 281, 20);
            this.joystickTester.Size = new System.Drawing.Size(356, 356);
            this.joystickTester.TabIndex = 9;
            this.joystickTester.TabStop = false;
            this.joystickTester.VirtualHandleLocation = new System.Drawing.Point(170, 155);
            this.joystickTester.VirtualRudderLocation = new System.Drawing.Point(170, 326);
            // 
            // label4
            // 
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.ForeColor = System.Drawing.Color.Lime;
            this.label4.Location = new System.Drawing.Point(3, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Yaw";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(3, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Roll";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(149, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pitch";
            // 
            // labelYawPercent
            // 
            this.labelYawPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelYawPercent.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelYawPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            this.labelYawPercent.Location = new System.Drawing.Point(2, 334);
            this.labelYawPercent.Name = "labelYawPercent";
            this.labelYawPercent.Size = new System.Drawing.Size(28, 13);
            this.labelYawPercent.TabIndex = 10;
            this.labelYawPercent.Text = "0%";
            this.labelYawPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRollPercent
            // 
            this.labelRollPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelRollPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            this.labelRollPercent.Location = new System.Drawing.Point(-1, 163);
            this.labelRollPercent.Name = "labelRollPercent";
            this.labelRollPercent.Size = new System.Drawing.Size(37, 13);
            this.labelRollPercent.TabIndex = 10;
            this.labelRollPercent.Text = "0%";
            this.labelRollPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPitchPercent
            // 
            this.labelPitchPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelPitchPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            this.labelPitchPercent.Location = new System.Drawing.Point(177, 0);
            this.labelPitchPercent.Name = "labelPitchPercent";
            this.labelPitchPercent.Size = new System.Drawing.Size(37, 13);
            this.labelPitchPercent.TabIndex = 10;
            this.labelPitchPercent.Text = "0%";
            this.labelPitchPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 502);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkHoldActivate);
            this.Controls.Add(this.buttonHotKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabAxis);
            this.Controls.Add(this.comboProfiles);
            this.Controls.Add(this.joystickTester);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Joystick Curves";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tabAxis.ResumeLayout(false);
            this.contextMenuTabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.joystickTester)).EndInit();
            this.joystickTester.ResumeLayout(false);
            this.joystickTester.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboProfiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabAxis;
        private System.Windows.Forms.TabPage tabAddNew;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button buttonHotKey;
        private JoystickTester joystickTester;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelYawPercent;
        private System.Windows.Forms.Label labelRollPercent;
        private System.Windows.Forms.Label labelPitchPercent;
        private System.Windows.Forms.CheckBox checkHoldActivate;
        private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
        private System.Windows.Forms.ToolStripMenuItem deleteAxis;
        private System.Windows.Forms.ComboBox comboPhysPitch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboPhysYaw;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboVirtYaw;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboVirtPitch;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripMenuItem copyCurveToToolStripMenuItem;
    }
}

