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

            for (var i = 0.0f; i <= 1.0f; i += 0.01f)
            {
                Debug.Print("{0:0.00}: {1:0.00}", i, _currentProfile.Tabs[0].CurvePoints.GetY(i));            
            }
            //TestAnimation();

        }
        void TestAnimation()
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
                    axisEditor.SourceControllers  = _deviceManager.Devices.Where(d => d.Type == GameControllerType.Physical).Select(d => d.Name).ToList();
                    axisEditor.DestinationControllers = _deviceManager.Devices.Where(d => d.Type == GameControllerType.Virtual).Select( d=>d.Name).ToList();
                    axisEditor.SourceAxis = DIUtils.AxisNames.ToList();
                    axisEditor.DestinationAxis = DIUtils.AxisNames.ToList();
                    axisEditor.OnChange += new EventHandler<EventArgs>(axisEditor_OnChange);
                }
            }
        }

        void SetupTabContextMenu()
        {
            var axisList = DIUtils.AxisNames.ToList().Except(new string[] {tabAxis.SelectedTab.Text}).ToList();
            copyCurveToToolStripMenuItem.DropDownItems.Clear();
            foreach( var a in axisList )
                copyCurveToToolStripMenuItem.DropDownItems.Add(a);
        }

        void axisEditor_OnChange(object sender, EventArgs e)
        {
            var axisEditor = sender as AxisEditor;
            Utils.SetProperty<TabPage,String>( (TabPage)axisEditor.Parent, "Text",axisEditor.CurrentSourceAxis );
            var currentProfileName = _currentProfile.Title;
            _currentProfile.Tabs[axisEditor.Index] = axisEditor;
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
                    Utils.SetProperty<Label, String>(labelRollPercent, "Text", e.Data.PercentValue.ToString() + "%");
                }
                else if (e.Data.DirectInputID == JoystickOffset.Y)
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "VirtualAxisPitch", e.Data);
                    Utils.SetProperty<Label, String>(labelPitchPercent, "Text", e.Data.PercentValue.ToString() + "%");
                }
                else if (e.Data.DirectInputID == JoystickOffset.RZ)
                {
                    Utils.SetProperty<JoystickTester, Axis>(joystickTester, "VirtualAxisRudder", e.Data);
                    Utils.SetProperty<Label,String>(labelYawPercent,"Text", e.Data.PercentValue.ToString() + "%");
                }

                return;
            }

            var devName = e.Data.DeviceName;
            var axisName = DIUtils.Name(e.Data.DirectInputID);
            BezierCurvePoints curvePoints = _currentProfile.Tabs.Where(t => t.SourceDevice == devName && t.SourceAxis == axisName).Select( t => t.CurvePoints).FirstOrDefault();
            float multiplier = 1.0f;
            if (curvePoints != null)
                multiplier = curvePoints.GetY(Utils.PTop(1, Math.Abs(e.Data.Value), 32767));

                
            if (e.Data.DirectInputID == JoystickOffset.X)
            {
                Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisRoll", e.Data);
                Debug.Print("{0:0.00} {1:0.00}", multiplier, (float)e.Data.Value * multiplier);
                _vjoy.X = (int)((float)e.Data.Value * multiplier);
            }
            else if (e.Data.DirectInputID == JoystickOffset.Y)
            {
                Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisPitch", e.Data);
                _vjoy.Y = (int)((float)e.Data.Value * multiplier);
            }
            else if (e.Data.DirectInputID == JoystickOffset.RZ)
            {
                Utils.SetProperty<JoystickTester, Axis>(joystickTester, "PhysicalAxisRudder", e.Data);
                _vjoy.RZ = (int)((float)e.Data.Value * multiplier);
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
                _currentProfile.Tabs.Add(axisEditor);
            }
            SetupTabContextMenu();
        }

        private TabPage AddNewProfileTab(TabControl tab, bool select = false)
        {
            var templateTab = tabAxis.TabPages[tabAxis.TabPages.Count-1];
            var newTabPage = new TabPage("Axis " + tab.TabCount);

            newTabPage.Controls.Add(new AxisEditor() { 
                Location = new Point( templateTab.Padding.Left, templateTab.Padding.Top ), 
                Size = new Size(templateTab.Width - Padding.Left - Padding.Right, templateTab.Height - Padding.Top - Padding.Bottom),
                Index = tabAxis.TabPages.Count - 1
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

        private void copyCurveToToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var title = e.ClickedItem.Text;
            var axisEditor = tabAxis.SelectedTab.Controls[0] as AxisEditor;
            var tab = _currentProfile.Tabs.Where(t => t.TabTitle == title).FirstOrDefault();
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


    }

}
