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
        private BindingSource _testBSource;
        private Properties.Settings _settings;
        private ProfileManager _profileManager;
        private BindingSource _axisNamesPhysPitch, _axisNamesVirtPitch, _axisNamesPhysYaw, _axisNamesVirtYaw;
        public MainForm()
        {
            _settings = Properties.Settings.Default;

            InitializeComponent();
            if (_settings.Profiles != null)
            {
                _profileManager = _settings.Profiles;
            }
            
            if( _profileManager == null )
            {
                _profileManager = new ProfileManager();
            }


        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            _profileManager.Profiles.Add(new Profile("test"));
            _settings.CurrentProfile = new Profile("Default");
            
            _profileManager = new ProfileManager();
            _settings.Profiles = _profileManager;
            _settings.Save();

            if (_vjoy == null)
                return;

            if (!_vjoy.Enabled)
                return;

            _vjoy.Reset();

            foreach (var d in _deviceManager.Devices)
                d.Unacquire();

            var p = DIUtils.AllNames[JoystickOffset.X];

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
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
            axisEditor.SourceControllers = _deviceManager.Devices.Where(d => d.Type == GameControllerType.Virtual).Select( d=>d.Name).ToList();
            axisEditor.DestinationControllers = _deviceManager.Devices.Where(d => d.Type == GameControllerType.Physical).Select( d=>d.Name).ToList();
            //var vv = DIUtils.AxisNames.Select(a => new Axis() { Name = a }).ToList();
            axisEditor.SourceAxis = DIUtils.AxisNames.ToList();
            axisEditor.DestinationAxis = DIUtils.AxisNames.ToList();
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
            Debug.Print(e.Data.Value.ToString());
        }

        void dev_OnAxisChange(object sender, CustomEventArgs<Axis> e)
        {
            if (e.Data.DirectInputID == JoystickOffset.X)
            {
                Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisRoll", e.Data);                   
            }
            else if (e.Data.DirectInputID == JoystickOffset.Y)
            {
                Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisPitch", e.Data);
            }
            else if (e.Data.DirectInputID == JoystickOffset.RZ)
            {
                Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisRudder", e.Data);                  

            }
        }

        private void tabAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = sender as TabControl;
            if (tab.SelectedTab.Name == "tabAddNew")
            {
                AddNewProfileTab(tab);
            }
        }

        private void AddNewProfileTab(TabControl tab)
        {
            var newTabPage = new TabPage("Axis " + tab.TabCount);
            newTabPage.Controls.Add(new AxisEditor() { Location = new Point(tab.TabPages[0].Controls[0].Location.X, tab.TabPages[0].Controls[0].Location.Y), Size = tab.TabPages[0].Controls[0].Size });

            tabAxis.TabPages.Insert(tabAxis.TabPages.Count - 1, newTabPage);

            tabAxis.SelectedTab = newTabPage;
            Invalidate();
        }


    }

}
