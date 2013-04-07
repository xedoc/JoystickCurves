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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("General");
            this.settingsTree1 = new mycontrol.SettingsTree();
            this.settingsPage1 = new mycontrol.SettingsPage();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.settingsPage2 = new mycontrol.SettingsPage();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.settingsTree1.SplitContainer.Panel2.SuspendLayout();
            this.settingsPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTree1
            // 
            treeNode1.Name = "";
            treeNode1.Text = "General";
            this.settingsTree1.CurrentNode = treeNode1;
            this.settingsTree1.CurrentPage = this.settingsPage2;
            this.settingsTree1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.settingsTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsTree1.LabelEdit = false;
            this.settingsTree1.Location = new System.Drawing.Point(0, 0);
            this.settingsTree1.Name = "settingsTree1";
            this.settingsTree1.Size = new System.Drawing.Size(381, 191);
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
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage1);
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage2);
            this.settingsTree1.SplitContainer.Size = new System.Drawing.Size(381, 191);
            this.settingsTree1.SplitContainer.SplitterDistance = 110;
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
            treeNode1});
            this.settingsTree1.TreeView.Size = new System.Drawing.Size(110, 191);
            this.settingsTree1.TreeView.Sorted = true;
            this.settingsTree1.TreeView.TabIndex = 0;
            this.settingsTree1.Load += new System.EventHandler(this.settingsTree1_Load);
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
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
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
            this.settingsPage2.ParentNode = treeNode1;
            this.settingsPage2.Size = new System.Drawing.Size(267, 191);
            this.settingsPage2.TabIndex = 1;
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
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 191);
            this.Controls.Add(this.settingsTree1);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.settingsTree1.SplitContainer.Panel2.ResumeLayout(false);
            this.settingsPage2.ResumeLayout(false);
            this.settingsPage2.PerformLayout();
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
    }
}