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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabAxis = new System.Windows.Forms.TabControl();
            this.contextMenuTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axisEditor1 = new JoystickCurves.AxisEditor();
            this.tabAddNew = new System.Windows.Forms.TabPage();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.joystickTester = new JoystickCurves.JoystickTester(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxTrackVirtual = new System.Windows.Forms.CheckBox();
            this.checkBoxTrackPhysical = new System.Windows.Forms.CheckBox();
            this.tabAxis.SuspendLayout();
            this.contextMenuTabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.joystickTester.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "<New profile...>",
            "Default"});
            this.comboBox1.Location = new System.Drawing.Point(54, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(269, 21);
            this.comboBox1.TabIndex = 4;
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
            this.tabAxis.Controls.Add(this.tabPage1);
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
            this.toolStripMenuItem1});
            this.contextMenuTabPage.Name = "contextMenuTabPage";
            this.contextMenuTabPage.Size = new System.Drawing.Size(158, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem1.Text = "Delete Axis State";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axisEditor1);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(599, 430);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Axis 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // axisEditor1
            // 
            this.axisEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axisEditor1.Location = new System.Drawing.Point(2, 6);
            this.axisEditor1.Name = "axisEditor1";
            this.axisEditor1.Size = new System.Drawing.Size(593, 421);
            this.axisEditor1.TabIndex = 0;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(329, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Hot key";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(410, 9);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(101, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Hold to activate";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // joystickTester
            // 
            this.joystickTester.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.joystickTester.Controls.Add(this.label4);
            this.joystickTester.Controls.Add(this.label3);
            this.joystickTester.Controls.Add(this.label1);
            this.joystickTester.Controls.Add(this.label6);
            this.joystickTester.Controls.Add(this.label10);
            this.joystickTester.Controls.Add(this.label5);
            this.joystickTester.HandleBounds = new System.Drawing.Rectangle(30, 15, 359, 359);
            this.joystickTester.Location = new System.Drawing.Point(629, 54);
            this.joystickTester.Margin = new System.Windows.Forms.Padding(40, 25, 20, 50);
            this.joystickTester.Name = "joystickTester";
            this.joystickTester.PhysicalHandleLocation = new System.Drawing.Point(209, 194);
            this.joystickTester.PhysicalRudderLocation = new System.Drawing.Point(209, 404);
            this.joystickTester.RudderBounds = new System.Drawing.Rectangle(30, 404, 359, 20);
            this.joystickTester.Size = new System.Drawing.Size(440, 434);
            this.joystickTester.TabIndex = 9;
            this.joystickTester.VirtualHandleLocation = new System.Drawing.Point(0, 0);
            this.joystickTester.VirtualRudderLocation = new System.Drawing.Point(0, 0);
            // 
            // label4
            // 
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.ForeColor = System.Drawing.Color.Lime;
            this.label4.Location = new System.Drawing.Point(4, 400);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Yaw";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(5, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Roll";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(186, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pitch";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(3, 416);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "0%";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(1, 206);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "0%";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(214, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "0%";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxTrackVirtual
            // 
            this.checkBoxTrackVirtual.AutoSize = true;
            this.checkBoxTrackVirtual.Checked = true;
            this.checkBoxTrackVirtual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTrackVirtual.Location = new System.Drawing.Point(708, 31);
            this.checkBoxTrackVirtual.Name = "checkBoxTrackVirtual";
            this.checkBoxTrackVirtual.Size = new System.Drawing.Size(123, 17);
            this.checkBoxTrackVirtual.TabIndex = 12;
            this.checkBoxTrackVirtual.Text = "Track virtual joystick";
            this.checkBoxTrackVirtual.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrackPhysical
            // 
            this.checkBoxTrackPhysical.AutoSize = true;
            this.checkBoxTrackPhysical.Checked = true;
            this.checkBoxTrackPhysical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTrackPhysical.Location = new System.Drawing.Point(866, 31);
            this.checkBoxTrackPhysical.Name = "checkBoxTrackPhysical";
            this.checkBoxTrackPhysical.Size = new System.Drawing.Size(133, 17);
            this.checkBoxTrackPhysical.TabIndex = 12;
            this.checkBoxTrackPhysical.Text = "Track physical joystick";
            this.checkBoxTrackPhysical.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 502);
            this.Controls.Add(this.checkBoxTrackPhysical);
            this.Controls.Add(this.checkBoxTrackVirtual);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.joystickTester);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabAxis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
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
            this.tabPage1.ResumeLayout(false);
            this.joystickTester.ResumeLayout(false);
            this.joystickTester.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabAxis;
        private System.Windows.Forms.TabPage tabPage1;
        private AxisEditor axisEditor1;
        private System.Windows.Forms.TabPage tabAddNew;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button button1;
        private JoystickTester joystickTester;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.CheckBox checkBoxTrackVirtual;
        private System.Windows.Forms.CheckBox checkBoxTrackPhysical;
    }
}

