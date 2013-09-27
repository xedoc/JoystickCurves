using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private const string WAITSPAWN = "you didn't spawn yet";
        private const string DISABLEDOPTION = "Disabled in settings";
        private const string PROFILEDEFNAME = "New Profile";
        private const string PRESSKEY = "Press Key";
        private bool isExit = false;
        private Mutex appMutex;
        private const string INSTANCENAME = "JoystickCurvesInstance";
        private Form debugForm;
        private bool lastEnableSaitek = false, lastEnableSteam = false;
        private object lockSetProfile = new object();
        private object lockHotKeyboard = new object();
        private object lockHotMouse = new object();
        private object lockHotJoystick = new object();
        private object lockWarThunder = new object();
        private object lockSaitek = new object();
        private CurrentAircraft currentAircraft;
        private AxisEditor currentAxisEditor;
        private Steam _steam;
        private SaitekMFD _saitek;
        
        SettingsForm settingsForm;

        public MainForm()
        {         

            if (CheckRunningInstances())
            {
                this.Close();
                return;
            }
            _settings = Properties.Settings.Default;           
            _settings.PropertyChanged += new PropertyChangedEventHandler(_settings_PropertyChanged);

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

        void settingsForm_OnHotKeyRequest(object sender, HotKeyArgs e)
        {
            var hk = _deviceManager.GetHotKey(e.HotKey);
            settingsForm.UpdateHotKey(hk);
        }

        void settingsForm_OnReset(object sender, EventArgs e)
        {
            _settings.Reset();
            _settings.Save();
            _settings = Properties.Settings.Default;
            _settings.CurrentProfile = null;
            _profileManager = null;
            _currentProfile = null;
            LoadSettings();
            _deviceManager = new DeviceManager();

        }
        private void ConnectSaitek()
        {
            lock (lockSaitek)
            {
                if (_settings.saitekX52ProEnable)
                {
                    if (_saitek == null || !_saitek.Acquired)
                    {
                        Debug.Print("Connecting Saitek");
                        try
                        {

                            _saitek = new SaitekMFD();
                            if (!_saitek.Acquiring)
                            {
                                _saitek.Acquire();
                                _saitek.AddPage(1, "JoystickCurves");
                            }
                        }
                        catch { }
                    }
                }
            }
        }
        private void DisconnectSaitek()
        {
            if (_saitek != null && !_saitek.Unacquiring)
            {
                _saitek.UnAcquire();
                _saitek = null;
            }
        }
        private void DisconnectSteam()
        {
            if (_steam != null && !_steam.Disconnecting)
            {
                _steam.Stop();
                _steam = null;
            }
        }
        private void SendSteamMessage( String text)
        {
            if (_settings.globalSteamEnable && _steam != null)
            {
                ThreadPool.QueueUserWorkItem( t => _steam.SendMessage(null, text));
            }
        }
        private void ConnectSteam()
        {
            if (_steam != null && ( _steam.Connecting || _steam.LoggedIn ))
                return;

            if ( _settings.globalSteamEnable)
            {

                _steam = new Steam();
                _steam.OnGuardCode += new EventHandler<EventArgs>(_steam_OnGuardCode);
                _steam.OnLoginError += new EventHandler<EventArgs>(_steam_OnLoginError);
                _steam.OnLogon += new EventHandler<SteamAPI.SteamEvent>(_steam_OnLogon);
                _steam.OnMessage += new EventHandler<SteamAPI.SteamEvent>(_steam_OnMessage);
                if (String.IsNullOrEmpty(_settings.steamLogin) || String.IsNullOrEmpty(_settings.steamPassword))
                    return;

                if( !_steam.LoggedIn && !_steam.Connecting)
                    _steam.Connect(_settings.steamLogin, _settings.steamPassword, String.Empty, _settings.steamToken);
            }
        }

        void _steam_OnMessage(object sender, SteamAPI.SteamEvent e)
        {
        }

        void _steam_OnLogon(object sender, SteamAPI.SteamEvent e)
        {
            _steam.Start();
        }

        void _steam_OnLoginError(object sender, EventArgs e)
        {
            _steam.Stop();
            MessageBox.Show("Steam login failed! Check your login and password!");
        }

        void _steam_OnGuardCode(object sender, EventArgs e)
        {
            var code = InputBox.Show("Check your mail for Steam Guard Code and enter it here:");
            if( !string.IsNullOrEmpty(code ))
            {
                if (_steam.Connect(_settings.steamLogin, _settings.steamPassword, code, _settings.steamToken) == SteamAPI.LoginStatus.LoginSuccessful)
                {
                    _settings.steamToken = _steam.Token;
                }
            }
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
            catch 
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

            var destAxis = DIUtils.ID(pTab.DestinationAxis);


            BezierCurvePoints curvePoints = pTab.CurvePoints;
            int newValue = data.Value;
            if (curvePoints != null)
            {
                float y = curvePoints.GetY(Utils.PTop(1.0f, Math.Abs(data.Value), 32767.0f));
                switch (curvePoints.CurveResponseType)
                {
                    case CurveResponseType.Multiplier:
                        newValue = (int)((float)data.Value * y);
                        Debug.Print(String.Format("y: {0}, value: {1}", y, newValue));
                        break;
                    case CurveResponseType.Values:
                        newValue = (int)(y * 32767.0f * (data.Value < 0 ? -1 : 1));
                        break;
                }
            }
            
           

            if (pTab.Correction != 0)
            {
                var c = (int)(32767.0f * pTab.Correction / 100.0f);

                if (pTab.PreserveAxisRange)
                {
                    newValue = (int)((32767.0f - (newValue<c?-1:1) * c) * newValue / 32767.0f + c);
                }
                else
                {
                    newValue += c;
                }
            }
            
            
            if (newValue < -32767)
                newValue = -32767;
            else if (newValue > 32767)
                newValue = 32767;

            virtualDevice.Set(destAxis, newValue);           

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
            Utils.SetProperty<Label, String>(labelRollPercent, "Text", data.PercentValue.ToString() + "%");
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
            joystickTester.ShowPhysicalHandle = _settings.TesterPhysicalJoystick == NOTSET ? false : true;
            joystickTester.ShowPhysicalRudder = _settings.TesterPhysicalJoystick == NOTSET ? false : true;
            joystickTester.ShowVirtualHandle = _settings.TesterVirtualJoystick == NOTSET ? false : true;
            joystickTester.ShowVirtualRudder = _settings.TesterVirtualJoystick == NOTSET ? false : true;

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

            if (_settings.hotKeys == null)
                _settings.hotKeys = new HotKeys();

            if (_settings.hotKeys.Keys == null)
            {
                _settings.hotKeys.Keys = new ObservableCollection<HotKey>();
                foreach (HotKeyType v in Enum.GetValues(typeof(HotKeyType)))
                {
                    _settings.hotKeys.AddHotKey(new HotKey() { HotKeyType = v });
                }
            }

            if (_currentProfile.Tabs == null)
                _currentProfile.Tabs.Add(new ProfileTab() { TabTitle = "Axis 1" });

            settingsForm = new SettingsForm(ref _settings);
            settingsForm.OnHotKeyRequest += new EventHandler<HotKeyArgs>(settingsForm_OnHotKeyRequest);
            settingsForm.OnReset += new EventHandler<EventArgs>(settingsForm_OnReset);

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
            switch (e.PropertyName)
            {
                case "generalAutoStart":
                    if (_settings.generalAutoStart)
                    {
                        Utils.AddToStartup();
                    }
                    else
                    {
                        Utils.RemoveFromStartup();
                    }
                    break;
                case "globalSteamEnable":
                    if (_settings.globalSteamEnable && !lastEnableSaitek)
                    {
                        ThreadPool.QueueUserWorkItem(t => ConnectSteam());
                    }
                    else
                    {
                        ThreadPool.QueueUserWorkItem(t => DisconnectSteam());
                    }
                    break;
                case "saitekX52ProEnable":
                    if (_settings.saitekX52ProEnable)
                    {
                        ThreadPool.QueueUserWorkItem(t => ConnectSaitek());
                    }
                    else
                    {
                        ThreadPool.QueueUserWorkItem( t => DisconnectSaitek());
                    }
                    break;
                case "warThunderTrackAircraft":
                    if (_settings.warThunderTrackAircraft)
                    {
                        ThreadPool.QueueUserWorkItem(t => ConnectWarThunder());
                    }
                    else
                    {
                        ThreadPool.QueueUserWorkItem(t => DisconnectWarThunder());
                    }
                    break;
            }

        }

        public void ConnectWarThunder()
        {
            lock (lockWarThunder)
            {
                if (!_settings.warThunderTrackAircraft)
                {
                    Utils.SetProperty<Label, String>(labelCurrentAircraft, "Text", DISABLEDOPTION);
                    return;
                }
                else
                {
                    Utils.SetProperty<Label, String>(labelCurrentAircraft, "Text", WAITSPAWN);
                    if (currentAircraft == null)
                    {
                        currentAircraft = new WTAircraft();
                        currentAircraft.AircraftChange += new EventHandler<EventArgsString>(wtCurrentAircraft_AircraftChange);
                        currentAircraft.StartPoll();
                    }

                }
            }
        }

        void wtCurrentAircraft_AircraftChange(object sender, EventArgsString e)
        {
            if (!checkBoxBindAircraft.Checked)
            {
                Utils.SetProperty<Label, String>(labelCurrentAircraft, "Text", e.Value);
            }
            
            var bindedProfile = _profileManager.Profiles.FirstOrDefault(p => p.BindVehicle == currentAircraft.AircraftName && p.BindGame == currentAircraft.GameName);
            if (bindedProfile != null && bindedProfile.Title != _currentProfile.Title)
            {
                Debug.Print("Set profile on aircraft change {0} aircraft: {1}", bindedProfile.Title, currentAircraft.AircraftName);
                Utils.CallMethod<Form>(this, "SetCurrentProfile", bindedProfile.Title);
            }



        }
        public void DisconnectWarThunder()
        {
            lock (lockWarThunder)
            {
                if (currentAircraft != null)
                {
                    try
                    {
                        Utils.SetProperty<Label, String>(labelCurrentAircraft, "Text", DISABLEDOPTION);
                        currentAircraft.StopPoll();
                        currentAircraft = null;
                    }
                    catch { }
                }
            }
        }
        void ShowSettings()
        {            
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
                        Correction = axisEditor.Correction,
                        PreserveAxisRange = axisEditor.PreserveAxisRange,
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
            if (_steam != null && _steam.LoggedIn)
            {
                _settings.steamToken = _steam.Token;
            }

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
            Text = String.Format("{0} Dev. ver: {1}", Text, GetRunningVersion());
            ConnectSteam();
            ConnectSaitek();
            ConnectWarThunder();
            LoadSettings();

            _deviceManager = new DeviceManager();
            _deviceManager.OnJoystickList += new EventHandler<EventArgs>(deviceManager_OnJoystickList);
            _deviceManager.OnKeyboardList += new EventHandler<EventArgs>(deviceManager_OnKeyboardList);
            _deviceManager.OnMouseList += new EventHandler<EventArgs>(deviceManager_OnMouseList);


        }
        private void UpdateDevices()
        {
            SetupTesterContextMenus();
            SetupEditorComboBoxes();
            UpdateAxisBindings();
            UpdateCurveActions();
            UpdateTesterActions();
        }
        private void deviceManager_OnJoystickList(object sender, EventArgs e)
        {
            _deviceManager.Joysticks.ForEach(j => j.Acquire());
            UpdateDevices();
        }

        void deviceManager_OnMouseList(object sender, EventArgs e)
        {
            _deviceManager.Mouses.ForEach(m => m.Acquire());
            Utils.SetProperty<CheckBox,bool>(checkBoxHotKey, "Enabled", true);
            SetupHotKeyActions();
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

                        //
                        axisEditor.SourceControllers = _deviceManager.Joysticks.Where(d => d.Type == DeviceType.Physical).Select(d => d.Name).ToList();
                        axisEditor.DestinationControllers = _deviceManager.Joysticks.Where(d => d.Type == DeviceType.Virtual).Select(d => d.Name).ToList();
                        axisEditor.SourceAxis = DIUtils.AxisNames.ToList(); 
                        axisEditor.DestinationAxis = DIUtils.AxisNames.ToList();
                        axisEditor.OnChange += new EventHandler<EventArgs>(axisEditor_OnChange);
                        axisEditor.OnTrimChange += new EventHandler<EventArgs>(axisEditor_OnTrimChange);
                    }
                }
            }
            catch { 
            
            }
            
        }

        void axisEditor_OnTrimChange(object sender, EventArgs e)
        {
   
            if (currentAxisEditor.Correction != 0)
            {
                _currentProfile.Tabs[currentAxisEditor.Index] = currentAxisEditor;
                var srcDevName = currentAxisEditor.CurrentSourceDevice;
                if (String.IsNullOrEmpty(srcDevName))
                    return;

                var srcDevice = _deviceManager.Joysticks.Where(d => d.Name == srcDevName).FirstOrDefault();
                if (srcDevice == null)
                    return;

                var srcAxis = DIUtils.ID(currentAxisEditor.CurrentSourceAxis);

                ActionSetVJoy(new DirectInputData()
                {
                    JoystickOffset = srcAxis,
                    DeviceName = currentAxisEditor.CurrentSourceDevice,
                    Type = DIDataType.Joystick,
                    Min = -32767,
                    Max = 32767,
                    Value = srcDevice.Get(srcAxis)

                });
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
                                        Type = DIDataType.Joystick,
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
                                        Type = DIDataType.Joystick,
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

            var axisEditor = tabAxis.SelectedTab.Controls[0] as AxisEditor;
            currentAxisEditor = axisEditor;
            if (axisEditor.CurveResponseType == CurveResponseType.Multiplier)
            {
                value.Checked = false;
                multiplier.Checked = true;
            }
            else
            {
                multiplier.Checked = false;
                value.Checked = true;
            }
        }

        private void axisEditor_OnChange(object sender, EventArgs e)
        {
            var axisEditor = sender as AxisEditor;
            var curText = Utils.GetProperty<TabPage>((TabPage)axisEditor.Parent, "Text").ToString();
            if( curText != axisEditor.CurrentSourceAxis )
                Utils.SetProperty<TabPage,String>( (TabPage)axisEditor.Parent, "Text",axisEditor.CurrentSourceAxis );

            var currentProfileName = _currentProfile.Title;
            _currentProfile.Tabs[axisEditor.Index] = axisEditor;

            UpdateAxisBindings();
            UpdateCurveActions();

            if (axisEditor.CurveResponseType == CurveResponseType.Multiplier && !multiplier.Checked)
            {
                value.Checked = false;
                multiplier.Checked = true;
            }
            else if (!value.Checked)
            {
                multiplier.Checked = false;
                value.Checked = true;
            }
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
                axisEditor.CurveResponseType = CurveResponseType.Multiplier;                
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
                Index = tabAxis.TabPages.Count - 1
                //Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };
            newAxisEditor.CurveResponseType = CurveResponseType.Multiplier;
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
        public void SetCurrentProfile(String title)
        {
            SetCurrentProfile(title, false);
        }
        public void SetCurrentProfile(String title, bool force)
        {
            lock (lockSetProfile)
            {
                var selectedItem = Utils.GetProperty<ComboBox>(comboProfiles, "SelectedItem");

                if (selectedItem != null && selectedItem.ToString() == _currentProfile.Title && _currentProfile == title && !force)
                    return;

                if (string.IsNullOrEmpty(title))
                {
                    Utils.SetProperty<ComboBox, String>(comboProfiles, "SelectedItem", _currentProfile.Title);
                    return;
                }
                _suspendTabActions = true;
                this.SuspendLayout();

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
                    axisEditor.PreserveAxisRange = p.PreserveAxisRange;
                    axisEditor.Correction = p.Correction;
                    axisEditor.Title = String.IsNullOrEmpty(p.TabTitle) || p.TabTitle == NOTSET ? String.Format("Axis {0}", _currentProfile.Tabs.IndexOf(p) + 1) : p.TabTitle;
                    axisEditor.CurveResponseType = p.CurvePoints.CurveResponseType;

                    tabPage.Text = axisEditor.Title;
                }
                try
                {
                    if (currentAircraft != null &&
                        !String.IsNullOrEmpty(_currentProfile.BindGame) &&
                        !String.IsNullOrEmpty(_currentProfile.BindVehicle) &&
                        _currentProfile.BindVehicle == currentAircraft.AircraftName &&
                        _currentProfile.BindGame == currentAircraft.GameName)
                    {
                        Utils.SetProperty<Label, String>(labelCurrentAircraft, "Text", _currentProfile.BindVehicle);
                        Utils.SetProperty<CheckBox, Boolean>(checkBoxBindAircraft, "Checked", true);
                    }
                    else if( currentAircraft != null )
                    {
                        if (!String.IsNullOrEmpty(currentAircraft.AircraftName))
                            Utils.SetProperty<Label, String>(labelCurrentAircraft, "Text", currentAircraft.AircraftName);
                        else
                            Utils.SetProperty<Label, String>(labelCurrentAircraft, "Text", WAITSPAWN);

                        Utils.SetProperty<CheckBox, Boolean>(checkBoxBindAircraft, "Checked", false);
                    }
                }
                catch { }


                var hotkey = "Hot Key";
                if (!String.IsNullOrEmpty(_currentProfile.HotKeyJoystickName))
                {
                    hotkey = "Joy: " + _currentProfile.JoystickHotKey;
                }
                else if (!String.IsNullOrEmpty(_currentProfile.HotKeyKeyboardName))
                {
                    hotkey = "Key: " + _currentProfile.KeyboardHotKey;
                }
                else if (!String.IsNullOrEmpty(_currentProfile.HotKeyMouseName))
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
                    profilesToolStripMenuItem.SelectedIndexChanged += new EventHandler(profilesToolStripMenuItem_SelectedIndexChanged);
                }

                if (_settings.saitekX52ProEnable && _saitek != null && _saitek.Acquired)
                    ThreadPool.QueueUserWorkItem( t => _saitek.SetText(1, 0, _currentProfile.Title));

                if( _settings.globalSteamEnable && _steam != null && _steam.LoggedIn) 
                    SendSteamMessage(_currentProfile.Title);

                SetupEditorComboBoxes();
                this.ResumeLayout();
            }
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
            System.IO.File.WriteAllText(@"C:\JoystickCurvesCrash.txt", errorMsg);
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
            this.Activate();
        }
        private void Exit()
        {
            UnacquireDevices();
            DisconnectSaitek();
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
            if( settingsForm == null )
            {
                settingsForm = new SettingsForm(ref _settings);

                settingsForm.OnReset +=new EventHandler<EventArgs>(settingsForm_OnReset);
            }

            settingsForm.Show();
            settingsForm.Activate();
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

        private bool ProfileComboFocused
        {
            get;set;
        }
        private void ActionKeyboardProfileHotKey(DirectInputData data)
        {
            lock (lockHotKeyboard)
            {
                if (ProfileComboFocused)
                    return;

                var profile = _profileManager.Profiles.FirstOrDefault(
                    p => p.HotKeyKeyboardName == data.DeviceName && p.KeyboardHotKey == data.KeyboardKey.ToString());

                if (profile != null && _currentProfile.Title != profile.Title)
                    Utils.CallMethod<Form>(this, "SetCurrentProfile", profile.Title);
            }
        }
        private void ActionJoystickProfileHotKey(DirectInputData data)
        {
            lock (lockHotJoystick)
            {
                if (ProfileComboFocused)
                    return;

                var profile = _profileManager.Profiles.FirstOrDefault(
                    p => p.HotKeyJoystickName == data.DeviceName && p.JoystickHotKey == data.JoystickOffset.ToString());

                if (profile != null && _currentProfile.Title != profile.Title)
                    Utils.CallMethod<Form>(this, "SetCurrentProfile", profile.Title);
            }

        }
        private void ActionMouseProfileHotKey(DirectInputData data)
        {
            lock (lockHotMouse)
            {
                if (ProfileComboFocused)
                    return;

                var profile = _profileManager.Profiles.FirstOrDefault(
                    p => p.HotKeyMouseName == data.DeviceName && p.MouseHotKey == data.MouseOffset.ToString());

                if (profile != null && _currentProfile.Title != profile.Title)
                    Utils.CallMethod<Form>(this, "SetCurrentProfile", profile.Title);
            }
        }

        private void SetupProfileHotKeyActions(Profile profile)
        {
            if (!String.IsNullOrEmpty(profile.HotKeyKeyboardName) &&
                !String.IsNullOrEmpty(profile.KeyboardHotKey))
            {
                var dev = _deviceManager.Keyboards.FirstOrDefault(d => d.Name == profile.HotKeyKeyboardName);
                if (dev != null)
                {
                    Key key;
                    Enum.TryParse<Key>(profile.KeyboardHotKey, out key);

                    dev.DeleteActions( key );
                    dev.AddAction( key, ActionKeyboardProfileHotKey);
                }
            }
            else if (!String.IsNullOrEmpty(profile.HotKeyMouseName) &&
                     !String.IsNullOrEmpty(profile.MouseHotKey))
            {
                var dev = _deviceManager.Mouses.FirstOrDefault(d => d.Name == profile.HotKeyMouseName);
                if (dev != null)
                {
                    MouseOffset key;
                    Enum.TryParse<MouseOffset>(profile.MouseHotKey, out key);

                    dev.DeleteActions(key);
                    dev.AddAction(key, ActionMouseProfileHotKey);
                }
            }
            else if (!String.IsNullOrEmpty(profile.HotKeyJoystickName) &&
                     !String.IsNullOrEmpty(profile.JoystickHotKey))
            {
                var dev = _deviceManager.Joysticks.FirstOrDefault(d => d.Name == profile.HotKeyJoystickName);
                if (dev != null)
                {
                    JoystickOffset key;
                    Enum.TryParse<JoystickOffset>(profile.JoystickHotKey, out key);

                    dev.DeleteActions(key);
                    dev.AddAction(key, ActionJoystickProfileHotKey);
                }
            }
        }
        private void SetupHotKeyActions()
        {
            foreach (var profile in _profileManager.Profiles)
            {
                SetupProfileHotKeyActions(profile);
            }
        }

        void joystick_OnButtonDown(object sender, CustomEventArgs<DirectInputData> e)
        {
            DirectInputJoystick gController = sender as DirectInputJoystick;

            _currentProfile.JoystickHotKey = e.Data.JoystickOffset.ToString();
            _currentProfile.HotKeyJoystickName = e.Data.DeviceName;

            Utils.SetProperty<CheckBox, String>(checkBoxHotKey, "Text", "Joy: " + e.Data.JoystickOffset.ToString());
            Utils.SetProperty<Form, bool>(this, "WaitingHotKey", false);

            SetupProfileHotKeyActions(_currentProfile);
        }

        void mouse_OnButtonDown(object sender, CustomEventArgs<DirectInputData> e)
        {
            DirectInputMouse mouse = sender as DirectInputMouse;
            _currentProfile.MouseHotKey = e.Data.MouseOffset.ToString();
            _currentProfile.HotKeyMouseName = e.Data.DeviceName;
            Utils.SetProperty<CheckBox, String>(checkBoxHotKey, "Text", "Mouse: " + e.Data.MouseOffset.ToString());
            Utils.SetProperty<Form, bool>(this, "WaitingHotKey", false);

            SetupProfileHotKeyActions(_currentProfile);
        }

        void keyboard_OnKeyDown(object sender, CustomEventArgs<DirectInputData> e)
        {
            DirectInputKeyboard keyboard = sender as DirectInputKeyboard;
            _currentProfile.KeyboardHotKey = e.Data.KeyboardKey.ToString();
            _currentProfile.HotKeyKeyboardName = e.Data.DeviceName;
            Utils.SetProperty<CheckBox, String>(checkBoxHotKey, "Text", "Key: " + e.Data.KeyboardKey.ToString());
            Utils.SetProperty<Form, bool>(this, "WaitingHotKey", false);

            SetupProfileHotKeyActions(_currentProfile);
        }

        private void CleanUpProfileHotKeys(Profile profile)
        {
            if (profile != null)
            {
                profile.KeyboardHotKey = String.Empty;
                profile.HotKeyKeyboardName = String.Empty;
                profile.MouseHotKey = String.Empty;
                profile.HotKeyMouseName = String.Empty;
                profile.JoystickHotKey = String.Empty;
                profile.HotKeyJoystickName = String.Empty;
            }
        }

        private void SetupWaitHotKeyHandlers()
        {
            CleanUpProfileHotKeys(_currentProfile);
            CleanUpProfileHotKeys(_profileManager.Profiles.FirstOrDefault(p => p.Title == _currentProfile));

            checkBoxHotKey.Text = PRESSKEY;

            foreach (var joy in _deviceManager.Joysticks.Where(d => d.Type == DeviceType.Physical))
                joy.OnButtonDown += new EventHandler<CustomEventArgs<DirectInputData>>(joystick_OnButtonDown);
            _deviceManager.Keyboards.ForEach(keyboard => keyboard.OnKeyDown += new EventHandler<CustomEventArgs<DirectInputData>>(keyboard_OnKeyDown));
            _deviceManager.Mouses.ForEach(mouse => mouse.OnButtonDown += new EventHandler<CustomEventArgs<DirectInputData>>(mouse_OnButtonDown));
        }

        private void RemoveWaitHotKeyHandlers()
        {
            foreach (var joy in _deviceManager.Joysticks.Where(d => d.Type == DeviceType.Physical))
                joy.OnButtonDown -= joystick_OnButtonDown;
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

        private Version GetRunningVersion()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            else
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        private void comboProfiles_Enter(object sender, EventArgs e)
        {
            ProfileComboFocused = true;
        }

        private void comboProfiles_Leave(object sender, EventArgs e)
        {
            ProfileComboFocused = false;
        }

        private int curX = 32767;
        private void timer1_Tick(object sender, EventArgs e)
        {
            return;
/*            var virtualDevice = _deviceManager.Joysticks.ToList().FirstOrDefault(
                gc => gc.Type == DeviceType.Virtual );

            if (virtualDevice == null)
                return;

            curX -= 5000;
            if (curX <= -32767)
                curX = 32767;
            virtualDevice.Set(JoystickOffset.X, curX);*/
        }

        private void multiplerToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void valueToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void multiplerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Current curve will be reset! Continue ?", "Change response modifier", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
            //    return;


            var currentItem = sender as ToolStripMenuItem;

            var parent = currentItem.GetCurrentParent();
            foreach (ToolStripMenuItem item in parent.Items)
                if (!item.Equals(currentItem))
                    item.Checked = false;

            var selName = currentItem.Name.ToLower();
            switch (selName)
            {
                case "value":
                {
                    multiplier.Checked = false;
                    var axisEditor = tabAxis.SelectedTab.Controls[0] as AxisEditor;
                    axisEditor.CurveResponseType = CurveResponseType.Values;
                    //axisEditor.ResetCurve();
                }
                break;
                case "multiplier":
                {
                    value.Checked = false;
                    var axisEditor = tabAxis.SelectedTab.Controls[0] as AxisEditor;
                    axisEditor.CurveResponseType = CurveResponseType.Multiplier;
                    //axisEditor.ResetCurve();
                }
                break;
            }           
        }

        private void checkBoxBindAircraft_CheckedChanged(object sender, EventArgs e)
        {            
            var checkbox = sender as CheckBox;
            var label = labelCurrentAircraft;

            if (checkbox.Checked)
            {
                if (_currentProfile.BindVehicle != currentAircraft.AircraftName ||
                    _currentProfile.BindGame != currentAircraft.GameName)
                {
                    _profileManager.Profiles.ForEach(p =>
                    {
                        if (!String.IsNullOrEmpty(p.BindGame) && p.BindGame == currentAircraft.GameName &&
                            !String.IsNullOrEmpty(p.BindVehicle) && p.BindVehicle == currentAircraft.AircraftName)
                        {
                            p.BindGame = String.Empty;
                            p.BindVehicle = String.Empty;
                        }
                    });
                    _currentProfile.BindVehicle = currentAircraft.AircraftName;
                    _currentProfile.BindGame = currentAircraft.GameName;
                }
            }
            else
            {
                Utils.SetProperty<Label, String>(labelCurrentAircraft, "Text", currentAircraft.AircraftName);
            }
        }
/*
        private string[] aircrafts = { "Aircraft 1", "Aircraft 2", "Aircraft 3" };
        private int airIndex = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (currentAircraft == null)
                currentAircraft = new WTAircraft();

            currentAircraft.AircraftName = aircrafts[airIndex++];
            if (airIndex >= aircrafts.Length)
            {
                airIndex = 0;
            }
            Debug.Print(currentAircraft.AircraftName);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            currentAircraft.AircraftName = aircrafts[airIndex--];
            if (airIndex < 0)
            {
                airIndex = aircrafts.Length - 1;
            }
            Debug.Print(currentAircraft.AircraftName);
        }
 */
    }

}
