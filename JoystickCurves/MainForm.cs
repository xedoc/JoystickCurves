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
        private BindingSource _axisNamesPhysPitch, _axisNamesVirtPitch, _axisNamesPhysYaw, _axisNamesVirtYaw;
        private Profile _currentProfile;

        public MainForm()
        {

           
            InitializeComponent();
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
                _currentProfile.Tabs[0].CurvePoints.PointsCount = 13;
            }
            else
                _currentProfile = _settings.CurrentProfile;

            if (_currentProfile.Tabs == null)
                _currentProfile.Tabs.Add(new ProfileTab() { TabTitle = "Axis 1" });
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

            SetProfile(_currentProfile);

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


        }
        void SetupTesterComboBoxes()
        {
            var axisList = (from d in _deviceManager.Devices.Select(d => d.Name)
                            from a in DIUtils.AxisNames
                            select d + ": " + a).ToList();

            axisList.Insert(0, "Not set");
            axisList.Insert(0, "Any");


            _axisNamesPhysPitch = new BindingSource();
            _axisNamesPhysYaw = new BindingSource();
            _axisNamesVirtPitch = new BindingSource();
            _axisNamesVirtYaw = new BindingSource();
            
            _axisNamesPhysPitch.DataSource = new List<String>(axisList);
            _axisNamesPhysYaw.DataSource = new List<String>(axisList);
            _axisNamesVirtPitch.DataSource = new List<String>(axisList);
            _axisNamesVirtYaw.DataSource = new List<String>(axisList);

            Utils.SetComboDataSource(comboPhysPitch, _axisNamesPhysPitch);
            Utils.SetComboDataSource(comboPhysYaw, _axisNamesPhysYaw);
            Utils.SetComboDataSource(comboVirtPitch, _axisNamesVirtPitch);
            Utils.SetComboDataSource(comboVirtYaw, _axisNamesVirtYaw);
        }

        void SetupEditorComboBoxes()
        {

            foreach (TabPage tp in tabAxis.TabPages)
            {
                if (tp.Controls.Count > 0)
                {
                    AxisEditor axisEditor = tp.Controls[0] as AxisEditor;
                    axisEditor.SourceControllers = _deviceManager.Devices.Where(d => d.Type == GameControllerType.Physical).Select( d=>d.Name).ToList();
                    axisEditor.DestinationControllers = _deviceManager.Devices.Where(d => d.Type == GameControllerType.Virtual).Select( d=>d.Name).ToList();
                    axisEditor.SourceAxis = DIUtils.AxisNames.ToList();
                    axisEditor.DestinationAxis = DIUtils.AxisNames.ToList();
                    axisEditor.OnChange += new EventHandler<EventArgs>(axisEditor_OnChange);
                }
            }
        }

        void axisEditor_OnChange(object sender, EventArgs e)
        {
            var axisEditor = sender as AxisEditor;
            axisEditor.Parent.Text = axisEditor.CurrentSourceAxis;
        }
        void deviceManager_OnDeviceList(object sender, EventArgs e)
        {
            foreach (var dev in _deviceManager.Devices)
            {
                dev.OnAxisChange += new EventHandler<CustomEventArgs<Axis>>(dev_OnAxisChange);
                dev.OnButtonChange += new EventHandler<CustomEventArgs<Button>>(dev_OnButtonChange);
                dev.Acquire();
            }

            SetupTesterComboBoxes();
            SetupEditorComboBoxes();
        }

        void dev_OnButtonChange(object sender, CustomEventArgs<Button> e)
        {
        }

        void dev_OnAxisChange(object sender, CustomEventArgs<Axis> e)
        {
            if (e.Data.DeviceName.ToLower().Contains("vjoy"))
            {
                if (e.Data.DirectInputID == JoystickOffset.X)
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "VirtualAxisRoll", e.Data);
                }
                else if (e.Data.DirectInputID == JoystickOffset.Y)
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "VirtualAxisPitch", e.Data);
                }
                else if (e.Data.DirectInputID == JoystickOffset.RZ)
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "VirtualAxisRudder", e.Data);

                }

                return;
            }

            var devName = e.Data.DeviceName;
            var axisName = DIUtils.Name(e.Data.DirectInputID);

            BezierCurvePoints curvePoints = _currentProfile.Tabs.Where(t => t.SourceDevice == devName && t.SourceAxis == axisName).Select( t => t.CurvePoints).FirstOrDefault();
                
            if (e.Data.DirectInputID == JoystickOffset.X)
            {
                Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisRoll", e.Data);
                //_vjoy.Axis.X.Set((int)(e.Data.Value * multipler));
            }
            else if (e.Data.DirectInputID == JoystickOffset.Y)
            {
                ThreadPool.QueueUserWorkItem(arg=> Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisPitch", e.Data));
                if (curvePoints != null)
                {
                    var multipler = 1 - curvePoints.GetY(Utils.PTop(1, e.Data.Value + 32767, 65534));
                    //Debug.Print("{0}", multipler);
                    _vjoy.Axis.Y.Set((e.Data.Value + 32767)/2);
                    //_vjoy.Axis.Y.Set((int)(e.Data.Value * multipler));
                }
            }
            else if (e.Data.DirectInputID == JoystickOffset.RZ)
            {
                Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisRudder", e.Data);
                //_vjoy.Axis.RZ.Set((int)(e.Data.Value * multipler));

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
                curve.PointsCount = 13;
                axisEditor.CurrentCurve = curve;
                SetupEditorComboBoxes();
            }
        }

        private TabPage AddNewProfileTab(TabControl tab, bool select = false)
        {
            var templateTab = tabAxis.TabPages[tabAxis.TabPages.Count-1];
            var newTabPage = new TabPage("Axis " + tab.TabCount);

            newTabPage.Controls.Add(new AxisEditor() { 
                Location = new Point( templateTab.Padding.Left, templateTab.Padding.Top ), 
                Size = new Size(templateTab.Width - Padding.Left - Padding.Right, templateTab.Height - Padding.Top - Padding.Bottom)
            });

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


    }

}
