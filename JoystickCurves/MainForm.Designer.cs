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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axisEditor1 = new JoystickCurves.AxisEditor();
            this.tabAddNew = new System.Windows.Forms.TabPage();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.joystickTester1 = new JoystickCurves.JoystickTester(this.components);
            this.aimReticle2 = new JoystickCurves.AimReticle();
            this.aimReticle1 = new JoystickCurves.AimReticle();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabAxis.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.joystickTester1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "<New profile...>"});
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
            this.tabAddNew.Click += new System.EventHandler(this.tabAddNew_Click);
            this.tabAddNew.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabAddNew_MouseClick);
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
            // joystickTester1
            // 
            this.joystickTester1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.joystickTester1.Controls.Add(this.aimReticle2);
            this.joystickTester1.Controls.Add(this.aimReticle1);
            this.joystickTester1.Controls.Add(this.label4);
            this.joystickTester1.Controls.Add(this.label3);
            this.joystickTester1.Controls.Add(this.label1);
            this.joystickTester1.Controls.Add(this.label6);
            this.joystickTester1.Controls.Add(this.label10);
            this.joystickTester1.Controls.Add(this.label5);
            this.joystickTester1.Location = new System.Drawing.Point(629, 54);
            this.joystickTester1.Margin = new System.Windows.Forms.Padding(30, 15, 10, 40);
            this.joystickTester1.Name = "joystickTester1";
            this.joystickTester1.Size = new System.Drawing.Size(290, 290);
            this.joystickTester1.TabIndex = 9;
            // 
            // aimReticle2
            // 
            this.aimReticle2.Location = new System.Drawing.Point(137, 122);
            this.aimReticle2.Name = "aimReticle2";
            this.aimReticle2.ReticleAppearance = JoystickCurves.Reticle.Cross;
            this.aimReticle2.Size = new System.Drawing.Size(20, 20);
            this.aimReticle2.TabIndex = 12;
            this.aimReticle2.X = 137;
            this.aimReticle2.Y = 122;
            // 
            // aimReticle1
            // 
            this.aimReticle1.Location = new System.Drawing.Point(137, 260);
            this.aimReticle1.Name = "aimReticle1";
            this.aimReticle1.ReticleAppearance = JoystickCurves.Reticle.VerticalLine;
            this.aimReticle1.Size = new System.Drawing.Size(20, 20);
            this.aimReticle1.TabIndex = 12;
            this.aimReticle1.X = 137;
            this.aimReticle1.Y = 260;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Lime;
            this.label4.Location = new System.Drawing.Point(4, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Yaw";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(5, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Roll";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(118, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pitch";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(3, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "0%";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(1, 135);
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
            this.label5.Location = new System.Drawing.Point(146, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "0%";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 500);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.joystickTester1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabAxis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.MaximumSize = new System.Drawing.Size(946, 527);
            this.MinimumSize = new System.Drawing.Size(946, 527);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Joystick Curves";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tabAxis.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.joystickTester1.ResumeLayout(false);
            this.joystickTester1.PerformLayout();
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
        private JoystickTester joystickTester1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private AimReticle aimReticle1;
        private AimReticle aimReticle2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

