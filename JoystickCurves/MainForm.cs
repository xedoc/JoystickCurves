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
namespace JoystickCurves
{
    public partial class MainForm : Form
    {
        private VirtualJoystick vjoy;
        private DeviceManager deviceManager;
        private BindingSource testBSource;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {            
            if (vjoy == null)
                return;

            if (!vjoy.Enabled)
                return;

            vjoy.Reset();             
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                vjoy = new VirtualJoystick(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }

            deviceManager = new DeviceManager();
            deviceManager.OnDeviceList += new EventHandler<EventArgs>(deviceManager_OnDeviceList);

        }

        void deviceManager_OnDeviceList(object sender, EventArgs e)
        {
            foreach (var dev in deviceManager.Devices)
            {
                dev.OnAxisChange += new EventHandler<CustomEventArgs<Axis>>(dev_OnAxisChange);
                dev.OnButtonChange += new EventHandler<CustomEventArgs<Button>>(dev_OnButtonChange);
                dev.Acquire();
            }
            axisEditor1.VirtualControllers = deviceManager.Devices.Where(d => d.Type == GameControllerType.Virtual).ToList();
            axisEditor1.PhysicalControllers = deviceManager.Devices.Where(d => d.Type == GameControllerType.Physical).ToList();
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
                var newTabPage = new TabPage("Axis " + tab.TabCount);
                newTabPage.Controls.Add(new AxisEditor() { Location = new Point(tab.TabPages[0].Controls[0].Location.X, tab.TabPages[0].Controls[0].Location.Y),Size = tab.TabPages[0].Controls[0].Size });

                tabAxis.TabPages.Insert(tabAxis.TabPages.Count - 1, newTabPage);

                tabAxis.SelectedTab = newTabPage;
                Invalidate();
            }
        }


    }

}
