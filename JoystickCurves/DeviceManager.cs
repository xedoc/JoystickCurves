using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Threading;
using System.Diagnostics;

namespace JoystickCurves
{
    
    public class DeviceManager
    {
        public event EventHandler<EventArgs> OnDeviceList;
        private String[] virtualTags = new String[] { "vjoy" };
        private Timer _pollTimer;
        private const int POLLINTERVAL = 1000;
        public DeviceManager()
        {
            _pollTimer = new Timer(new TimerCallback(poll_Tick), null, 0, POLLINTERVAL);           
        }

        private void poll_Tick(object o)
        {
            _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);
            RefreshDeviceList();
            _pollTimer.Change(POLLINTERVAL, POLLINTERVAL);
        }

        public void RefreshDeviceList()
        {
            List<DeviceInstance> deviceInstances;
            deviceInstances= Utils.DevList(Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly));

            if (Devices == null)
                Devices = new List<GameController>();

            Devices.Where(dev => !deviceInstances.Exists(d => dev.Guid == d.InstanceGuid)).ToList().ForEach(
                gc => { gc.Unacquire(); Devices.Remove(gc); }
            );
           
            var addList = deviceInstances.Where(d => !Devices.Exists(dev => dev.Guid == d.InstanceGuid));
            
            if (addList == null || addList.Count() <= 0)
                return;

            foreach (DeviceInstance dev in addList)
            {                
                var gameController = new GameController(dev, GetDeviceType(dev.ProductName));
                
                if (gameController.Type == GameControllerType.Virtual)
                {
                    gameController.OnButtonChange += new EventHandler<CustomEventArgs<JoystickData>>(gameController_OnButtonChange);
                }
                
                Devices.Add(gameController);
                if (Devices.Where(d => d.Name.StartsWith(gameController.Name)).Count() > 1)
                {
                    Devices.Where(d => d.Name.StartsWith(gameController.Name) && d.Index == 0).ToList().ForEach(
                            d => d.Index = Devices.Max(dm => dm.Index) + 1
                        );
                }
            }

            for (uint i = 1; i <= 16; i++)
            {
                var vjoy = new VirtualJoystick(i);
                vjoy.OnAcquire += new EventHandler<EventArgs>(vjoy_OnAcquire);
                vjoy.Acquire();
            }

            if (OnDeviceList != null)
                OnDeviceList(this, EventArgs.Empty);
        }

        void vjoy_OnAcquire(object sender, EventArgs e)
        {
            VirtualJoystick vjoy = sender as VirtualJoystick;
            vjoy.SetButton(vjoy.DeviceID, true);
        }

        void gameController_OnButtonChange(object sender, CustomEventArgs<JoystickData> e)
        {
            GameController device = Devices.Where( d => d.Name == e.Data.DeviceName).FirstOrDefault();
            if (device == null)
                return;

            var i = Devices.IndexOf(device);
            var devName = device.Name;
            var deviceId = (uint)e.Data.DirectInputID - (uint)JoystickOffset.Button0 + 1;
            
            Devices[i].VirtualJoystick = new VirtualJoystick(deviceId);

            device.OnButtonChange -= gameController_OnButtonChange;
            device.VirtualJoystick.SetButton(device.VirtualJoystick.DeviceID, false);           
        }

        private GameControllerType GetDeviceType( string name )
        {
            return virtualTags.Where(vt => name.ToLower().Contains(vt)).Count() > 0 ? GameControllerType.Virtual : GameControllerType.Physical;
        }

        public List<GameController> Devices
        {
            get;
            set;
        }
    }

}
