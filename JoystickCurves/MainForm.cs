﻿using System;
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

    public partial class MainForm : Form, INotifyPropertyChanged
    {
        private DeviceManager _deviceManager;
        private Properties.Settings _settings;
        private ProfileManager _profileManager;
        private BindingSource _axisNamesPhysPitch, _axisNamesVirtPitch, _axisNamesPhysYaw, _axisNamesVirtYaw, _virtualDeviceNames, _physicalDeviceNames;
        private Dictionary<String, Dictionary<JoystickOffset, DirectInputData>> _axisBinding;
        private List<VirtualJoystick> _virtualJoysticks;
        private Profile _currentProfile;
        private bool waitingHotkey = false;
        private bool _suspendTabActions = false;
        private String _currentContextMenu;
        private object lockAxisBinding = new object();
        private bool closeTesterContextMenu = true;
        private const int DEFPOINTSCOUNT = 13;
        private const string ANY = "Any";
        private const string NOTSET = "Not set";
        private const string NEWPROFILE = "<New profile...>";
        private const string COPYPROFILE = "<Copy profile...>";
        private const string PROFILEDEFNAME = "New Profile";
        private const string PRESSKEY = "Press Key";
        private bool isExit = false;
        private Mutex appMutex;
        private const string INSTANCENAME = "JoystickCurvesInstance";
        private Form debugForm;
        

        public MainForm()
        {         

            if (CheckRunningInstances())
            {
                this.Close();
                return;
            }


            InitializeComponent();
            
            checkBoxHotKey.DataBindings.Add(new Binding("Checked", this, "WaitingHotKey",false,DataSourceUpdateMode.OnPropertyChanged,null));

            _virtualJoysticks = new List<VirtualJoystick>();
            _axisBinding = new Dictionary<string, Dictionary<JoystickOffset, DirectInputData>>();
            _axisNamesPhysPitch = new BindingSource();
            _axisNamesPhysYaw = new BindingSource();
            _axisNamesVirtPitch = new BindingSource();
            _axisNamesVirtYaw = new BindingSource();
            _virtualDeviceNames = new BindingSource();
            _physicalDeviceNames = new BindingSource();


            debugForm = new DebugForm();
        }

        private bool CheckRunningInstances()
        {
            try
            {
                appMutex = Mutex.OpenExisting(INSTANCENAME);
                if (appMutex != null)
                {
                    return true;
                }
                return false;
            }
            catch (WaitHandleCannotBeOpenedException ex)
            {
                appMutex = new Mutex(true, INSTANCENAME);
                GC.KeepAlive(appMutex);
                return false;
            }


        }
        private void emptyAction(DirectInputData d) { }

        private void ActionSetVJoy(DirectInputData data)
        {
            if (data == null)
                return;

            var sourceDeviceName = data.DeviceName;
            var sourceAxisName = DIUtils.Name(data.JoystickOffset);
            var pTab = _currentProfile.Tabs.ToList().Where(t => t.SourceDevice == sourceDeviceName && t.SourceAxis == sourceAxisName).FirstOrDefault();

            if (pTab == null)
                return;

            var virtualDevice = _deviceManager.Joysticks.ToList().FirstOrDefault(
                gc => gc.Type == DeviceType.Virtual && gc.Name == pTab.DestinationDevice);

            if (virtualDevice == null)
                return;

            var sourceAxis = DIUtils.ID( sourceAxisName);

            BezierCurvePoints curvePoints = pTab.CurvePoints;
            int newValue = data.Value;

            if (curvePoints != null)
                newValue = (int)((float)data.Value * curvePoints.GetY(Utils.PTop(1, Math.Abs(data.Value), 32767)));

            virtualDevice.Set(sourceAxis, newValue);           
        }
        private void UpdateCurveActions()
        {
            var srcDeviceNames = _currentProfile.SourceDeviceList;
            var dstDeviceNames = _currentProfile.DestinationDeviceList;

            foreach (var srcDevName in srcDeviceNames)
            {
                var actionMap = _currentProfile.Tabs.Where(
                    t => t.SourceAxis != NOTSET && t.DestinationAxis != NOTSET).Select(
                    t => new KeyValuePair<JoystickOffset, Action<DirectInputData>>(DIUtils.ID(t.SourceAxis), ActionSetVJoy)).ToDictionary(
                    t => t.Key, t => t.Value);

                var srcDevice = _deviceManager.Joysticks.Where(d => d.Name == srcDevName).FirstOrDefault();
                if (srcDevice != null)
                {
                    srcDevice.SetActions(actionMap);
                }
            }

            var restOfSourceDeviceNames = _currentProfile.SourceDeviceList.Except(srcDeviceNames);
            var restOfDestinationDeviceNames = _currentProfile.DestinationDeviceList.Except(dstDeviceNames);

            _deviceManager.Joysticks.ForEach(d =>
            {
                if (restOfDestinationDeviceNames.Contains(d.Name))
                {
                    //How to unacquire vJoy ?!
                }
                else
                {
                    //Acquire if it isn't acquired yet
                }
            });


        }

        private void ActionSetTesterVirtualX(DirectInputData data)
        {
            Utils.SetProperty<JoystickTester, DirectInputData>(joystickTester, "VirtualAxisRoll", data);
            Utils.SetProperty<Label, String>(labelPitchPercent, "Text", data.PercentValue.ToString() + "%");
        }
        private void ActionSetTesterVirtualY(DirectInputData data)
        {
            Utils.SetProperty<JoystickTester, DirectInputData>(joystickTester, "VirtualAxisPitch", data);
            Utils.SetProperty<Label, String>(labelPitchPercent, "Text", data.PercentValue.ToString() + "%");
        }
        private void ActionSetTesterVirtualRZ(DirectInputData data)
        {
            Utils.SetProperty<JoystickTester, DirectInputData>(joystickTester, "VirtualAxisRudder", data);
            Utils.SetProperty<Label, String>(labelYawPercent, "Text", data.PercentValue.ToString() + "%");
        }

        private void ActionSetTesterPhysicalX(DirectInputData data)
        {
            Utils.SetProperty<JoystickTester, DirectInputData>(joystickTester, "PhysicalAxisRoll", data);
        }
        private void ActionSetTesterPhysicalY(DirectInputData data)
        {
            Utils.SetProperty<JoystickTester, DirectInputData>(joystickTester, "PhysicalAxisPitch", data);
        }
        private void ActionSetTesterPhysicalRZ(DirectInputData data)
        {
            Utils.SetProperty<JoystickTester, DirectInputData>(joystickTester, "PhysicalAxisRudder", data);
        }
        private void ClearTesterActions()
        {

            foreach (DirectInputJoystick dev in _deviceManager.Joysticks)
            {
                if (dev != null && dev.Type == DeviceType.Virtual)
                {
                    dev.DeleteAction(ActionSetTesterVirtualRZ);
                    dev.DeleteAction(ActionSetTesterVirtualX);
                    dev.DeleteAction(ActionSetTesterVirtualY);
                }
                else if (dev != null && dev.Type == DeviceType.Physical)
                {
                    dev.DeleteAction(ActionSetTesterPhysicalRZ);
                    dev.DeleteAction(ActionSetTesterPhysicalX);
                    dev.DeleteAction(ActionSetTesterPhysicalY);
                }

            }
        }
        private void UpdateTesterActions()
        {
            ClearTesterActions();

            var virtualDev = _deviceManager.Joysticks.Where(d => d.Name == joystickTester.CurrentVirtualDevice && d.Type == DeviceType.Virtual).FirstOrDefault();
            if (virtualDev != null)
            {
                var virtualActionMap = new Dictionary<JoystickOffset, Action<DirectInputData>>()
                {
                    { joystickTester.CurrentVirtualRZ, 
                      _settings.TesterVirtualJoystickRZ != NOTSET?new Action<DirectInputData>( ActionSetTesterVirtualRZ):emptyAction },
                    { joystickTester.CurrentVirtualX, 
                      _settings.TesterVirtualJoystickX != NOTSET?new Action<DirectInputData>( ActionSetTesterVirtualX):emptyAction },
                    { joystickTester.CurrentVirtualY, 
                      _settings.TesterVirtualJoystickY != NOTSET?new Action<DirectInputData>( ActionSetTesterVirtualY):emptyAction }
                };
                virtualDev.SetActions(virtualActionMap);
            }

            var physicalDev = _deviceManager.Joysticks.Where(d => d.Name == joystickTester.CurrentPhysicalDevice && d.Type == DeviceType.Physical ).FirstOrDefault();
            if (physicalDev != null)
            {
                var physicalActionMap = new Dictionary<JoystickOffset, Action<DirectInputData>>()
                {
                    { joystickTester.CurrentPhysicalRZ, 
                      _settings.TesterPhysicalJoystickRZ != NOTSET?new Action<DirectInputData>( ActionSetTesterPhysicalRZ):emptyAction },
                    { joystickTester.CurrentPhysicalX, 
                      _settings.TesterPhysicalJoystickX != NOTSET?new Action<DirectInputData>( ActionSetTesterPhysicalX):emptyAction },
                    { joystickTester.CurrentPhysicalY, 
                      _settings.TesterPhysicalJoystickY != NOTSET?new Action<DirectInputData>( ActionSetTesterPhysicalY):emptyAction }
                };
                physicalDev.SetActions(physicalActionMap);
            }
            


        }

        private void LoadSettings()
        {
            _settings = Properties.Settings.Default;
            _settings.PropertyChanged += new PropertyChangedEventHandler(_settings_PropertyChanged);
            if (!_settings.generalStartMinimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                MinimizeToTray();
            }

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

            SetCurrentProfile(_currentProfile);
            SetupProfileCombo();


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

        void _settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "generalAutoStart")
            {
                if (_settings.generalAutoStart)
                {
                    Utils.AddToStartup();
                }
                else
                {
                    Utils.RemoveFromStartup();
                }
            }
        }

        void ShowSettings()
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private Profile GetCurrentProfile()
        {
            Profile profile = new Profile(_currentProfile.Title);
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
                        TabTitle = axisEditor.Title,                                                
                    });
                }
            }
            profile.HotKeyJoystickName = _currentProfile.HotKeyJoystickName;
            profile.HotKeyKeyboardName = _currentProfile.HotKeyKeyboardName;
            profile.HotKeyMouseName = _currentProfile.HotKeyMouseName;
            profile.JoystickHotKey = _currentProfile.JoystickHotKey;
            profile.MouseHotKey = _currentProfile.MouseHotKey;
            profile.KeyboardHotKey = _currentProfile.KeyboardHotKey;

            return profile;
        }

        private void SaveSettings()
        {
            _settings.Profiles = _profileManager;
            _settings.CurrentProfile = GetCurrentProfile();
            _settings.Save();
        
        }

        private void UnacquireDevices()
        {
            foreach (var d in _deviceManager.Joysticks.ToList())
                d.Unacquire();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (_settings.generalMinimizeOnClose && !isExit)
            {
                this.WindowState = FormWindowState.Minimized;
                MinimizeToTray();
                e.Cancel = true;
            }

            Exit();
            
        }
        private void MinimizeToTray()
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Hide();
                trayIcon.BalloonTipText = "JoystickCurves is running!";
                //trayIcon.ShowBalloonTip(10);
            }
            else
            {
                if( _currentProfile != null )
                    SetCurrentProfile(_currentProfile.Title,true);
            }
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadSettings();

            _deviceManager = new DeviceManager();
            _deviceManager.OnJoystickList += new EventHandler<EventArgs>(deviceManager_OnJoystickList);
            _deviceManager.OnKeyboardList += new EventHandler<EventArgs>(deviceManager_OnKeyboardList);
            _deviceManager.OnMouseList += new EventHandler<EventArgs>(deviceManager_OnMouseList);


        }

        private void deviceManager_OnJoystickList(object sender, EventArgs e)
        {
            _deviceManager.Joysticks.ForEach(j => j.Acquire());
            SetupTesterContextMenus();
            SetupEditorComboBoxes();
            UpdateAxisBindings();
            UpdateCurveActions();
            UpdateTesterActions();
        }

        void deviceManager_OnMouseList(object sender, EventArgs e)
        {
            _deviceManager.Mouses.ForEach(m => m.Acquire());
            Utils.SetProperty<CheckBox,bool>(checkBoxHotKey, "Enabled", true);
        }

        void deviceManager_OnKeyboardList(object sender, EventArgs e)
        {
            _deviceManager.Keyboards.ForEach(k => k.Acquire());
        }

        private void SetupTesterContextMenus()
        {
            var physicalDevices = Utils.SetDeviceContextMenuItems( 
                    _deviceManager.Joysticks.Where(d => d.Type == DeviceType.Physical).Select(d => new ToolStripMenuItem(d.Name) { CheckOnClick = true, Name = "physicalDevice" }).ToList(),
                    "PhysicalDevice", 
                    _settings.TesterPhysicalJoystick, 
                    testerContextDevices_Click, 
                    testerContextDevices_MouseDown
               );

            var virtualDevices = Utils.SetDeviceContextMenuItems( 
                    _deviceManager.Joysticks.Where(d => d.Type == DeviceType.Virtual).Select(d => new ToolStripMenuItem(d.Name) { CheckOnClick = true, Name = "virtualDevice" }).ToList(),
                    "VirtualDevice", 
                    _settings.TesterVirtualJoystick,
                    testerContextDevices_Click, 
                    testerContextDevices_MouseDown
               );

            if (physicalDevices != null)
            {
                var currentPhysDevice = physicalDevices.Where(p => p.Checked).FirstOrDefault();
                if (currentPhysDevice != null)
                {
                    joystickTester.CurrentPhysicalDevice = currentPhysDevice.Text;
                    var axisListPhysX = Utils.SetAxisContextMenuItems("PhysAxisX", joystickTester.CurrentPhysicalX, testerContextDevices_Click, testerContextDevices_MouseDown);
                    var axisListPhysY = Utils.SetAxisContextMenuItems("PhysAxisY", joystickTester.CurrentPhysicalY, testerContextDevices_Click, testerContextDevices_MouseDown);
                    var axisListPhysRZ = Utils.SetAxisContextMenuItems("PhysAxisRZ", joystickTester.CurrentPhysicalRZ, testerContextDevices_Click, testerContextDevices_MouseDown);
                    physicalDeviceDarkGreenToolStripMenuItem.DropDownItems.Clear();
                    contextMenuPhysicalDevices.Items.Clear();
                    contextMenuAxisListPhysX.Items.Clear();
                    contextMenuAxisListPhysY.Items.Clear();
                    contextMenuAxisListPhysRZ.Items.Clear();
                    contextMenuPhysicalDevices.Items.AddRange(physicalDevices.ToArray());
                    contextMenuAxisListPhysX.Items.AddRange(axisListPhysX.ToArray());
                    contextMenuAxisListPhysY.Items.AddRange(axisListPhysY.ToArray());
                    contextMenuAxisListPhysRZ.Items.AddRange(axisListPhysRZ.ToArray());
                }

            }
            if (virtualDevices != null)
            {
                var currentVirtDevice = virtualDevices.Where(p => p.Checked).FirstOrDefault();
                if (currentVirtDevice == null)
                    currentVirtDevice = virtualDevices.Where(p => p.Text != NOTSET).FirstOrDefault();

                if (currentVirtDevice != null)
                {
                    joystickTester.CurrentVirtualDevice = currentVirtDevice.Text;
                    
                }
                var axisListVirtX = Utils.SetAxisContextMenuItems("VirtAxisX", joystickTester.CurrentVirtualX, testerContextDevices_Click, testerContextDevices_MouseDown);
                var axisListVirtY = Utils.SetAxisContextMenuItems("VirtAxisY", joystickTester.CurrentVirtualY, testerContextDevices_Click, testerContextDevices_MouseDown);
                var axisListVirtRZ = Utils.SetAxisContextMenuItems("VirtAxisRZ", joystickTester.CurrentVirtualRZ, testerContextDevices_Click, testerContextDevices_MouseDown);
                virtualDeviceLightGreenToolStripMenuItem.DropDownItems.Clear();
                contextMenuVirtualDevices.Items.Clear();
                contextMenuAxisListVirtualX.Items.Clear();
                contextMenuAxisListVirtualY.Items.Clear();
                contextMenuAxisListVirtualRZ.Items.Clear();
                contextMenuVirtualDevices.Items.AddRange(virtualDevices.ToArray());
                contextMenuAxisListVirtualX.Items.AddRange(axisListVirtX.ToArray());
                contextMenuAxisListVirtualY.Items.AddRange(axisListVirtY.ToArray());
                contextMenuAxisListVirtualRZ.Items.AddRange(axisListVirtRZ.ToArray());
            }
        }

        private void testerContextDevices_Click(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;

            var parent = currentItem.GetCurrentParent();
            foreach (ToolStripMenuItem item in parent.Items)
                    if( !item.Equals(currentItem) )
                        item.Checked = false;

            var selName = currentItem.Name.ToLower();
            var selText = currentItem.Text;
            var selOffset = DIUtils.ID(selText);

            switch (selName)
            {
                case "virtaxisy":
                    {                        
                        joystickTester.CurrentVirtualY = selOffset;
                        _settings.TesterVirtualJoystickY = selText;
                    }
                    break;
                case "virtaxisx":
                    {
                        joystickTester.CurrentVirtualX = selOffset;
                        _settings.TesterVirtualJoystickX = selText;
                    }
                    break;
                case "virtaxisrz":
                    {
                        joystickTester.CurrentVirtualRZ = selOffset;
                        _settings.TesterVirtualJoystickRZ = selText;
                    }
                    break;
                case "physaxisy":
                    {
                        joystickTester.CurrentPhysicalY = selOffset;
                        _settings.TesterPhysicalJoystickY = selText;
                    }
                    break;
                case "physzxisx":
                    {
                        joystickTester.CurrentPhysicalX = selOffset;
                        _settings.TesterPhysicalJoystickX = selText;
                    }
                    break;
                case "physaxisrz":
                    {
                        joystickTester.CurrentPhysicalRZ = selOffset;
                        _settings.TesterPhysicalJoystickRZ = selText;
                    }
                    break;
                case "virtualdevice":
                    {
                        joystickTester.CurrentVirtualDevice = selText;
                        _settings.TesterVirtualJoystick = selText;
                    }
                    break;
                case "physicaldevice":
                    {
                        joystickTester.CurrentPhysicalDevice = selText;
                        _settings.TesterPhysicalJoystick = selText;
                    }

                    break;
            }
            UpdateTesterActions();
            currentItem.Checked = true;
        }

        private void testerContextDevices_MouseDown(object sender, MouseEventArgs e)
        {
            closeTesterContextMenu = false;
        }

        private void SetupEditorComboBoxes()
        {
            if (_deviceManager == null)
                return;

            try
            {

                foreach (TabPage tp in tabAxis.TabPages)
                {
                    if (tp.Controls.Count > 0)
                    {
                        AxisEditor axisEditor = tp.Controls[0] as AxisEditor;
                        axisEditor.SourceControllers = _deviceManager.Joysticks.Where(d => d.Type == DeviceType.Physical).Select(d => d.Name).ToList();
                        axisEditor.DestinationControllers = _deviceManager.Joysticks.Where(d => d.Type == DeviceType.Virtual).Select(d => d.Name).ToList();
                        axisEditor.SourceAxis = DIUtils.AxisNames.ToList();
                        axisEditor.DestinationAxis = DIUtils.AxisNames.ToList();
                        axisEditor.OnChange += new EventHandler<EventArgs>(axisEditor_OnChange);
                    }
                }
            }
            catch { 
            
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
                                    _axisBinding[axisEditor.CurrentSourceDevice].Add(DIUtils.ID(axisEditor.CurrentSourceAxis), new DirectInputData()
                                    {
                                        JoystickOffset = DIUtils.ID(axisEditor.CurrentDestAxis),
                                        DeviceName = axisEditor.CurrentDestDevice,
                                        Min = -32767,
                                        Max = 32767,
                                        Value = 0

                                    });
                                }
                            }
                            else
                            {
                            
                                _axisBinding.Add(axisEditor.CurrentSourceDevice, new Dictionary<JoystickOffset, DirectInputData>{
                                {
                                    DIUtils.ID(axisEditor.CurrentSourceAxis),
                                    new DirectInputData() { 
                                        JoystickOffset = DIUtils.ID(axisEditor.CurrentDestAxis),
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
            var curText = Utils.GetProperty<TabPage>((TabPage)axisEditor.Parent, "Text");
            if( curText != axisEditor.CurrentSourceAxis )
                Utils.SetProperty<TabPage,String>( (TabPage)axisEditor.Parent, "Text",axisEditor.CurrentSourceAxis );

            var currentProfileName = _currentProfile.Title;
            _currentProfile.Tabs[axisEditor.Index] = axisEditor;

            UpdateAxisBindings();
            UpdateCurveActions();

        }


        private void tabAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suspendTabActions)
                return;

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
            var newTabPage = new TabPage("Axis " + tab.TabCount) { Name = "tabAxis" };

            var newAxisEditor = new AxisEditor() { 
                Location = new Point( templateTab.Padding.Left, templateTab.Padding.Top ), 
                Size = new Size(templateTab.Width - Padding.Left - Padding.Right, templateTab.Height - Padding.Top - Padding.Bottom),               
                Index = tabAxis.TabPages.Count - 1,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };
            newAxisEditor.ResetCurve();
            newTabPage.Controls.Add( newAxisEditor );

            tabAxis.TabPages.Insert(tabAxis.TabPages.Count - 1, newTabPage);
            if( select )
                tabAxis.SelectedTab = newTabPage;
            
            return newTabPage;
        }

        private void contextMenuTabPage_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "deleteAxis" && tabAxis.SelectedTab != null)
            {
                tabAxis.TabPages.Remove(tabAxis.SelectedTab);
                _currentProfile = GetCurrentProfile();
            }
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
                        ((AxisEditor)tp.Controls[0]).CurrentCurve = axisEditor.CurrentCurve.GetCopy();
                        break;
                    }
                }
             
            }
            else
            {
                var newTab = AddNewProfileTab(tabAxis, true);
                var newAxisEditor = newTab.Controls[0] as AxisEditor;
                newAxisEditor.CurrentSourceAxis = e.ClickedItem.Text;
                newAxisEditor.CurrentCurve = axisEditor.CurrentCurve.GetCopy();
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


        private void comboProfiles_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var selectedItem = (String)Utils.GetProperty<ComboBox>(comboProfiles, "SelectedItem");
            if (selectedItem == COPYPROFILE)
            {
                var newProfile = CreateNewProfile();
                _currentProfile.CopyTo(ref newProfile);
                SetCurrentProfile(newProfile.Title);
            }
            else if( selectedItem == NEWPROFILE)
            {
                var newProfile = CreateNewProfile();
                SetCurrentProfile(newProfile.Title);
            }
            else 
            {
                SetCurrentProfile(selectedItem);
            }
        }
        private Profile CreateNewProfile()
        {
            String profName = PROFILEDEFNAME;
            for (var i = 0; i <= _profileManager.Profiles.Count + 1; i++)
            {
                profName = String.Format(String.Format("{0}{1}", PROFILEDEFNAME, i == 0 ? "" : " #" + i));
                if (!_profileManager.Profiles.Exists(p => p.Title == profName))
                    break;
            }
            var newProfile = new Profile(profName);
            newProfile.Tabs.Add(new ProfileTab()
            {
                CurvePoints = new BezierCurvePoints(),
                TabTitle = "Axis 1"
            });
            newProfile.Tabs[0].CurvePoints.PointsCount = DEFPOINTSCOUNT;
            newProfile.Tabs[0].CurvePoints.Reset();

            _profileManager.Profiles.Add(newProfile);
            SetupProfileCombo();
            return newProfile;
        }
        private void SetCurrentProfile(String title, bool force = false)
        {
            if (((String)comboProfiles.SelectedItem) == _currentProfile.Title && _currentProfile == title && !force)
                return;

            if (string.IsNullOrEmpty(title))
            {
                Utils.SetProperty<ComboBox, String>(comboProfiles, "SelectedItem", _currentProfile.Title);
                return;
            }
            _suspendTabActions = true;

            while (tabAxis.TabPages.Count > 1)
                tabAxis.TabPages.RemoveAt(0);


            if (_profileManager.Profiles.Exists(p => p.Title == _currentProfile.Title))
            {
                _profileManager.Profiles.RemoveAll(p => p.Title == _currentProfile.Title);
            }
            _profileManager.Profiles.Add(_currentProfile);


            var selectedProfile = _profileManager.Profiles.FirstOrDefault(p => p.Title == title);
            
            if (selectedProfile != null)
                _currentProfile = selectedProfile;
            else
                _currentProfile.Title = title;

            foreach (var p in _currentProfile.Tabs)
            {
                var tabPage = AddNewProfileTab(tabAxis);
                var axisEditor = tabPage.Controls[0] as AxisEditor;

                p.CurvePoints.ScaleDrawPoints();
                axisEditor.CurrentCurve = p.CurvePoints;
                axisEditor.CurrentDestAxis = p.DestinationAxis;
                axisEditor.CurrentDestDevice = p.DestinationDevice;
                axisEditor.CurrentSourceAxis = p.SourceAxis;
                axisEditor.CurrentSourceDevice = p.SourceDevice;
                axisEditor.Title = String.IsNullOrEmpty(p.TabTitle)||p.TabTitle == NOTSET?String.Format("Axis {0}",_currentProfile.Tabs.IndexOf(p)+1):p.TabTitle;
                tabPage.Text = axisEditor.Title;
            }
            var hotkey = "Hot Key";
            if( !String.IsNullOrEmpty(_currentProfile.HotKeyJoystickName))
            {
                hotkey = "Joy: " + _currentProfile.JoystickHotKey;
            }
            else if( !String.IsNullOrEmpty(_currentProfile.HotKeyKeyboardName))
            {
                hotkey = "Key: " + _currentProfile.KeyboardHotKey;
            }
            else if( !String.IsNullOrEmpty(_currentProfile.HotKeyMouseName))
            {
                hotkey = "Mouse: " + _currentProfile.MouseHotKey;
            }
            checkBoxHotKey.Text = hotkey;

            _suspendTabActions = false;

            tabAxis.SelectedIndex = 0;
            Utils.SetProperty<ComboBox, String>(comboProfiles, "SelectedItem", _currentProfile.Title);

            if (!profilesToolStripMenuItem.Items.Contains(_currentProfile.Title))
            {
                var items = _profileManager.Profiles.Select(p => new ToolStripMenuItem(p.Title)
                {
                    Name = "profileItem",
                }
                ).ToArray();

                profilesToolStripMenuItem.SelectedIndexChanged -= profilesToolStripMenuItem_SelectedIndexChanged;
                profilesToolStripMenuItem.Items.Clear();
                profilesToolStripMenuItem.Items.AddRange(items);
                profilesToolStripMenuItem.Text = _currentProfile.Title;
                profilesToolStripMenuItem.SelectedItem = _currentProfile.Title;
                profilesToolStripMenuItem.SelectedIndexChanged +=new EventHandler(profilesToolStripMenuItem_SelectedIndexChanged); 
            }

            SetupEditorComboBoxes();

        }

        private void SetupProfileCombo()
        {
            var profileTitles = _profileManager.Profiles.Select(p => p.Title).OrderBy( p => p).ToArray();
            comboProfiles.Items.Clear();
            comboProfiles.Items.AddRange(profileTitles);
            comboProfiles.Items.Insert(0, NEWPROFILE);
            comboProfiles.Items.Insert(1, COPYPROFILE);
            Utils.SetProperty<ComboBox, String>(comboProfiles, "SelectedItem", _currentProfile.Title);
        }
        public static void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var errorMsg = e.Exception.Message + "\n\nStack Trace:\n" + e.Exception.StackTrace;
            Debug.Print(errorMsg);
        }

        private void streightenUpCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var axisEditor = tabAxis.SelectedTab.Controls[0] as AxisEditor;
            axisEditor.CurrentCurve.Streighten();
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void Exit()
        {
            UnacquireDevices();
            SaveSettings();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isExit = true;
            Close();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            MinimizeToTray();
        }

        private void profilesToolStripMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCurrentProfile(((ToolStripMenuItem)profilesToolStripMenuItem.SelectedItem).Text);
        }

        private void comboProfiles_Validated(object sender, EventArgs e)
        {

            var comboText = comboProfiles.Text;
            if (String.IsNullOrEmpty(comboText))
                return;

            if (!_profileManager.Profiles.Exists( p => p.Title == comboText ) )
            {
                _profileManager.Profiles.ForEach(p => { if (p.Title == _currentProfile.Title) { p.Title = comboText; } });
                _currentProfile.Title = comboText;
                SetupProfileCombo();

            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private void deleteCurrentProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nextProfile = _profileManager.Profiles.SkipWhile(p => p == _currentProfile).FirstOrDefault();
            if (nextProfile == null)
                nextProfile = _profileManager.Profiles.FirstOrDefault();

            if (nextProfile != null)
            {
                _profileManager.Profiles.Remove(_currentProfile);
                _currentProfile = nextProfile;
            }

            if (_profileManager.Profiles.Count <= 0)
            {
                _currentProfile = new Profile("Default");
                _currentProfile.Tabs.Add(new ProfileTab()
                {
                    CurvePoints = new BezierCurvePoints(),
                    TabTitle = "Axis 1"
                });
                _currentProfile.Tabs[0].CurvePoints.PointsCount = DEFPOINTSCOUNT;
                _currentProfile.Tabs[0].CurvePoints.Reset();
                _profileManager.Profiles.Add(_currentProfile);
            }
            SetCurrentProfile(_currentProfile.Title);
            SetupProfileCombo();

        }

        void dev_OnButtonChange(object sender, CustomEventArgs<DirectInputData> e)
        {
            DirectInputJoystick gController = sender as DirectInputJoystick;

            _currentProfile.JoystickHotKey = e.Data.JoystickOffset.ToString();
            _currentProfile.HotKeyJoystickName = e.Data.DeviceName;

            Utils.SetProperty<CheckBox, String>(checkBoxHotKey, "Text", "Joy: " + e.Data.JoystickOffset.ToString());
            Utils.SetProperty<Form, bool>(this, "WaitingHotKey", false);
        }


        void mouse_OnButtonDown(object sender, CustomEventArgs<DirectInputData> e)
        {
            DirectInputMouse mouse = sender as DirectInputMouse;
            _currentProfile.MouseHotKey = e.Data.MouseOffset.ToString();
            _currentProfile.HotKeyMouseName = e.Data.DeviceName;
            Utils.SetProperty<CheckBox, String>(checkBoxHotKey, "Text", "Mouse: " + e.Data.MouseOffset.ToString());
            Utils.SetProperty<Form, bool>(this, "WaitingHotKey", false);
        }

        void keyboard_OnKeyDown(object sender, CustomEventArgs<DirectInputData> e)
        {
            DirectInputKeyboard keyboard = sender as DirectInputKeyboard;
            _currentProfile.KeyboardHotKey = e.Data.KeyboardKey.ToString();
            _currentProfile.HotKeyKeyboardName = e.Data.DeviceName;
            Utils.SetProperty<CheckBox, String>(checkBoxHotKey, "Text", "Key: " + e.Data.KeyboardKey.ToString());
            Utils.SetProperty<Form, bool>(this, "WaitingHotKey", false);
        }

        private void SetupWaitHotKeyHandlers()
        {
            if (_currentProfile != null)
            {
                _currentProfile.KeyboardHotKey = String.Empty;
                _currentProfile.HotKeyKeyboardName = String.Empty;
                _currentProfile.MouseHotKey = String.Empty;
                _currentProfile.HotKeyMouseName = String.Empty;
                _currentProfile.JoystickHotKey = String.Empty;
                _currentProfile.HotKeyJoystickName = String.Empty;
            }
            checkBoxHotKey.Text = PRESSKEY;

            foreach (var joy in _deviceManager.Joysticks.Where(d => d.Type == DeviceType.Physical))
            {
                joy.OnButtonChange += new EventHandler<CustomEventArgs<DirectInputData>>(dev_OnButtonChange);
            }
            _deviceManager.Keyboards.ForEach(keyboard => keyboard.OnKeyDown += new EventHandler<CustomEventArgs<DirectInputData>>(keyboard_OnKeyDown));
            _deviceManager.Mouses.ForEach(mouse => mouse.OnButtonDown += new EventHandler<CustomEventArgs<DirectInputData>>(mouse_OnButtonDown));
        }
        private void RemoveWaitHotKeyHandlers()
        {
            foreach (var joy in _deviceManager.Joysticks.Where(d => d.Type == DeviceType.Physical))
            {
                joy.OnButtonChange -= dev_OnButtonChange;
            }
            _deviceManager.Keyboards.ForEach(keyboard => keyboard.OnKeyDown -= keyboard_OnKeyDown);
            _deviceManager.Mouses.ForEach(mouse => mouse.OnButtonDown -= mouse_OnButtonDown);            
        }

        public bool WaitingHotKey
        {
            get { return waitingHotkey; }
            set
            {
                waitingHotkey = value;
                if (waitingHotkey)
                {
                    SetupWaitHotKeyHandlers();                    
                    checkBoxHotKey.Enabled = false;
                    tabAxis.Focus();
                }
                else
                {
                    RemoveWaitHotKeyHandlers();
                    checkBoxHotKey.Enabled = true;
                }
                OnPropertyChanged("WaitingHotKey");
            }
        }


        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void tabAxis_KeyUp(object sender, KeyEventArgs e)
        {
            if (WaitingHotKey) e.SuppressKeyPress = true;
        }

        private void tabAxis_KeyDown(object sender, KeyEventArgs e)
        {
            if (WaitingHotKey) e.SuppressKeyPress = true;
        }
    }

}
