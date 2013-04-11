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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Steam overlay");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Saitek X52 Pro display");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("General");
            this.settingsTree1 = new mycontrol.SettingsTree();
            this.settingsPage4 = new mycontrol.SettingsPage();
            this.settingsPage1 = new mycontrol.SettingsPage();
            this.settingsPage2 = new mycontrol.SettingsPage();
            this.settingsPage3 = new mycontrol.SettingsPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.settingsTree1.SplitContainer)).BeginInit();
            this.settingsTree1.SplitContainer.Panel2.SuspendLayout();
            this.settingsPage4.SuspendLayout();
            this.settingsPage2.SuspendLayout();
            this.settingsPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTree1
            // 
            treeNode1.Name = "";
            treeNode1.Text = "Steam overlay";
            this.settingsTree1.CurrentNode = treeNode1;
            this.settingsTree1.CurrentPage = this.settingsPage3;
            this.settingsTree1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.settingsTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTree1.LabelEdit = false;
            this.settingsTree1.Location = new System.Drawing.Point(0, 0);
            this.settingsTree1.Name = "settingsTree1";
            this.settingsTree1.Size = new System.Drawing.Size(506, 191);
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
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage1);
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage4);
            this.settingsTree1.SplitContainer.Size = new System.Drawing.Size(506, 191);
            this.settingsTree1.SplitContainer.SplitterDistance = 150;
            this.settingsTree1.SplitContainer.TabIndex = 5;
            this.settingsTree1.TabIndex = 0;
            // 
            // 
            // 
            this.settingsTree1.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTree1.TreeView.HideSelection = false;
            this.settingsTree1.TreeView.Location = new System.Drawing.Point(0, 0);
            this.settingsTree1.TreeView.Name = "treeSettings";
            this.settingsTree1.TreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode1});
            this.settingsTree1.TreeView.Size = new System.Drawing.Size(150, 191);
            this.settingsTree1.TreeView.Sorted = true;
            this.settingsTree1.TreeView.TabIndex = 0;
            // 
            // settingsPage4
            // 
            this.settingsPage4.Controls.Add(this.checkBox5);
            this.settingsPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage4.isActive = true;
            this.settingsPage4.Location = new System.Drawing.Point(0, 0);
            this.settingsPage4.Name = "settingsPage4";
            treeNode3.Name = "";
            treeNode3.Text = "Saitek X52 Pro display";
            this.settingsPage4.ParentNode = treeNode3;
            this.settingsPage4.Size = new System.Drawing.Size(352, 191);
            this.settingsPage4.TabIndex = 3;
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
            // settingsPage2
            // 
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
            this.settingsPage2.Size = new System.Drawing.Size(352, 191);
            this.settingsPage2.TabIndex = 1;
            // 
            // settingsPage3
            // 
            this.settingsPage3.Controls.Add(this.label3);
            this.settingsPage3.Controls.Add(this.label2);
            this.settingsPage3.Controls.Add(this.label1);
            this.settingsPage3.Controls.Add(this.checkBox4);
            this.settingsPage3.Controls.Add(this.textBox2);
            this.settingsPage3.Controls.Add(this.textBox1);
            this.settingsPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage3.isActive = true;
            this.settingsPage3.Location = new System.Drawing.Point(0, 0);
            this.settingsPage3.Name = "settingsPage3";
            this.settingsPage3.ParentNode = treeNode1;
            this.settingsPage3.Size = new System.Drawing.Size(352, 191);
            this.settingsPage3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "* Add your main Steam account to friends of the bot";
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
            this.label1.Location = new System.Drawing.Point(31, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bot login:";
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
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
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::JoystickCurves.Properties.Settings.Default, "globalSteamEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JoystickCurves.Properties.Settings.Default, "steamPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.Enabled = global::JoystickCurves.Properties.Settings.Default.globalSteamEnable;
            this.textBox2.Location = new System.Drawing.Point(94, 66);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = global::JoystickCurves.Properties.Settings.Default.steamPassword;
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
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 191);
            this.Controls.Add(this.settingsTree1);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.settingsTree1.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.settingsTree1.SplitContainer)).EndInit();
            this.settingsPage4.ResumeLayout(false);
            this.settingsPage4.PerformLayout();
            this.settingsPage2.ResumeLayout(false);
            this.settingsPage2.PerformLayout();
            this.settingsPage3.ResumeLayout(false);
            this.settingsPage3.PerformLayout();
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
    }
}