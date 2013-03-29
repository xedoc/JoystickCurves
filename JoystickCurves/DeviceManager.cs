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
