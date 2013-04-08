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
        private DeviceList _gameControllers;
        private String[] virtualTags = new String[] { "vjoy" };      

        public DeviceManager()
        {
            ThreadPool.QueueUserWorkItem(f => RefreshDeviceList());
        }

        public void RefreshDeviceList()
        {
            _gameControllers= Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);

            if (Devices == null)
                Devices = new List<GameController>();
            else
                Devices.Clear();

            foreach (DeviceInstance dev in _gameControllers)
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

            if (OnDeviceList != null)
                OnDeviceList(this, EventArgs.Empty);

            for (uint i = 1; i <= 16; i++)
            {
                var vjoy = new VirtualJoystick(i);
                vjoy.OnAcquire += new EventHandler<EventArgs>(vjoy_OnAcquire);
                vjoy.Acquire();
            }
        }

        void vjoy_OnAcquire(object sender, EventArgs e)
        {
            VirtualJoystick vjoy = sender as VirtualJoystick;
            vjoy.SetButton(vjoy.DeviceID, true);
        }

        void gameController_OnAcquire(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void gameController_OnButtonChange(object sender, CustomEventArgs<JoystickData> e)
        {
            GameController device = Devices.Where( d => d.Name == e.Data.DeviceName).FirstOrDefault();
            if (device == null)
                return;

            var i = Devices.IndexOf(device);

            if (device.VirtualJoystick != null)
            {
                Devices[i].OnButtonChange -= gameController_OnButtonChange;
                return;
            }

            var devName = device.Name;

            var deviceId = (uint)e.Data.DirectInputID - (uint)JoystickOffset.Button0 + 1;
            Devices[i].VirtualJoystick = new VirtualJoystick(deviceId);
            var b = Devices[i].VirtualJoystick.SetButton(deviceId, false);
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
