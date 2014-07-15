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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Steam overlay");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Saitek X52 Pro display");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Hot keys");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("War Thunder");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Network");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Mouse");
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.settingsTree1 = new mycontrol.SettingsTree();
            this.settingsPage2 = new mycontrol.SettingsPage();
            this.checkAskSave = new System.Windows.Forms.CheckBox();
            this.checkSaveOnExit = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkExclusiveDI = new System.Windows.Forms.CheckBox();
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
            this.settingsPage6 = new mycontrol.SettingsPage();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.settingsPage7 = new mycontrol.SettingsPage();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.checkBoxServer = new System.Windows.Forms.CheckBox();
            this.settingsPage1 = new mycontrol.SettingsPage();
            this.settingsPage8 = new mycontrol.SettingsPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.trackBarScroll = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.trackBarMouseY = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.trackBarMouseX = new System.Windows.Forms.TrackBar();
            this.labelVjoyNumber = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.settingsTree1.SplitContainer)).BeginInit();
            this.settingsTree1.SplitContainer.Panel2.SuspendLayout();
            this.settingsPage2.SuspendLayout();
            this.settingsPage3.SuspendLayout();
            this.settingsPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.settingsPage5.SuspendLayout();
            this.settingsPage6.SuspendLayout();
            this.settingsPage7.SuspendLayout();
            this.settingsPage8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMouseY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMouseX)).BeginInit();
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
            treeNode1.Text = "General";
            this.settingsTree1.CurrentNode = treeNode1;
            this.settingsTree1.CurrentPage = this.settingsPage2;
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
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage6);
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage7);
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage1);
            this.settingsTree1.SplitContainer.Panel2.Controls.Add(this.settingsPage8);
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
            treeNode1,
            treeNode4,
            treeNode7,
            treeNode6,
            treeNode3,
            treeNode2,
            treeNode5});
            this.settingsTree1.TreeView.Size = new System.Drawing.Size(148, 269);
            this.settingsTree1.TreeView.Sorted = true;
            this.settingsTree1.TreeView.TabIndex = 0;
            this.settingsTree1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.settingsTree1_KeyDown);
            // 
            // settingsPage2
            // 
            this.settingsPage2.Controls.Add(this.label13);
            this.settingsPage2.Controls.Add(this.textBox3);
            this.settingsPage2.Controls.Add(this.labelVjoyNumber);
            this.settingsPage2.Controls.Add(this.checkAskSave);
            this.settingsPage2.Controls.Add(this.checkSaveOnExit);
            this.settingsPage2.Controls.Add(this.label8);
            this.settingsPage2.Controls.Add(this.label7);
            this.settingsPage2.Controls.Add(this.button1);
            this.settingsPage2.Controls.Add(this.checkExclusiveDI);
            this.settingsPage2.Controls.Add(this.checkBox3);
            this.settingsPage2.Controls.Add(this.checkBox2);
            this.settingsPage2.Controls.Add(this.checkBox1);
            this.settingsPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage2.isActive = true;
            this.settingsPage2.Location = new System.Drawing.Point(0, 0);
            this.settingsPage2.Name = "settingsPage2";
            this.settingsPage2.ParentNode = treeNode1;
            this.settingsPage2.Size = new System.Drawing.Size(322, 269);
            this.settingsPage2.TabIndex = 1;
            // 
            // checkAskSave
            // 
            this.checkAskSave.AutoSize = true;
            this.checkAskSave.Checked = global::JoystickCurves.Properties.Settings.Default.globalAskSave;
            this.checkAskSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAskSave.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::JoystickCurves.Properties.Settings.Default, "globalAskSave", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkAskSave.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::JoystickCurves.Properties.Settings.Default, "globalSaveOnExit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkAskSave.Enabled = global::JoystickCurves.Properties.Settings.Default.globalSaveOnExit;
            this.checkAskSave.Location = new System.Drawing.Point(12, 105);
            this.checkAskSave.Name = "checkAskSave";
            this.checkAskSave.Size = new System.Drawing.Size(155, 17);
            this.checkAskSave.TabIndex = 9;
            this.checkAskSave.Text = "Ask to save settings on exit";
            this.checkAskSave.UseVisualStyleBackColor = true;
            // 
            // checkSaveOnExit
            // 
            this.checkSaveOnExit.AutoSize = true;
            this.checkSaveOnExit.Checked = global::JoystickCurves.Properties.Settings.Default.globalSaveOnExit;
            this.checkSaveOnExit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSaveOnExit.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::JoystickCurves.Properties.Settings.Default, "globalSaveOnExit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkSaveOnExit.Location = new System.Drawing.Point(12, 81);
            this.checkSaveOnExit.Name = "checkSaveOnExit";
            this.checkSaveOnExit.Size = new System.Drawing.Size(124, 17);
            this.checkSaveOnExit.TabIndex = 8;
            this.checkSaveOnExit.Text = "Save settings on exit";
            this.checkSaveOnExit.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(11, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(225, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "Could be useful when you set up axis controls in game";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(11, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "Enable this to suppress normal joystick. ";
            this.label7.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Reset Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkExclusiveDI
            // 
            this.checkExclusiveDI.AutoSize = true;
            this.checkExclusiveDI.Checked = global::JoystickCurves.Properties.Settings.Default.exclusiveDirectInput;
            this.checkExclusiveDI.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::JoystickCurves.Properties.Settings.Default, "exclusiveDirectInput", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkExclusiveDI.Enabled = false;
            this.checkExclusiveDI.Location = new System.Drawing.Point(12, 168);
            this.checkExclusiveDI.Name = "checkExclusiveDI";
            this.checkExclusiveDI.Size = new System.Drawing.Size(163, 17);
            this.checkExclusiveDI.TabIndex = 5;
            this.checkExclusiveDI.Text = "Exclusive DirectInput access";
            this.checkExclusiveDI.UseVisualStyleBackColor = true;
            this.checkExclusiveDI.Visible = false;
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
            treeNode2.Name = "";
            treeNode2.Text = "Steam overlay";
            this.settingsPage3.ParentNode = treeNode2;
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
            treeNode3.Name = "";
            treeNode3.Text = "Saitek X52 Pro display";
            this.settingsPage4.ParentNode = treeNode3;
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
            this.listHotKeys.Enabled = false;
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
            this.checkBoxHotKey.Enabled = false;
            this.checkBoxHotKey.Location = new System.Drawing.Point(13, 209);
            this.checkBoxHotKey.Name = "checkBoxHotKey";
            this.checkBoxHotKey.Size = new System.Drawing.Size(55, 23);
            this.checkBoxHotKey.TabIndex = 5;
            this.checkBoxHotKey.Text = "Hot Key";
            this.checkBoxHotKey.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(10, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(271, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Select an action and press Hot Key button to bind a key";
            // 
            // checkBoxHold
            // 
            this.checkBoxHold.AutoSize = true;
            this.checkBoxHold.Enabled = false;
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
            this.valueInput.Enabled = false;
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
            // settingsPage7
            // 
            this.settingsPage7.Controls.Add(this.label9);
            this.settingsPage7.Controls.Add(this.textBoxServerIP);
            this.settingsPage7.Controls.Add(this.checkBoxServer);
            this.settingsPage7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage7.isActive = true;
            this.settingsPage7.Location = new System.Drawing.Point(0, 0);
            this.settingsPage7.Name = "settingsPage7";
            treeNode6.Name = "";
            treeNode6.Text = "Network";
            this.settingsPage7.ParentNode = treeNode6;
            this.settingsPage7.Size = new System.Drawing.Size(322, 269);
            this.settingsPage7.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Port (TCP):";
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JoystickCurves.Properties.Settings.Default, "joystickServerPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxServerIP.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::JoystickCurves.Properties.Settings.Default, "enableJoystickServer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxServerIP.Enabled = global::JoystickCurves.Properties.Settings.Default.enableJoystickServer;
            this.textBoxServerIP.Location = new System.Drawing.Point(79, 35);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.Size = new System.Drawing.Size(42, 20);
            this.textBoxServerIP.TabIndex = 1;
            this.textBoxServerIP.Text = global::JoystickCurves.Properties.Settings.Default.joystickServerPort;
            // 
            // checkBoxServer
            // 
            this.checkBoxServer.AutoSize = true;
            this.checkBoxServer.Checked = global::JoystickCurves.Properties.Settings.Default.enableJoystickServer;
            this.checkBoxServer.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::JoystickCurves.Properties.Settings.Default, "enableJoystickServer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxServer.Location = new System.Drawing.Point(17, 12);
            this.checkBoxServer.Name = "checkBoxServer";
            this.checkBoxServer.Size = new System.Drawing.Size(155, 17);
            this.checkBoxServer.TabIndex = 0;
            this.checkBoxServer.Text = "Enable joystick state server";
            this.checkBoxServer.UseVisualStyleBackColor = true;
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
            // settingsPage8
            // 
            this.settingsPage8.Controls.Add(this.groupBox1);
            this.settingsPage8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPage8.isActive = true;
            this.settingsPage8.Location = new System.Drawing.Point(0, 0);
            this.settingsPage8.Name = "settingsPage8";
            treeNode7.Name = "";
            treeNode7.Text = "Mouse";
            this.settingsPage8.ParentNode = treeNode7;
            this.settingsPage8.Size = new System.Drawing.Size(322, 269);
            this.settingsPage8.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.trackBarScroll);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.trackBarMouseY);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.trackBarMouseX);
            this.groupBox1.Location = new System.Drawing.Point(7, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 150);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sensitivity";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 106);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Throttle (Scroll):";
            // 
            // trackBarScroll
            // 
            this.trackBarScroll.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::JoystickCurves.Properties.Settings.Default, "mouseSensScroll", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBarScroll.Location = new System.Drawing.Point(90, 103);
            this.trackBarScroll.Maximum = 1000;
            this.trackBarScroll.Minimum = 1;
            this.trackBarScroll.Name = "trackBarScroll";
            this.trackBarScroll.Size = new System.Drawing.Size(211, 45);
            this.trackBarScroll.SmallChange = 10;
            this.trackBarScroll.TabIndex = 6;
            this.trackBarScroll.TickFrequency = 25;
            this.trackBarScroll.Value = global::JoystickCurves.Properties.Settings.Default.mouseSensScroll;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(37, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Pitch (Y):";
            // 
            // trackBarMouseY
            // 
            this.trackBarMouseY.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::JoystickCurves.Properties.Settings.Default, "mouseSensY", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBarMouseY.Location = new System.Drawing.Point(90, 61);
            this.trackBarMouseY.Maximum = 10000;
            this.trackBarMouseY.Minimum = 1;
            this.trackBarMouseY.Name = "trackBarMouseY";
            this.trackBarMouseY.Size = new System.Drawing.Size(211, 45);
            this.trackBarMouseY.SmallChange = 10;
            this.trackBarMouseY.TabIndex = 4;
            this.trackBarMouseY.TickFrequency = 250;
            this.trackBarMouseY.Value = global::JoystickCurves.Properties.Settings.Default.mouseSensY;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Roll (X):";
            // 
            // trackBarMouseX
            // 
            this.trackBarMouseX.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::JoystickCurves.Properties.Settings.Default, "mouseSensX", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBarMouseX.Location = new System.Drawing.Point(90, 20);
            this.trackBarMouseX.Maximum = 10000;
            this.trackBarMouseX.Minimum = 1;
            this.trackBarMouseX.Name = "trackBarMouseX";
            this.trackBarMouseX.Size = new System.Drawing.Size(211, 45);
            this.trackBarMouseX.SmallChange = 10;
            this.trackBarMouseX.TabIndex = 2;
            this.trackBarMouseX.TickFrequency = 250;
            this.trackBarMouseX.Value = global::JoystickCurves.Properties.Settings.Default.mouseSensX;
            // 
            // labelVjoyNumber
            // 
            this.labelVjoyNumber.AutoSize = true;
            this.labelVjoyNumber.Location = new System.Drawing.Point(10, 139);
            this.labelVjoyNumber.Name = "labelVjoyNumber";
            this.labelVjoyNumber.Size = new System.Drawing.Size(108, 13);
            this.labelVjoyNumber.TabIndex = 10;
            this.labelVjoyNumber.Text = "Virtual joysticks max#";
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JoystickCurves.Properties.Settings.Default, "generalMaxVjoyNum", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox3.Location = new System.Drawing.Point(124, 136);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(43, 20);
            this.textBox3.TabIndex = 11;
            this.textBox3.Text = global::JoystickCurves.Properties.Settings.Default.generalMaxVjoyNum.ToString();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Enabled = false;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(173, 140);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 12);
            this.label13.TabIndex = 12;
            this.label13.Text = "0 - unlimited";
            this.label13.Visible = false;
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
            this.settingsPage2.ResumeLayout(false);
            this.settingsPage2.PerformLayout();
            this.settingsPage3.ResumeLayout(false);
            this.settingsPage3.PerformLayout();
            this.settingsPage4.ResumeLayout(false);
            this.settingsPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.settingsPage5.ResumeLayout(false);
            this.settingsPage5.PerformLayout();
            this.settingsPage6.ResumeLayout(false);
            this.settingsPage6.PerformLayout();
            this.settingsPage7.ResumeLayout(false);
            this.settingsPage7.PerformLayout();
            this.settingsPage8.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMouseY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMouseX)).EndInit();
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkExclusiveDI;
        private mycontrol.SettingsPage settingsPage7;
        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.CheckBox checkBoxServer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkSaveOnExit;
        private System.Windows.Forms.CheckBox checkAskSave;
        private mycontrol.SettingsPage settingsPage8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar trackBarScroll;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar trackBarMouseY;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar trackBarMouseX;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label labelVjoyNumber;
    }
}