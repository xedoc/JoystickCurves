namespace JoystickCurves
{
    partial class SettingsForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Saitek X52 Pro display");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("War Thunder");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Steam overlay");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Hot keys");
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.settingsTree1 = new mycontrol.SettingsTree();
            this.settingsPage6 = new mycontrol.SettingsPage();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.settingsPage2 = new mycontrol.SettingsPage();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.settingsPage3 = new mycontrol.SettingsPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.settingsPage4 = new mycontrol.SettingsPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.settingsPage5 = new mycontrol.SettingsPage();
            this.listHotKeys = new System.Windows.Forms.ListBox();
            this.checkBoxHotKey = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxHold = new System.Windows.Forms.CheckBox();
            this.valueInput = new JoystickCurves.ValueInput();
            this.settingsPage1 = new mycontrol.SettingsPage();
            ((System.ComponentModel.ISupportInitialize)(this.settingsTree1.SplitContainer)).BeginInit();
            this.settingsTree1.SplitContainer.Panel2.SuspendLayout();
            this.settingsPage6.SuspendLayout();
            this.settingsPage2.SuspendLayout();
            this.settingsPage3.SuspendLayout();
            this.settingsPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.settingsPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // settingsTree1
            // 
            treeNode1.Name = "";
            treeNode1.Text = "Saitek X52 Pro display";
            this.settingsTree1.CurrentNode = treeNode1;
            this.settingsTree1.CurrentPage = this.settingsPage4;
            this.settingsTree1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.settingsTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTree1.LabelEdit = false;
            this.settingsTree1.Location = new System.Drawing.Point(0, 0);
            this.settingsTree1.Name = "settingsTree1";
            this.settingsTree1.Size = new System.Drawing.Size(474, 269);
            // 
            // 
            // 
            this.settingsTree1.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTree1.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.settingsTree1.SplitContainer.Name = "splitContainer";
            // 
            // 
            // 
            this.settingsTree1.SplitContainer.Panel2.AllowDrop = true;
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage2);
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage3);
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage4);
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage5);
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage1);
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage6);
            this.settingsTree1.SplitContainer.Size = new System.Drawing.Size(474, 269);
            this.settingsTree1.SplitContainer.SplitterDistance = 148;
            this.settingsTree1.SplitContainer.TabIndex = 5;
            this.settingsTree1.TabIndex = 0;
            this.settingsTree1.TabStop = false;
            // 
            // 
            // 
            this.settingsTree1.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTree1.TreeView.HideSelection = false;
            this.settingsTree1.TreeView.Location = new System.Drawing.Point(0, 0);
            this.settingsTree1.TreeView.Name = "treeSettings";
            this.settingsTree1.TreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode4,
            treeNode1,
            treeNode3,
            treeNode5});
            this.settingsTree1.TreeView.Size = new System.Drawing.Size(148, 269);
            this.settingsTree1.TreeView.Sorted = true;
            this.settingsTree1.TreeView.TabIndex = 0;
            this.settingsTree1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.settingsTree1_KeyDown);
            // 
            // settingsPage6
            // 
            this.settingsPage6.Controls.Add(this.label6);
            this.settingsPage6.Controls.Add(this.checkBox6);
            this.settingsPage6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage6.isActive = true;
            this.settingsPage6.Location = new System.Drawing.Point(0, 0);
            this.settingsPage6.Name = "settingsPage6";
            treeNode5.Name = "";
            treeNode5.Text = "War Thunder";
            this.settingsPage6.ParentNode = treeNode5;
            this.settingsPage6.Size = new System.Drawing.Size(322, 269);
            this.settingsPage6.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(11, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(285, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "*Switch profile automatically on aircraft selection (online modes only)";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Checked = global::JoystickCurves.Properties.Settings.Default.warThunderTrackAircraft;
            this.checkBox6.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::JoystickCurves.Properties.Settings.Default, "warThunderTrackAircraft", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox6.Location = new System.Drawing.Point(14, 12);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(174, 17);
            this.checkBox6.TabIndex = 0;
            this.checkBox6.Text = "Track aircraft selection in game";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // settingsPage2
            // 
            this.settingsPage2.Controls.Add(this.button1);
            this.settingsPage2.Controls.Add(this.checkBox3);
            this.settingsPage2.Controls.Add(this.checkBox2);
            this.settingsPage2.Controls.Add(this.checkBox1);
            this.settingsPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage2.isActive = true;
            this.settingsPage2.Location = new System.Drawing.Point(0, 0);
            this.settingsPage2.Name = "settingsPage2";
            treeNode2.Name = "";
            treeNode2.Text = "General";
            this.settingsPage2.ParentNode = treeNode2;
            this.settingsPage2.Size = new System.Drawing.Size(322, 269);
            this.settingsPage2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Reset Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = global::JoystickCurves.Properties.Settings.Default.generalStartMinimized;
            this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::JoystickCurves.Properties.Settings.Default, "generalStartMinimized", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox3.Location = new System.Drawing.Point(12, 58);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(96, 17);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "Start minimized";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = global::JoystickCurves.Properties.Settings.Default.generalAutoStart;
            this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::JoystickCurves.Properties.Settings.Default, "generalAutoStart", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox2.Location = new System.Drawing.Point(12, 35);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(216, 17);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Start automatically when Windows starts";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = global::JoystickCurves.Properties.Settings.Default.generalMinimizeOnClose;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::JoystickCurves.Properties.Settings.Default, "generalMinimizeOnClose", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(12, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(109, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Minimize on close";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // settingsPage3
            // 
            this.settingsPage3.Controls.Add(this.label3);
            this.settingsPage3.Controls.Add(this.label2);
            this.settingsPage3.Controls.Add(this.label1);
            this.settingsPage3.Controls.Add(this.textBox2);
            this.settingsPage3.Controls.Add(this.checkBox4);
            this.settingsPage3.Controls.Add(this.textBox1);
            this.settingsPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage3.isActive = true;
            this.settingsPage3.Location = new System.Drawing.Point(0, 0);
            this.settingsPage3.Name = "settingsPage3";
            treeNode3.Name = "";
            treeNode3.Text = "Steam overlay";
            this.settingsPage3.ParentNode = treeNode3;
            this.settingsPage3.Size = new System.Drawing.Size(322, 269);
            this.settingsPage3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(339, 37);
            this.label3.TabIndex = 5;
            this.label3.Text = "* Add bot to the friend list of your main Steam account. Bot must have single fri" +
    "end only!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bot password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Steam bot login:";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::JoystickCurves.Properties.Settings.Default, "globalSteamEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JoystickCurves.Properties.Settings.Default, "steamPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.Enabled = global::JoystickCurves.Properties.Settings.Default.globalSteamEnable;
            this.textBox2.Location = new System.Drawing.Point(94, 66);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = global::JoystickCurves.Properties.Settings.Default.steamPassword;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = global::JoystickCurves.Properties.Settings.Default.globalSteamEnable;
            this.checkBox4.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::JoystickCurves.Properties.Settings.Default, "globalSteamEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox4.Location = new System.Drawing.Point(34, 12);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(201, 17);
            this.checkBox4.TabIndex = 4;
            this.checkBox4.Text = "Show current profile in Steam overlay";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::JoystickCurves.Properties.Settings.Default, "globalSteamEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JoystickCurves.Properties.Settings.Default, "steamLogin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Enabled = global::JoystickCurves.Properties.Settings.Default.globalSteamEnable;
            this.textBox1.Location = new System.Drawing.Point(94, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = global::JoystickCurves.Properties.Settings.Default.steamLogin;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // settingsPage4
            // 
            this.settingsPage4.Controls.Add(this.pictureBox1);
            this.settingsPage4.Controls.Add(this.label4);
            this.settingsPage4.Controls.Add(this.checkBox5);
            this.settingsPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage4.isActive = true;
            this.settingsPage4.Location = new System.Drawing.Point(0, 0);
            this.settingsPage4.Name = "settingsPage4";
            this.settingsPage4.ParentNode = treeNode1;
            this.settingsPage4.Size = new System.Drawing.Size(322, 269);
            this.settingsPage4.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(17, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(290, 159);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Scroll left wheel under display to switch a page:";
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Checked = global::JoystickCurves.Properties.Settings.Default.saitekX52ProEnable;
            this.checkBox5.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::JoystickCurves.Properties.Settings.Default, "saitekX52ProEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox5.Location = new System.Drawing.Point(17, 12);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(206, 17);
            this.checkBox5.TabIndex = 0;
            this.checkBox5.Text = "Enable Saitek X52 Pro display support";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // settingsPage5
            // 
            this.settingsPage5.Controls.Add(this.listHotKeys);
            this.settingsPage5.Controls.Add(this.checkBoxHotKey);
            this.settingsPage5.Controls.Add(this.label5);
            this.settingsPage5.Controls.Add(this.checkBoxHold);
            this.settingsPage5.Controls.Add(this.valueInput);
            this.settingsPage5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage5.isActive = true;
            this.settingsPage5.Location = new System.Drawing.Point(0, 0);
            this.settingsPage5.Name = "settingsPage5";
            treeNode4.Name = "";
            treeNode4.Text = "Hot keys";
            this.settingsPage5.ParentNode = treeNode4;
            this.settingsPage5.Size = new System.Drawing.Size(322, 269);
            this.settingsPage5.TabIndex = 4;
            // 
            // listHotKeys
            // 
            this.listHotKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listHotKeys.FormattingEnabled = true;
            this.listHotKeys.Location = new System.Drawing.Point(13, 25);
            this.listHotKeys.MultiColumn = true;
            this.listHotKeys.Name = "listHotKeys";
            this.listHotKeys.Size = new System.Drawing.Size(297, 173);
            this.listHotKeys.TabIndex = 7;
            this.listHotKeys.Resize += new System.EventHandler(this.listHotKeys_Resize);
            // 
            // checkBoxHotKey
            // 
            this.checkBoxHotKey.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxHotKey.AutoSize = true;
            this.checkBoxHotKey.Location = new System.Drawing.Point(13, 209);
            this.checkBoxHotKey.Name = "checkBoxHotKey";
            this.checkBoxHotKey.Size = new System.Drawing.Size(55, 23);
            this.checkBoxHotKey.TabIndex = 5;
            this.checkBoxHotKey.Text = "Hot Key";
            this.checkBoxHotKey.UseVisualStyleBackColor = true;
            this.checkBoxHotKey.CheckedChanged += new System.EventHandler(this.checkBoxHotKey_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(271, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Select an action and press Hot Key button to bind a key";
            // 
            // checkBoxHold
            // 
            this.checkBoxHold.AutoSize = true;
            this.checkBoxHold.Location = new System.Drawing.Point(13, 238);
            this.checkBoxHold.Name = "checkBoxHold";
            this.checkBoxHold.Size = new System.Drawing.Size(101, 17);
            this.checkBoxHold.TabIndex = 3;
            this.checkBoxHold.Text = "Hold to activate";
            this.checkBoxHold.UseVisualStyleBackColor = true;
            // 
            // valueInput
            // 
            this.valueInput.BoolValue = false;
            this.valueInput.FloatMax = 1F;
            this.valueInput.FloatMin = 0F;
            this.valueInput.FloatStep = 0.01F;
            this.valueInput.FloatValue = 0F;
            this.valueInput.IntMax = 0;
            this.valueInput.IntMin = 0;
            this.valueInput.IntStep = 0;
            this.valueInput.IntValue = 0;
            this.valueInput.Label = "Value";
            this.valueInput.Location = new System.Drawing.Point(91, 207);
            this.valueInput.Name = "valueInput";
            this.valueInput.Size = new System.Drawing.Size(219, 25);
            this.valueInput.StringValue = "";
            this.valueInput.TabIndex = 6;
            this.valueInput.Type = JoystickCurves.ValueInput.ValueType.Float;
            // 
            // settingsPage1
            // 
            this.settingsPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage1.isActive = true;
            this.settingsPage1.Location = new System.Drawing.Point(0, 0);
            this.settingsPage1.Name = "settingsPage1";
            this.settingsPage1.ParentNode = null;
            this.settingsPage1.Size = new System.Drawing.Size(267, 191);
            this.settingsPage1.TabIndex = 0;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 269);
            this.Controls.Add(this.settingsTree1);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.settingsTree1.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.settingsTree1.SplitContainer)).EndInit();
            this.settingsPage6.ResumeLayout(false);
            this.settingsPage6.PerformLayout();
            this.settingsPage2.ResumeLayout(false);
            this.settingsPage2.PerformLayout();
            this.settingsPage3.ResumeLayout(false);
            this.settingsPage3.PerformLayout();
            this.settingsPage4.ResumeLayout(false);
            this.settingsPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.settingsPage5.ResumeLayout(false);
            this.settingsPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private mycontrol.SettingsTree settingsTree1;
        private mycontrol.SettingsPage settingsPage1;
        private mycontrol.SettingsPage settingsPage2;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private mycontrol.SettingsPage settingsPage3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private mycontrol.SettingsPage settingsPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private mycontrol.SettingsPage settingsPage5;
        private System.Windows.Forms.CheckBox checkBoxHold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxHotKey;
        private ValueInput valueInput;
        private System.Windows.Forms.ListBox listHotKeys;
        private mycontrol.SettingsPage settingsPage6;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.Label label6;
    }
}