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
                var gc = new GameController(
                    dev.InstanceName,
                    dev.ProductName,
                    dev.ProductGuid,
                    dev.InstanceGuid,
                    GetDeviceType(dev.ProductName)
                );
                gc.OnAcquire += new EventHandler<EventArgs>(gc_OnAcquire);
                gc.Acquire();
                Devices.Add(gc);

            }
        }

        void gc_OnAcquire(object sender, EventArgs e)
        {
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
