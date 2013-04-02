using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using Microsoft.DirectX.DirectInput;
using System.IO;
using System.Xml;
namespace JoystickCurves
{
    public partial class MainForm : Form
    {
        private VirtualJoystick _vjoy;
        private DeviceManager _deviceManager;
        private Properties.Settings _settings;
        private ProfileManager _profileManager;
        private BindingSource _axisNamesPhysPitch, _axisNamesVirtPitch, _axisNamesPhysYaw, _axisNamesVirtYaw, _virtualDeviceNames, _physicalDeviceNames;
        private Dictionary<String, Dictionary<JoystickOffset, Axis>> _axisBinding;
        private Profile _currentProfile;
        private String _currentContextMenu;
        private object lockAxisBinding = new object();
        private bool closeTesterContextMenu = true;
        private const int DEFPOINTSCOUNT = 13;
        private const string ANY = "Any";
        private const string NOTSET = "Not set";
        private Form debugForm;
        public MainForm()
        {

           
            InitializeComponent();

            _axisBinding = new Dictionary<string, Dictionary<JoystickOffset, Axis>>();
            _axisNamesPhysPitch = new BindingSource();
            _axisNamesPhysYaw = new BindingSource();
            _axisNamesVirtPitch = new BindingSource();
            _axisNamesVirtYaw = new BindingSource();
            _virtualDeviceNames = new BindingSource();
            _physicalDeviceNames = new BindingSource();


            debugForm = new DebugForm();
        }



        private void LoadSettings()
        {
            _settings = Properties.Settings.Default;

            if (_settings.Profiles != null)
                _profileManager = _settings.Profiles;

            if (_profileManager == null)
                _profileManager = new ProfileManager() { Title = "Default Manager" };


            if (_settings.CurrentProfile == null)
            {
                _currentProfile = new Profile("Default");
                _currentProfile.Tabs.Add(new ProfileTab()
                {
                    CurvePoints = new BezierCurvePoints(),
                    TabTitle = "Axis 1"
                });
                _currentProfile.Tabs[0].CurvePoints.PointsCount = DEFPOINTSCOUNT;
                _currentProfile.Tabs[0].CurvePoints.Reset();
            }
            else
                _currentProfile = _settings.CurrentProfile;

            if (_currentProfile.Tabs == null)
                _currentProfile.Tabs.Add(new ProfileTab() { TabTitle = "Axis 1" });

            SetProfile(_currentProfile);

            if (String.IsNullOrEmpty(_settings.TesterPhysicalJoystickX))
                _settings.TesterPhysicalJoystickX = "Roll";
            if (String.IsNullOrEmpty(_settings.TesterPhysicalJoystickY))
                _settings.TesterPhysicalJoystickY = "Pitch";
            if (String.IsNullOrEmpty(_settings.TesterPhysicalJoystickRZ))
                _settings.TesterPhysicalJoystickRZ = "Yaw";

            if (String.IsNullOrEmpty(_settings.TesterVirtualJoystickX))
                _settings.TesterVirtualJoystickX = "Roll";
            if (String.IsNullOrEmpty(_settings.TesterVirtualJoystickY))
                _settings.TesterVirtualJoystickY = "Pitch";
            if (String.IsNullOrEmpty(_settings.TesterVirtualJoystickRZ))
                _settings.TesterVirtualJoystickRZ = "Yaw";



            joystickTester.CurrentPhysicalDevice = _settings.TesterPhysicalJoystick;
            joystickTester.CurrentVirtualDevice = _settings.TesterVirtualJoystick;
            joystickTester.CurrentPhysicalX = DIUtils.ID(_settings.TesterPhysicalJoystickX);
            joystickTester.CurrentPhysicalY = DIUtils.ID(_settings.TesterPhysicalJoystickY);
            joystickTester.CurrentPhysicalRZ = DIUtils.ID(_settings.TesterPhysicalJoystickRZ);
            joystickTester.CurrentVirtualX = DIUtils.ID(_settings.TesterVirtualJoystickX);
            joystickTester.CurrentVirtualY = DIUtils.ID(_settings.TesterVirtualJoystickY);
            joystickTester.CurrentVirtualRZ = DIUtils.ID(_settings.TesterVirtualJoystickRZ);

        }

        private void SetProfile(Profile profile)
        {
            foreach (var p in profile.Tabs)
            {
                var tabPage = AddNewProfileTab(tabAxis);
                var axisEditor = tabPage.Controls[0] as AxisEditor;
                p.CurvePoints.ScaleDrawPoints();
                axisEditor.CurrentCurve = p.CurvePoints;
                axisEditor.CurrentDestAxis = p.DestinationAxis;
                axisEditor.CurrentDestDevice = p.DestinationDevice;
                axisEditor.CurrentSourceAxis = p.SourceAxis;
                axisEditor.CurrentSourceDevice = p.SourceDevice;
                axisEditor.Title = p.TabTitle;
                tabPage.Text = axisEditor.Title;
            }
            tabAxis.SelectedIndex = 0;
        }

        private Profile GetProfile()
        {
            Profile profile = new Profile();
            foreach (TabPage p in tabAxis.TabPages)
            {
                if (p.Controls.Count > 0)
                {
                    var axisEditor = p.Controls[0] as AxisEditor;
                    axisEditor.CurrentCurve.ScaleRawPoints();
                    profile.Tabs.Add(new ProfileTab()
                    {
                        CurvePoints = axisEditor.CurrentCurve,
                        DestinationAxis = axisEditor.CurrentDestAxis,
                        DestinationDevice = axisEditor.CurrentDestDevice,
                        SourceAxis = axisEditor.CurrentSourceAxis,
                        SourceDevice = axisEditor.CurrentSourceDevice,
                        TabTitle = axisEditor.Title
                    });
                }
            }
            return profile;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.Profiles = _profileManager;
            _settings.CurrentProfile = GetProfile();
            
            _settings.Save();

            if (_vjoy == null)
                return;

            if (!_vjoy.Enabled)
                return;

            _vjoy.Reset();

            
            foreach (var d in _deviceManager.Devices)
                d.Unacquire();

   
            Thread.Sleep(100);

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadSettings();

            try
            {
                _vjoy = new VirtualJoystick(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            _deviceManager = new DeviceManager();
            _deviceManager.OnDeviceList += new EventHandler<EventArgs>(deviceManager_OnDeviceList);

            //TestAnimation();

        }

        private void TestAnimation()
        {
            var x = _vjoy.MinX;
            var min = _vjoy.MinX;
            var max = _vjoy.MaxX;
            var ax = new Axis(_vjoy.MinX, _vjoy.MaxX);
            for (var i = 0; i < 6000000; i++)
            {
                x += 50;
                if (x > max)
                    x = min;
                ax.Value = x;
                //Utils.SetProperty<JoystickTester,Axis>(joystickTester,"VirtualAxisRoll",ax);
                ThreadPool.QueueUserWorkItem(arg => { _vjoy.X = x; });
                Application.DoEvents();
    
            }
        }

        private void SetupTester()
        {
            var physicalDevices = _deviceManager.Devices.Where(d => d.Type == GameControllerType.Physical).Select(d => 
                new ToolStripMenuItem(d.Name) { CheckOnClick = true, Name = "physicalDevice" }).ToList();
            var virtualDevices = _deviceManager.Devices.Where(d => d.Type == GameControllerType.Virtual).Select(d => 
                new ToolStripMenuItem(d.Name) { CheckOnClick = true, Name = "virtualDevice" }).ToList();

            if (string.IsNullOrEmpty(_settings.TesterPhysicalJoystick))
            {
                if (physicalDevices.Count > 0)
                    _settings.TesterPhysicalJoystick = physicalDevices[0].Text;
                else
                    _settings.TesterPhysicalJoystick = "Any";

                joystickTester.CurrentPhysicalDevice = _settings.TesterPhysicalJoystick;

            }
            if (string.IsNullOrEmpty(_settings.TesterVirtualJoystick))
            {
                if (virtualDevices.Count > 0)
                    _settings.TesterVirtualJoystick = virtualDevices[0].Text;
                else
                    _settings.TesterVirtualJoystick = "Any";

                joystickTester.CurrentVirtualDevice = _settings.TesterVirtualJoystick;
            }

            var axisListVirtX = DIUtils.AxisNames.Select(ax => new ToolStripMenuItem(ax) { CheckOnClick = true }).ToList();
            var axisListVirtY = DIUtils.AxisNames.Select(ax => new ToolStripMenuItem(ax) { CheckOnClick = true }).ToList();
            var axisListVirtRZ = DIUtils.AxisNames.Select(ax => new ToolStripMenuItem(ax) { CheckOnClick = true }).ToList();
            var axisListPhysX = DIUtils.AxisNames.Select(ax => new ToolStripMenuItem(ax) { CheckOnClick = true }).ToList();
            var axisListPhysY = DIUtils.AxisNames.Select(ax => new ToolStripMenuItem(ax) { CheckOnClick = true }).ToList();
            var axisListPhysRZ = DIUtils.AxisNames.Select(ax => new ToolStripMenuItem(ax) { CheckOnClick = true }).ToList();

            physicalDevices.Insert(0, new ToolStripMenuItem("Any") { CheckOnClick = true });
            physicalDevices.Insert(0, new ToolStripMenuItem("Not set") { CheckOnClick = true });
            virtualDevices.Insert(0, new ToolStripMenuItem("Any") { CheckOnClick = true });
            virtualDevices.Insert(0, new ToolStripMenuItem("Not set") { CheckOnClick = true });

            axisListVirtX.Insert(0, new ToolStripMenuItem("Not set") { CheckOnClick = true });
            axisListVirtY.Insert(0, new ToolStripMenuItem("Not set") { CheckOnClick = true });
            axisListVirtRZ.Insert(0, new ToolStripMenuItem("Not set") { CheckOnClick = true });
            axisListPhysX.Insert(0, new ToolStripMenuItem("Not set") { CheckOnClick = true });
            axisListPhysY.Insert(0, new ToolStripMenuItem("Not set") { CheckOnClick = true });
            axisListPhysRZ.Insert(0, new ToolStripMenuItem("Not set") { CheckOnClick = true });

            foreach (var d in physicalDevices)
            {
                d.Click += new EventHandler(testerContextDevices_Click);
                d.MouseDown += new MouseEventHandler(testerContextDevices_MouseDown);
                d.Name = "PhysicalDevice";
                if (joystickTester.CurrentPhysicalDevice == d.Text)
                    d.Checked = true;
                else
                    d.Checked = false;
            }
            foreach (var d in virtualDevices)
            {
                d.Click += new EventHandler(testerContextDevices_Click);
                d.MouseDown += new MouseEventHandler(testerContextDevices_MouseDown);
                d.Name = "VirtualDevice";
                if (joystickTester.CurrentVirtualDevice == d.Text)
                    d.Checked = true;
                else
                    d.Checked = false;
            }

            foreach (var d in axisListVirtX)
            {
                d.Click += new EventHandler(testerContextDevices_Click);
                d.MouseDown += new MouseEventHandler(testerContextDevices_MouseDown);
                d.Name = "VirtAxisX";
                if (joystickTester.CurrentVirtualX == DIUtils.ID(d.Text) && DIUtils.AxisNames.Contains(d.Text) )
                    d.Checked = true;
                else
                    d.Checked = false;
            }
            foreach (var d in axisListVirtY)
            {
                d.Click += new EventHandler(testerContextDevices_Click);
                d.MouseDown += new MouseEventHandler(testerContextDevices_MouseDown);
                d.Name = "VirtAxisY";
                if (joystickTester.CurrentVirtualY == DIUtils.ID(d.Text) && DIUtils.AxisNames.Contains(d.Text))
                    d.Checked = true;
                else
                    d.Checked = false;
            }
            foreach (var d in axisListVirtRZ)
            {
                d.Click += new EventHandler(testerContextDevices_Click);
                d.MouseDown += new MouseEventHandler(testerContextDevices_MouseDown);
                d.Name = "VirtAxisRZ";
                if (joystickTester.CurrentVirtualRZ == DIUtils.ID(d.Text) && DIUtils.AxisNames.Contains(d.Text))
                    d.Checked = true;
                else
                    d.Checked = false;
            }
            foreach (var d in axisListPhysX)
            {
                d.Click += new EventHandler(testerContextDevices_Click);
                d.MouseDown += new MouseEventHandler(testerContextDevices_MouseDown);
                d.Name = "PhysAxisX";
                if (joystickTester.CurrentPhysicalX == DIUtils.ID(d.Text) && DIUtils.AxisNames.Contains(d.Text))
                    d.Checked = true;
                else
                    d.Checked = false;
            }
            foreach (var d in axisListPhysY)
            {
                d.Click += new EventHandler(testerContextDevices_Click);
                d.MouseDown += new MouseEventHandler(testerContextDevices_MouseDown);
                d.Name = "PhysAxisY";
                if (joystickTester.CurrentPhysicalY == DIUtils.ID(d.Text) && DIUtils.AxisNames.Contains(d.Text))
                    d.Checked = true;
                else
                    d.Checked = false;
            }
            foreach (var d in axisListPhysRZ)
            {
                d.Click += new EventHandler(testerContextDevices_Click);
                d.MouseDown += new MouseEventHandler(testerContextDevices_MouseDown);
                d.Name = "PhysAxisRZ";
                if (joystickTester.CurrentPhysicalRZ == DIUtils.ID(d.Text) && DIUtils.AxisNames.Contains(d.Text))
                    d.Checked = true;
                else
                    d.Checked = false;
            }
            
            virtualDeviceLightGreenToolStripMenuItem.DropDownItems.Clear();
            physicalDeviceDarkGreenToolStripMenuItem.DropDownItems.Clear();


            contextMenuVirtualDevices.Items.AddRange(virtualDevices.ToArray());
            contextMenuPhysicalDevices.Items.AddRange(physicalDevices.ToArray());
            
            contextMenuAxisListVirtualX.Items.AddRange(axisListVirtX.ToArray());
            contextMenuAxisListVirtualY.Items.AddRange(axisListVirtY.ToArray());
            contextMenuAxisListVirtualRZ.Items.AddRange(axisListVirtRZ.ToArray());
            contextMenuAxisListPhysX.Items.AddRange(axisListPhysX.ToArray());
            contextMenuAxisListPhysY.Items.AddRange(axisListPhysY.ToArray());
            contextMenuAxisListPhysRZ.Items.AddRange(axisListPhysRZ.ToArray());


        }

        private void testerContextDevices_Click(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;

            var parent = currentItem.GetCurrentParent();
            foreach (ToolStripMenuItem item in parent.Items)
                    if( !item.Equals(currentItem) )
                        item.Checked = false;

            switch (currentItem.Name.ToLower())
            {
                case "virtaxisy":
                    {
                        joystickTester.CurrentVirtualY = DIUtils.ID(currentItem.Text);
                        _settings.TesterVirtualJoystickY = currentItem.Text;
                    }
                    break;
                case "virtaxisx":
                    {
                        joystickTester.CurrentVirtualX = DIUtils.ID(currentItem.Text);
                        _settings.TesterVirtualJoystickX = currentItem.Text;
                    }
                    break;
                case "virtaxisrz":
                    {
                        joystickTester.CurrentVirtualRZ = DIUtils.ID(currentItem.Text);
                        _settings.TesterVirtualJoystickRZ = currentItem.Text;
                    }
                    break;
                case "physaxisy":
                    {
                        joystickTester.CurrentPhysicalY = DIUtils.ID(currentItem.Text);
                        _settings.TesterPhysicalJoystickY = currentItem.Text;
                    }
                    break;
                case "physzxisx":
                    {
                        joystickTester.CurrentPhysicalX = DIUtils.ID(currentItem.Text);
                        _settings.TesterPhysicalJoystickX = currentItem.Text;
                    }
                    break;
                case "physaxisrz":
                    {
                        joystickTester.CurrentPhysicalRZ = DIUtils.ID(currentItem.Text);
                        _settings.TesterPhysicalJoystickRZ = currentItem.Text;
                    }
                    break;
                case "virtualdevice":
                    {
                        joystickTester.CurrentVirtualDevice = currentItem.Text;
                        _settings.TesterVirtualJoystick = currentItem.Text;
                    }
                    break;
                case "physicaldevice":
                    {
                        joystickTester.CurrentPhysicalDevice = currentItem.Text;
                        _settings.TesterPhysicalJoystick = currentItem.Text;
                    }

                    break;
            }
            currentItem.Checked = true;
        }

        private void testerContextDevices_MouseDown(object sender, MouseEventArgs e)
        {
            closeTesterContextMenu = false;
        }

        private void SetupEditorComboBoxes()
        {

            foreach (TabPage tp in tabAxis.TabPages)
            {
                if (tp.Controls.Count > 0)
                {
                    AxisEditor axisEditor = tp.Controls[0] as AxisEditor;
                    axisEditor.SourceControllers = _deviceManager.Devices.Where(d => d.Type == GameControllerType.Physical).Select(d => d.Name).ToList();
                    axisEditor.DestinationControllers = _deviceManager.Devices.Where(d => d.Type == GameControllerType.Virtual).Select(d => d.Name).ToList();
                    axisEditor.SourceAxis = DIUtils.AxisNames.ToList();
                    axisEditor.DestinationAxis = DIUtils.AxisNames.ToList();
                    axisEditor.OnChange += new EventHandler<EventArgs>(axisEditor_OnChange);
                }
            }
        }

        private void UpdateAxisBindings()
        {

            lock (lockAxisBinding)
            {
                _axisBinding.Clear();
                foreach (TabPage tp in tabAxis.TabPages)
                {                    
                    if (tp.Controls.Count > 0)
                    {
                        AxisEditor axisEditor = tp.Controls[0] as AxisEditor;
                        if (axisEditor.CurrentDestDevice != NOTSET && axisEditor.CurrentDestAxis != NOTSET && axisEditor.CurrentSourceAxis != NOTSET )
                        {
                            if (_axisBinding.ContainsKey(axisEditor.CurrentSourceDevice))
                            {
                                if (!_axisBinding[axisEditor.CurrentSourceDevice].ContainsKey(DIUtils.ID(axisEditor.CurrentSourceAxis)))
                                {
                                    _axisBinding[axisEditor.CurrentSourceDevice].Add(DIUtils.ID(axisEditor.CurrentSourceAxis), new Axis()
                                    {
                                        DirectInputID = DIUtils.ID(axisEditor.CurrentDestAxis),
                                        DeviceName = axisEditor.CurrentDestDevice,
                                        Min = -32767,
                                        Max = 32767,
                                        Value = 0

                                    });
                                }
                            }
                            else
                            {
                            
                                _axisBinding.Add(axisEditor.CurrentSourceDevice, new Dictionary<JoystickOffset, Axis>{
                                {
                                    DIUtils.ID(axisEditor.CurrentSourceAxis),
                                    new Axis() { 
                                        DirectInputID = DIUtils.ID(axisEditor.CurrentDestAxis),
                                        DeviceName = axisEditor.CurrentDestDevice,
                                        Min = -32767,
                                        Max = 32767,
                                        Value = 0
                                    }
                                }
                                });
                            }
                        }

                    }
                }
            }
        }

        private void SetupTabContextMenu()
        {
            var axisList = DIUtils.AxisNames.ToList().Except(new string[] {tabAxis.SelectedTab.Text}).ToList();
            copyCurveToToolStripMenuItem.DropDownItems.Clear();
            foreach( var a in axisList )
                copyCurveToToolStripMenuItem.DropDownItems.Add(a);
        }

        private void axisEditor_OnChange(object sender, EventArgs e)
        {
            var axisEditor = sender as AxisEditor;
            Utils.SetProperty<TabPage,String>( (TabPage)axisEditor.Parent, "Text",axisEditor.CurrentSourceAxis );
            var currentProfileName = _currentProfile.Title;
            _currentProfile.Tabs[axisEditor.Index] = axisEditor;

            UpdateAxisBindings();

        }

        private void deviceManager_OnDeviceList(object sender, EventArgs e)
        {
            foreach (var dev in _deviceManager.Devices)
            {
                dev.OnAxisChange += new EventHandler<CustomEventArgs<Axis>>(dev_OnAxisChange);
                dev.OnButtonChange += new EventHandler<CustomEventArgs<Button>>(dev_OnButtonChange);
                dev.Acquire();
            }

            SetupTester();
            SetupEditorComboBoxes();
            UpdateAxisBindings();
        }

        private void dev_OnButtonChange(object sender, CustomEventArgs<Button> e)
        {
        }

        private void dev_OnAxisChange(object sender, CustomEventArgs<Axis> e)
        {
            var devName = e.Data.DeviceName;

            var axisName = DIUtils.Name(e.Data.DirectInputID);



                lock (lockAxisBinding)
                {
                    Dictionary<JoystickOffset, Axis> bindParams = _axisBinding.FirstOrDefault(x => x.Key == devName).Value;
                    if (bindParams != null)
                    {
                    
                        BezierCurvePoints curvePoints = _currentProfile.Tabs.Where(t => t.SourceDevice == devName && t.SourceAxis == axisName).Select(t => t.CurvePoints).FirstOrDefault();
                        int newValue = e.Data.Value;

                        if (curvePoints != null)
                            newValue = (int)((float)e.Data.Value * curvePoints.GetY(Utils.PTop(1, Math.Abs(e.Data.Value), 32767)));
                    
                        if (bindParams != null)
                        {
                            //Axis dstAxis
                            var dstAxis = bindParams.FirstOrDefault(x => x.Key == e.Data.DirectInputID).Value;
                            if (dstAxis != null)
                            {
                                switch (dstAxis.DirectInputID)
                                {
                                    case JoystickOffset.X:
                                        _vjoy.X = newValue;
                                        break;
                                    case JoystickOffset.Y:
                                        _vjoy.Y = newValue;
                                        break;
                                    case JoystickOffset.Z:
                                        _vjoy.Z = newValue;
                                        break;
                                    case JoystickOffset.RX:
                                        _vjoy.RX = newValue;
                                        break;
                                    case JoystickOffset.RY:
                                        _vjoy.RY = newValue;
                                        break;
                                    case JoystickOffset.RZ:
                                        _vjoy.RZ = newValue;
                                        break;
                                    case JoystickOffset.Slider0:
                                        _vjoy.SL0 = newValue;
                                        break;
                                    case JoystickOffset.Slider1:
                                        _vjoy.SL1 = newValue;
                                        break;
                                }
                            }
                        }
                    }
                
            }

            
            if (devName == joystickTester.CurrentVirtualDevice || (joystickTester.CurrentVirtualDevice == ANY && devName.ToLower().Contains("vjoy")))
            {
                if (e.Data.DirectInputID == joystickTester.CurrentVirtualX )
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "VirtualAxisRoll", e.Data);
                    Utils.SetProperty<Label, String>(labelRollPercent, "Text", e.Data.PercentValue.ToString() + "%");
                }
                else if (e.Data.DirectInputID == joystickTester.CurrentVirtualY)
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "VirtualAxisPitch", e.Data);
                    Utils.SetProperty<Label, String>(labelPitchPercent, "Text", e.Data.PercentValue.ToString() + "%");
                }
                else if (e.Data.DirectInputID == joystickTester.CurrentVirtualRZ)
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "VirtualAxisRudder", e.Data);
                    Utils.SetProperty<Label,String>(labelYawPercent,"Text", e.Data.PercentValue.ToString() + "%");
                }
                return;
            }

            if (devName == joystickTester.CurrentPhysicalDevice || (joystickTester.CurrentVirtualDevice == ANY && !devName.ToLower().Contains("vjoy")))
            {
                if (e.Data.DirectInputID == joystickTester.CurrentPhysicalX)
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisRoll", e.Data);
                }
                else if (e.Data.DirectInputID == joystickTester.CurrentPhysicalY)
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisPitch", e.Data);
                }
                else if (e.Data.DirectInputID == joystickTester.CurrentPhysicalRZ)
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisRudder", e.Data);
                }
            }


        }

        private void tabAxis_SelectedIndexChanged(object sender, EventArgs e)
        {

            var tab = sender as TabControl;
            if (tab.SelectedTab.Name == "tabAddNew")
            {
                var newTab = AddNewProfileTab(tab,true);
                var axisEditor = newTab.Controls[0] as AxisEditor;
                var curve = new BezierCurvePoints();
                curve.PointsCount = DEFPOINTSCOUNT;
                axisEditor.CurrentCurve.Reset();
                SetupEditorComboBoxes();
                _currentProfile.Tabs.Add(axisEditor);
            }
            SetupTabContextMenu();
        }

        private TabPage AddNewProfileTab(TabControl tab, bool select = false)
        {
            var templateTab = tabAxis.TabPages[tabAxis.TabPages.Count-1];
            var newTabPage = new TabPage("Axis " + tab.TabCount);

            var newAxisEditor = new AxisEditor() { 
                Location = new Point( templateTab.Padding.Left, templateTab.Padding.Top ), 
                Size = new Size(templateTab.Width - Padding.Left - Padding.Right, templateTab.Height - Padding.Top - Padding.Bottom),
                Index = tabAxis.TabPages.Count - 1
            };
            newAxisEditor.ResetCurve();
            newTabPage.Controls.Add( newAxisEditor );

            tabAxis.TabPages.Insert(tabAxis.TabPages.Count - 1, newTabPage);
            if( select )
                tabAxis.SelectedTab = newTabPage;
            Invalidate();
            
            return newTabPage;
        }

        private void contextMenuTabPage_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "deleteAxis")
                tabAxis.TabPages.Remove(tabAxis.SelectedTab);
        }

        private void copyCurveToToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var title = e.ClickedItem.Text;
            var axisEditor = tabAxis.SelectedTab.Controls[0] as AxisEditor;
            var tab = _currentProfile.Tabs.Where(t => t.TabTitle == title || t.SourceAxis == title ).FirstOrDefault();
            if (tab != null)
            {
                tab.CurvePoints = axisEditor.CurrentCurve;
                foreach (TabPage tp in tabAxis.TabPages)
                {
                    if (tp.Text == title)
                    {
                        ((AxisEditor)tp.Controls[0]).CurrentCurve = axisEditor.CurrentCurve;
                        break;
                    }
                }
             
            }
            else
            {
                var newTab = AddNewProfileTab(tabAxis, true);
                var newAxisEditor = newTab.Controls[0] as AxisEditor;
                newAxisEditor.CurrentSourceAxis = e.ClickedItem.Text;
                newAxisEditor.CurrentCurve = axisEditor.CurrentCurve;
                SetupEditorComboBoxes();
                SetupTabContextMenu();

                _currentProfile.Tabs.Add(newAxisEditor);
            }
        }

        private void resetCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var axisEditor = tabAxis.SelectedTab.Controls[0] as AxisEditor;
            axisEditor.ResetCurve();

        }

        private void contextMenuTester_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            e.Cancel = !closeTesterContextMenu;
            closeTesterContextMenu = true;
        }

        private void testerRootMenuItem_Click(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;
            _currentContextMenu = currentItem.Text;
        }

        public static void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var errorMsg = e.Exception.Message + "\n\nStack Trace:\n" + e.Exception.StackTrace;
            Debug.Print(errorMsg);
        }
    }

}
