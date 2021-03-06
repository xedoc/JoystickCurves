﻿namespace JoystickCurves
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
            this.copyCurveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetCurveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streightenUpCurveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAxis = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCurrentProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.responseModifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiplier = new System.Windows.Forms.ToolStripMenuItem();
            this.value = new System.Windows.Forms.ToolStripMenuItem();
            this.importExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabAddNew = new System.Windows.Forms.TabPage();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuTester = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.virtualDeviceLightGreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuVirtualDevices = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.physicalDeviceDarkGreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuPhysicalDevices = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.virtualPitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuAxisListVirtualY = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.virtualRollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuAxisListVirtualX = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.virtualYawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuAxisListVirtualRZ = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.physicalPitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuAxisListPhysY = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.physicalRollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuAxisListPhysX = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.physicalYawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuAxisListPhysRZ = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.profilesToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.checkBoxHotKey = new System.Windows.Forms.CheckBox();
            this.timerTest = new System.Windows.Forms.Timer(this.components);
            this.buttonSave = new System.Windows.Forms.Button();
            this.contextMenuAddTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newAxisTab = new System.Windows.Forms.ToolStripMenuItem();
            this.newMacroTab = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxBindAircraft = new System.Windows.Forms.CheckBox();
            this.labelCurrentAircraft = new System.Windows.Forms.Label();
            this.joystickTester = new JoystickCurves.JoystickTester(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelYawPercent = new System.Windows.Forms.Label();
            this.labelRollPercent = new System.Windows.Forms.Label();
            this.labelPitchPercent = new System.Windows.Forms.Label();
            this.tabAxis.SuspendLayout();
            this.contextMenuTabPage.SuspendLayout();
            this.contextMenuTester.SuspendLayout();
            this.contextMenuTray.SuspendLayout();
            this.contextMenuAddTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.joystickTester)).BeginInit();
            this.joystickTester.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboProfiles
            // 
            this.comboProfiles.FormattingEnabled = true;
            this.comboProfiles.Location = new System.Drawing.Point(54, 6);
            this.comboProfiles.Name = "comboProfiles";
            this.comboProfiles.Size = new System.Drawing.Size(269, 21);
            this.comboProfiles.TabIndex = 4;
            this.comboProfiles.SelectionChangeCommitted += new System.EventHandler(this.comboProfiles_SelectionChangeCommitted);
            this.comboProfiles.Enter += new System.EventHandler(this.comboProfiles_Enter);
            this.comboProfiles.Leave += new System.EventHandler(this.comboProfiles_Leave);
            this.comboProfiles.Validated += new System.EventHandler(this.comboProfiles_Validated);
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
            this.tabAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabAxis.ContextMenuStrip = this.contextMenuTabPage;
            this.tabAxis.Controls.Add(this.tabAddNew);
            this.tabAxis.ImageList = this.imageList;
            this.tabAxis.Location = new System.Drawing.Point(12, 31);
            this.tabAxis.Name = "tabAxis";
            this.tabAxis.SelectedIndex = 0;
            this.tabAxis.Size = new System.Drawing.Size(613, 380);
            this.tabAxis.TabIndex = 7;
            this.tabAxis.SelectedIndexChanged += new System.EventHandler(this.tabAxis_SelectedIndexChanged);
            this.tabAxis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabAxis_KeyDown);
            this.tabAxis.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tabAxis_KeyUp);
            // 
            // contextMenuTabPage
            // 
            this.contextMenuTabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCurveToToolStripMenuItem,
            this.resetCurveToolStripMenuItem,
            this.streightenUpCurveToolStripMenuItem,
            this.deleteAxis,
            this.deleteCurrentProfileToolStripMenuItem,
            this.responseModifierToolStripMenuItem,
            this.importExportToolStripMenuItem});
            this.contextMenuTabPage.Name = "contextMenuTabPage";
            this.contextMenuTabPage.Size = new System.Drawing.Size(186, 158);
            this.contextMenuTabPage.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuTabPage_ItemClicked);
            // 
            // copyCurveToToolStripMenuItem
            // 
            this.copyCurveToToolStripMenuItem.Name = "copyCurveToToolStripMenuItem";
            this.copyCurveToToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.copyCurveToToolStripMenuItem.Text = "Copy curve to...";
            this.copyCurveToToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.copyCurveToToolStripMenuItem_DropDownItemClicked);
            // 
            // resetCurveToolStripMenuItem
            // 
            this.resetCurveToolStripMenuItem.Name = "resetCurveToolStripMenuItem";
            this.resetCurveToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.resetCurveToolStripMenuItem.Text = "Reset curve";
            this.resetCurveToolStripMenuItem.Click += new System.EventHandler(this.resetCurveToolStripMenuItem_Click);
            // 
            // streightenUpCurveToolStripMenuItem
            // 
            this.streightenUpCurveToolStripMenuItem.Name = "streightenUpCurveToolStripMenuItem";
            this.streightenUpCurveToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.streightenUpCurveToolStripMenuItem.Text = "Streighten curve";
            this.streightenUpCurveToolStripMenuItem.Click += new System.EventHandler(this.streightenUpCurveToolStripMenuItem_Click);
            // 
            // deleteAxis
            // 
            this.deleteAxis.Name = "deleteAxis";
            this.deleteAxis.Size = new System.Drawing.Size(185, 22);
            this.deleteAxis.Text = "Delete tab";
            // 
            // deleteCurrentProfileToolStripMenuItem
            // 
            this.deleteCurrentProfileToolStripMenuItem.Name = "deleteCurrentProfileToolStripMenuItem";
            this.deleteCurrentProfileToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.deleteCurrentProfileToolStripMenuItem.Text = "Delete current profile";
            this.deleteCurrentProfileToolStripMenuItem.Click += new System.EventHandler(this.deleteCurrentProfileToolStripMenuItem_Click);
            // 
            // responseModifierToolStripMenuItem
            // 
            this.responseModifierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.multiplier,
            this.value});
            this.responseModifierToolStripMenuItem.Name = "responseModifierToolStripMenuItem";
            this.responseModifierToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.responseModifierToolStripMenuItem.Text = "Response modifier";
            // 
            // multiplier
            // 
            this.multiplier.Checked = true;
            this.multiplier.CheckOnClick = true;
            this.multiplier.CheckState = System.Windows.Forms.CheckState.Checked;
            this.multiplier.Name = "multiplier";
            this.multiplier.Size = new System.Drawing.Size(125, 22);
            this.multiplier.Text = "Multiplier";
            this.multiplier.CheckedChanged += new System.EventHandler(this.multiplerToolStripMenuItem_CheckedChanged);
            this.multiplier.Click += new System.EventHandler(this.multiplerToolStripMenuItem_Click);
            // 
            // value
            // 
            this.value.CheckOnClick = true;
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(125, 22);
            this.value.Text = "Value";
            this.value.CheckedChanged += new System.EventHandler(this.valueToolStripMenuItem_CheckedChanged);
            this.value.Click += new System.EventHandler(this.multiplerToolStripMenuItem_Click);
            // 
            // importExportToolStripMenuItem
            // 
            this.importExportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.importToolStripMenuItem});
            this.importExportToolStripMenuItem.Name = "importExportToolStripMenuItem";
            this.importExportToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.importExportToolStripMenuItem.Text = "Import/Export";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportProfileToolStripMenuItem,
            this.exportAxisToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // exportProfileToolStripMenuItem
            // 
            this.exportProfileToolStripMenuItem.Name = "exportProfileToolStripMenuItem";
            this.exportProfileToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exportProfileToolStripMenuItem.Text = "Export profile";
            this.exportProfileToolStripMenuItem.Click += new System.EventHandler(this.exportProfileToolStripMenuItem_Click);
            // 
            // exportAxisToolStripMenuItem
            // 
            this.exportAxisToolStripMenuItem.Name = "exportAxisToolStripMenuItem";
            this.exportAxisToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exportAxisToolStripMenuItem.Text = "Export axis";
            this.exportAxisToolStripMenuItem.Click += new System.EventHandler(this.exportAxisToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // tabAddNew
            // 
            this.tabAddNew.ContextMenuStrip = this.contextMenuTabPage;
            this.tabAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabAddNew.ImageIndex = 0;
            this.tabAddNew.Location = new System.Drawing.Point(4, 23);
            this.tabAddNew.Name = "tabAddNew";
            this.tabAddNew.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddNew.Size = new System.Drawing.Size(605, 353);
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
            // contextMenuTester
            // 
            this.contextMenuTester.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.virtualDeviceLightGreenToolStripMenuItem,
            this.physicalDeviceDarkGreenToolStripMenuItem,
            this.virtualPitchToolStripMenuItem,
            this.virtualRollToolStripMenuItem,
            this.virtualYawToolStripMenuItem,
            this.physicalPitchToolStripMenuItem,
            this.physicalRollToolStripMenuItem,
            this.physicalYawToolStripMenuItem});
            this.contextMenuTester.Name = "contextMenuTester";
            this.contextMenuTester.Size = new System.Drawing.Size(229, 180);
            this.contextMenuTester.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuTester_Closing);
            // 
            // virtualDeviceLightGreenToolStripMenuItem
            // 
            this.virtualDeviceLightGreenToolStripMenuItem.DropDown = this.contextMenuVirtualDevices;
            this.virtualDeviceLightGreenToolStripMenuItem.Name = "virtualDeviceLightGreenToolStripMenuItem";
            this.virtualDeviceLightGreenToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.virtualDeviceLightGreenToolStripMenuItem.Text = "Virtual device ( Light green )";
            this.virtualDeviceLightGreenToolStripMenuItem.Click += new System.EventHandler(this.testerRootMenuItem_Click);
            // 
            // contextMenuVirtualDevices
            // 
            this.contextMenuVirtualDevices.Name = "contextMenuVirtualDevices";
            this.contextMenuVirtualDevices.OwnerItem = this.virtualDeviceLightGreenToolStripMenuItem;
            this.contextMenuVirtualDevices.Size = new System.Drawing.Size(61, 4);
            this.contextMenuVirtualDevices.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuTester_Closing);
            this.contextMenuVirtualDevices.MouseDown += new System.Windows.Forms.MouseEventHandler(this.testerContextDevices_MouseDown);
            // 
            // physicalDeviceDarkGreenToolStripMenuItem
            // 
            this.physicalDeviceDarkGreenToolStripMenuItem.DropDown = this.contextMenuPhysicalDevices;
            this.physicalDeviceDarkGreenToolStripMenuItem.Name = "physicalDeviceDarkGreenToolStripMenuItem";
            this.physicalDeviceDarkGreenToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.physicalDeviceDarkGreenToolStripMenuItem.Text = "Physical device ( Dark green )";
            this.physicalDeviceDarkGreenToolStripMenuItem.Click += new System.EventHandler(this.testerRootMenuItem_Click);
            // 
            // contextMenuPhysicalDevices
            // 
            this.contextMenuPhysicalDevices.Name = "contextMenuVirtualDevices";
            this.contextMenuPhysicalDevices.OwnerItem = this.physicalDeviceDarkGreenToolStripMenuItem;
            this.contextMenuPhysicalDevices.Size = new System.Drawing.Size(61, 4);
            this.contextMenuPhysicalDevices.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuTester_Closing);
            this.contextMenuPhysicalDevices.MouseDown += new System.Windows.Forms.MouseEventHandler(this.testerContextDevices_MouseDown);
            // 
            // virtualPitchToolStripMenuItem
            // 
            this.virtualPitchToolStripMenuItem.DropDown = this.contextMenuAxisListVirtualY;
            this.virtualPitchToolStripMenuItem.Name = "virtualPitchToolStripMenuItem";
            this.virtualPitchToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.virtualPitchToolStripMenuItem.Text = "Virtual Pitch";
            this.virtualPitchToolStripMenuItem.Click += new System.EventHandler(this.testerRootMenuItem_Click);
            // 
            // contextMenuAxisListVirtualY
            // 
            this.contextMenuAxisListVirtualY.Name = "contextMenuVirtualDevices";
            this.contextMenuAxisListVirtualY.OwnerItem = this.virtualPitchToolStripMenuItem;
            this.contextMenuAxisListVirtualY.Size = new System.Drawing.Size(61, 4);
            this.contextMenuAxisListVirtualY.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuTester_Closing);
            this.contextMenuAxisListVirtualY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.testerContextDevices_MouseDown);
            // 
            // virtualRollToolStripMenuItem
            // 
            this.virtualRollToolStripMenuItem.DropDown = this.contextMenuAxisListVirtualX;
            this.virtualRollToolStripMenuItem.Name = "virtualRollToolStripMenuItem";
            this.virtualRollToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.virtualRollToolStripMenuItem.Text = "Virtual Roll";
            this.virtualRollToolStripMenuItem.Click += new System.EventHandler(this.testerRootMenuItem_Click);
            // 
            // contextMenuAxisListVirtualX
            // 
            this.contextMenuAxisListVirtualX.Name = "contextMenuVirtualDevices";
            this.contextMenuAxisListVirtualX.OwnerItem = this.virtualRollToolStripMenuItem;
            this.contextMenuAxisListVirtualX.Size = new System.Drawing.Size(61, 4);
            this.contextMenuAxisListVirtualX.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuTester_Closing);
            this.contextMenuAxisListVirtualX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.testerContextDevices_MouseDown);
            // 
            // virtualYawToolStripMenuItem
            // 
            this.virtualYawToolStripMenuItem.DropDown = this.contextMenuAxisListVirtualRZ;
            this.virtualYawToolStripMenuItem.Name = "virtualYawToolStripMenuItem";
            this.virtualYawToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.virtualYawToolStripMenuItem.Text = "Virtual Yaw";
            this.virtualYawToolStripMenuItem.Click += new System.EventHandler(this.testerRootMenuItem_Click);
            // 
            // contextMenuAxisListVirtualRZ
            // 
            this.contextMenuAxisListVirtualRZ.Name = "contextMenuVirtualDevices";
            this.contextMenuAxisListVirtualRZ.OwnerItem = this.virtualYawToolStripMenuItem;
            this.contextMenuAxisListVirtualRZ.Size = new System.Drawing.Size(61, 4);
            this.contextMenuAxisListVirtualRZ.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuTester_Closing);
            this.contextMenuAxisListVirtualRZ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.testerContextDevices_MouseDown);
            // 
            // physicalPitchToolStripMenuItem
            // 
            this.physicalPitchToolStripMenuItem.DropDown = this.contextMenuAxisListPhysY;
            this.physicalPitchToolStripMenuItem.Name = "physicalPitchToolStripMenuItem";
            this.physicalPitchToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.physicalPitchToolStripMenuItem.Text = "Physical Pitch";
            this.physicalPitchToolStripMenuItem.Click += new System.EventHandler(this.testerRootMenuItem_Click);
            // 
            // contextMenuAxisListPhysY
            // 
            this.contextMenuAxisListPhysY.Name = "contextMenuVirtualDevices";
            this.contextMenuAxisListPhysY.OwnerItem = this.physicalPitchToolStripMenuItem;
            this.contextMenuAxisListPhysY.Size = new System.Drawing.Size(61, 4);
            this.contextMenuAxisListPhysY.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuTester_Closing);
            this.contextMenuAxisListPhysY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.testerContextDevices_MouseDown);
            // 
            // physicalRollToolStripMenuItem
            // 
            this.physicalRollToolStripMenuItem.DropDown = this.contextMenuAxisListPhysX;
            this.physicalRollToolStripMenuItem.Name = "physicalRollToolStripMenuItem";
            this.physicalRollToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.physicalRollToolStripMenuItem.Text = "Physical Roll";
            this.physicalRollToolStripMenuItem.Click += new System.EventHandler(this.testerRootMenuItem_Click);
            // 
            // contextMenuAxisListPhysX
            // 
            this.contextMenuAxisListPhysX.Name = "contextMenuVirtualDevices";
            this.contextMenuAxisListPhysX.OwnerItem = this.physicalRollToolStripMenuItem;
            this.contextMenuAxisListPhysX.Size = new System.Drawing.Size(61, 4);
            this.contextMenuAxisListPhysX.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuTester_Closing);
            this.contextMenuAxisListPhysX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.testerContextDevices_MouseDown);
            // 
            // physicalYawToolStripMenuItem
            // 
            this.physicalYawToolStripMenuItem.DropDown = this.contextMenuAxisListPhysRZ;
            this.physicalYawToolStripMenuItem.Name = "physicalYawToolStripMenuItem";
            this.physicalYawToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.physicalYawToolStripMenuItem.Text = "Physical Yaw";
            this.physicalYawToolStripMenuItem.Click += new System.EventHandler(this.testerRootMenuItem_Click);
            // 
            // contextMenuAxisListPhysRZ
            // 
            this.contextMenuAxisListPhysRZ.Name = "contextMenuVirtualDevices";
            this.contextMenuAxisListPhysRZ.OwnerItem = this.physicalYawToolStripMenuItem;
            this.contextMenuAxisListPhysRZ.Size = new System.Drawing.Size(61, 4);
            this.contextMenuAxisListPhysRZ.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuTester_Closing);
            this.contextMenuAxisListPhysRZ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.testerContextDevices_MouseDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(625, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Right click on Tab/Tester for options";
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.contextMenuTray;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "JoystickCurve";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            // 
            // contextMenuTray
            // 
            this.contextMenuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profilesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuTray.Name = "contextMenuTray";
            this.contextMenuTray.Size = new System.Drawing.Size(213, 53);
            // 
            // profilesToolStripMenuItem
            // 
            this.profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
            this.profilesToolStripMenuItem.Size = new System.Drawing.Size(152, 23);
            this.profilesToolStripMenuItem.Text = "Profile";
            this.profilesToolStripMenuItem.SelectedIndexChanged += new System.EventHandler(this.profilesToolStripMenuItem_SelectedIndexChanged);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSettings.Location = new System.Drawing.Point(902, 2);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonSettings.TabIndex = 8;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // checkBoxHotKey
            // 
            this.checkBoxHotKey.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxHotKey.Enabled = false;
            this.checkBoxHotKey.Location = new System.Drawing.Point(339, 5);
            this.checkBoxHotKey.Name = "checkBoxHotKey";
            this.checkBoxHotKey.Size = new System.Drawing.Size(109, 25);
            this.checkBoxHotKey.TabIndex = 13;
            this.checkBoxHotKey.Text = "Hot Key";
            this.checkBoxHotKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxHotKey.UseVisualStyleBackColor = true;
            // 
            // timerTest
            // 
            this.timerTest.Interval = 10;
            this.timerTest.Tick += new System.EventHandler(this.timerTest_Tick);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Enabled = false;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSave.Location = new System.Drawing.Point(791, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(105, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save settings";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Visible = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // contextMenuAddTab
            // 
            this.contextMenuAddTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAxisTab,
            this.newMacroTab});
            this.contextMenuAddTab.Name = "contextMenuAddTab";
            this.contextMenuAddTab.Size = new System.Drawing.Size(132, 48);
            this.contextMenuAddTab.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuAddTab_Closing);
            this.contextMenuAddTab.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuAddTab_ItemClicked);
            // 
            // newAxisTab
            // 
            this.newAxisTab.Name = "newAxisTab";
            this.newAxisTab.Size = new System.Drawing.Size(131, 22);
            this.newAxisTab.Text = "Axis Tab";
            // 
            // newMacroTab
            // 
            this.newMacroTab.Name = "newMacroTab";
            this.newMacroTab.Size = new System.Drawing.Size(131, 22);
            this.newMacroTab.Text = "Macro Tab";
            // 
            // checkBoxBindAircraft
            // 
            this.checkBoxBindAircraft.AutoSize = true;
            this.checkBoxBindAircraft.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::JoystickCurves.Properties.Settings.Default, "warThunderTrackAircraft", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxBindAircraft.Enabled = global::JoystickCurves.Properties.Settings.Default.warThunderTrackAircraft;
            this.checkBoxBindAircraft.Location = new System.Drawing.Point(464, 8);
            this.checkBoxBindAircraft.Name = "checkBoxBindAircraft";
            this.checkBoxBindAircraft.Size = new System.Drawing.Size(165, 17);
            this.checkBoxBindAircraft.TabIndex = 16;
            this.checkBoxBindAircraft.Text = "Bind this profile to the aircraft:";
            this.checkBoxBindAircraft.UseVisualStyleBackColor = true;
            this.checkBoxBindAircraft.CheckedChanged += new System.EventHandler(this.checkBoxBindAircraft_CheckedChanged);
            // 
            // labelCurrentAircraft
            // 
            this.labelCurrentAircraft.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::JoystickCurves.Properties.Settings.Default, "warThunderTrackAircraft", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.labelCurrentAircraft.Enabled = global::JoystickCurves.Properties.Settings.Default.warThunderTrackAircraft;
            this.labelCurrentAircraft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCurrentAircraft.Location = new System.Drawing.Point(627, 9);
            this.labelCurrentAircraft.Name = "labelCurrentAircraft";
            this.labelCurrentAircraft.Size = new System.Drawing.Size(155, 13);
            this.labelCurrentAircraft.TabIndex = 15;
            // 
            // joystickTester
            // 
            this.joystickTester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.joystickTester.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.joystickTester.ContextMenuStrip = this.contextMenuTester;
            this.joystickTester.Controls.Add(this.label4);
            this.joystickTester.Controls.Add(this.label3);
            this.joystickTester.Controls.Add(this.label1);
            this.joystickTester.Controls.Add(this.labelYawPercent);
            this.joystickTester.Controls.Add(this.labelRollPercent);
            this.joystickTester.Controls.Add(this.labelPitchPercent);
            this.joystickTester.CurrentPhysicalDevice = null;
            this.joystickTester.CurrentVirtualDevice = null;
            this.joystickTester.HandleBounds = new System.Drawing.Rectangle(30, 15, 281, 281);
            this.joystickTester.Location = new System.Drawing.Point(628, 54);
            this.joystickTester.Margin = new System.Windows.Forms.Padding(40, 25, 20, 50);
            this.joystickTester.Name = "joystickTester";
            this.joystickTester.PhysicalHandleLocation = new System.Drawing.Point(170, 155);
            this.joystickTester.PhysicalRudderLocation = new System.Drawing.Point(170, 326);
            this.joystickTester.RudderBounds = new System.Drawing.Rectangle(30, 326, 281, 20);
            this.joystickTester.ShowPhysicalHandle = false;
            this.joystickTester.ShowPhysicalRudder = false;
            this.joystickTester.ShowVirtualHandle = false;
            this.joystickTester.ShowVirtualRudder = false;
            this.joystickTester.Size = new System.Drawing.Size(356, 356);
            this.joystickTester.TabIndex = 9;
            this.joystickTester.TabStop = false;
            this.joystickTester.VirtualHandleLocation = new System.Drawing.Point(170, 155);
            this.joystickTester.VirtualRudderLocation = new System.Drawing.Point(170, 326);
            this.joystickTester.Click += new System.EventHandler(this.joystickTester_Click);
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
            this.ClientSize = new System.Drawing.Size(989, 417);
            this.Controls.Add(this.checkBoxBindAircraft);
            this.Controls.Add(this.labelCurrentAircraft);
            this.Controls.Add(this.checkBoxHotKey);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabAxis);
            this.Controls.Add(this.comboProfiles);
            this.Controls.Add(this.joystickTester);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSettings);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Joystick Curves";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tabAxis.ResumeLayout(false);
            this.contextMenuTabPage.ResumeLayout(false);
            this.contextMenuTester.ResumeLayout(false);
            this.contextMenuTray.ResumeLayout(false);
            this.contextMenuAddTab.ResumeLayout(false);
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
        private JoystickTester joystickTester;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelYawPercent;
        private System.Windows.Forms.Label labelRollPercent;
        private System.Windows.Forms.Label labelPitchPercent;
        private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
        private System.Windows.Forms.ToolStripMenuItem deleteAxis;
        private System.Windows.Forms.ToolStripMenuItem copyCurveToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetCurveToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuTester;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem virtualDeviceLightGreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem physicalDeviceDarkGreenToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuVirtualDevices;
        private System.Windows.Forms.ContextMenuStrip contextMenuPhysicalDevices;
        private System.Windows.Forms.ToolStripMenuItem virtualPitchToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuAxisListVirtualX;
        private System.Windows.Forms.ToolStripMenuItem virtualRollToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem virtualYawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem physicalPitchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem physicalRollToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem physicalYawToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuAxisListVirtualY;
        private System.Windows.Forms.ContextMenuStrip contextMenuAxisListVirtualRZ;
        private System.Windows.Forms.ContextMenuStrip contextMenuAxisListPhysX;
        private System.Windows.Forms.ContextMenuStrip contextMenuAxisListPhysY;
        private System.Windows.Forms.ContextMenuStrip contextMenuAxisListPhysRZ;
        private System.Windows.Forms.ToolStripMenuItem streightenUpCurveToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuTray;
        private System.Windows.Forms.ToolStripComboBox profilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.ToolStripMenuItem deleteCurrentProfileToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxHotKey;
        private System.Windows.Forms.Timer timerTest;
        private System.Windows.Forms.ToolStripMenuItem responseModifierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiplier;
        private System.Windows.Forms.ToolStripMenuItem value;
        private System.Windows.Forms.Label labelCurrentAircraft;
        private System.Windows.Forms.CheckBox checkBoxBindAircraft;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuAddTab;
        private System.Windows.Forms.ToolStripMenuItem newAxisTab;
        private System.Windows.Forms.ToolStripMenuItem newMacroTab;
        private System.Windows.Forms.ToolStripMenuItem importExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
    }
}

