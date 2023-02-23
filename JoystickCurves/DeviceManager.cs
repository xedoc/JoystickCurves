using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.DirectInput;
using System.Threading;
using System.Diagnostics;

namespace JoystickCurves
{
    
    public class DeviceManager
    {
        public event EventHandler<EventArgs> OnJoystickList;
        public event EventHandler<EventArgs> OnKeyboardList;
        public event EventHandler<EventArgs> OnMouseList;
        public object pollLock = new object();
        public object bindVirtualJoyLock = new object();
        private String[] virtualTags = new String[] { "vjoy" };
        private Timer _pollTimer;
        private const int POLLINTERVAL = 3000;
        private HotKey _currentHotkey;
        private object lockRefreshJoysticks = new object();
        private object lockRefreshMouses = new object();
        private object lockRefreshKeyboards= new object();
        private Properties.Settings _settings;
        public DeviceManager()
        {
            _settings = Properties.Settings.Default;
            Joysticks = new List<DirectInputJoystick>();
            Mouses = new List<DirectInputMouse>();
            Keyboards = new List<DirectInputKeyboard>();

            _pollTimer = new Timer(new TimerCallback(poll_Tick), null, 0, POLLINTERVAL);           
        }

        private void poll_Tick(object o)
        {
            lock (pollLock)
            {
                RefreshJoystickList();
                RefreshKeyboardList();
                RefreshMouseList();
            }
        }

        public void OnNoNeed(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(DirectInputJoystick) )
            {
                var joystick = sender as DirectInputJoystick;
                Debug.Print("Joystick {0} removed", ((DirectInputJoystick)sender).Name);

                if (joystick.Type == DeviceType.Virtual && joystick.VirtualJoystick != null)
                    joystick.VirtualJoystick.Unacquire();

                Joysticks.Remove(joystick);
            }
            else if( sender.GetType() == typeof(DirectInputKeyboard))
            {
                Debug.Print("Keyboard {0} removed", ((DirectInputKeyboard)sender).Name);
                Keyboards.Remove((DirectInputKeyboard)sender);
            }
            else if ( sender.GetType() == typeof(DirectInputMouse))
            {
                Debug.Print("Mouse {0} removed", ((DirectInputMouse)sender).Name);
                Mouses.Remove((DirectInputMouse)sender);
            }
        }
        public void SwitchExclusiveDirectInput(bool exclusive)
        {
            foreach (DirectInputJoystick joystick in Joysticks)
            {
                if (joystick.Type == DeviceType.Physical)
                {
                    joystick.ExclusiveMode = exclusive;
                }
            }
        }
        public void RefreshMouseList()
        {
            lock (lockRefreshMouses)
            {
                List<DeviceInstance> mouseInstances = null;

                if (Mouses == null)
                    Mouses = new List<DirectInputMouse>();
                try
                {
                    var dI = new DirectInput();
                    mouseInstances = Utils.DevList(dI.GetDevices(DeviceClass.Pointer, DeviceEnumerationFlags.AttachedOnly));
                }
                catch (Exception e)
                {
                    Debug.Print("RefreshMouseList error {0}", e.Message);
                }

                if (mouseInstances == null)
                    return;

                Mouses.Where(dev => !mouseInstances.Exists(d => dev.Guid == d.InstanceGuid)).ToList().ForEach(
                    gc => { gc.Unacquire(); Mouses.Remove(gc); }
                );


                var addList = mouseInstances.Where(d => !Mouses.Exists(dev => dev.Guid == d.InstanceGuid));

                if (addList == null || addList.Count() <= 0)
                    return;

                foreach (DeviceInstance dev in addList)
                {
                    var mouse = new DirectInputMouse(dev);
                    mouse.OnError += new EventHandler<EventArgs>(OnNoNeed);
                    mouse.OnUnacquire += new EventHandler<EventArgs>(OnNoNeed);

                    Mouses.Add(mouse);
                    if (Mouses.Where(d => d.Name.StartsWith(mouse.Name)).Count() > 1)
                    {
                        Mouses.Where(d => d.Name.StartsWith(mouse.Name) && d.Index == 0).ToList().ForEach(
                                d => d.Index = Mouses.Max(dm => dm.Index) + 1
                            );
                    }
                }

                if (OnMouseList != null)
                    OnMouseList(this, EventArgs.Empty);

            }
        }
        public HotKey GetHotKey(HotKey key)
        {
            if (key == null)
            {
                _currentHotkey = null;
                return null;
            }

            _currentHotkey = key;

            foreach (var joy in Joysticks.Where(d => d.Type == DeviceType.Physical))
                joy.OnButtonDown += new EventHandler<CustomEventArgs<DirectInputData>>(hotkey_OnButton);
            
            Keyboards.ForEach(keyboard => keyboard.OnKeyUp += new EventHandler<CustomEventArgs<DirectInputData>>(hotkey_OnButton));
            Mouses.ForEach(mouse => mouse.OnButtonDown += new EventHandler<CustomEventArgs<DirectInputData>>(hotkey_OnButton));
            _currentHotkey.Key = null;
            while (_currentHotkey.Key == null)
            {
                Thread.Sleep(50);
            }
            return _currentHotkey;
        }

        void hotkey_OnButton(object sender, CustomEventArgs<DirectInputData> e)
        {
            _currentHotkey.Key = e.Data;
        }

        public void RefreshKeyboardList()
        {
            lock (lockRefreshKeyboards)
            {
                List<DeviceInstance> keyboardInstances = null;
                if (Keyboards == null)
                    Keyboards = new List<DirectInputKeyboard>();

                try
                {
                    var dI = new DirectInput();
                    keyboardInstances = Utils.DevList(dI.GetDevices(DeviceClass.Keyboard, DeviceEnumerationFlags.AttachedOnly));
                }
                catch (Exception e)
                {
                    Debug.Print("RefreshKeyboardList {0}", e.Message);
                }

                if (keyboardInstances == null)
                    return;

                Keyboards.Where(dev => !keyboardInstances.Exists(d => dev.Guid == d.InstanceGuid)).ToList().ForEach(
                    gc => { gc.Unacquire(); Keyboards.Remove(gc); }
                );

                var addList = keyboardInstances.Where(d => !Keyboards.Exists(dev => dev.Guid == d.InstanceGuid));

                if (addList == null || addList.Count() <= 0)
                    return;

                foreach (DeviceInstance dev in addList)
                {
                    var keyboard = new DirectInputKeyboard(dev);
                    keyboard.OnError += new EventHandler<EventArgs>(OnNoNeed);
                    keyboard.OnUnacquire += new EventHandler<EventArgs>(OnNoNeed);

                    Keyboards.Add(keyboard);
                    if (Keyboards.Where(d => d.Name.StartsWith(keyboard.Name)).Count() > 1)
                    {
                        Keyboards.Where(d => d.Name.StartsWith(keyboard.Name) && d.Index == 0).ToList().ForEach(
                                d => d.Index = Keyboards.Max(dm => dm.Index) + 1
                            );
                    }
                }

                if (OnKeyboardList != null)
                    OnKeyboardList(this, EventArgs.Empty);
            }

        }

        public void RefreshJoystickList()
        {
            lock (lockRefreshJoysticks)
            {
                List<DeviceInstance> joystickInstances;
                IList<DeviceInstance> devices = null;
                if (Joysticks == null)
                    Joysticks = new List<DirectInputJoystick>();

                try
                {
                    var dI = new DirectInput();
                    devices = dI.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly);
                }
                catch( Exception e) {
                    Debug.Print("RefreshJoystickList: error getting device list {0}", e.Message);
                }
                if (devices == null)
                {
                    if (OnJoystickList != null)
                        OnJoystickList(this, EventArgs.Empty);

                    return;
                }

                joystickInstances = Utils.DevList(devices);


                Joysticks.Where(dev => !joystickInstances.Exists(d => dev.Guid == d.InstanceGuid)).ToList().ForEach(
                    gc => { gc.Unacquire(); Joysticks.Remove(gc); }
                );

                var addList = joystickInstances.Where(d => !Joysticks.Exists(dev => dev.Guid == d.InstanceGuid));

                if (addList == null || addList.Count() <= 0)
                    return;

                foreach (DeviceInstance dev in addList)
                {
                    var joystick = new DirectInputJoystick(dev, GetDeviceType(dev.ProductName));

                    if (joystick.Type == DeviceType.Physical)
                        joystick.ExclusiveMode = _settings.exclusiveDirectInput;

                    joystick.OnError += new EventHandler<EventArgs>(OnNoNeed);
                    joystick.OnUnacquire += new EventHandler<EventArgs>(OnNoNeed);

                    Joysticks.Add(joystick);


                    if (Joysticks.Where(d => d.Name.StartsWith(joystick.Name)).Count() > 1)
                    {
                        Joysticks.Where(d => d.Name.StartsWith(joystick.Name) && d.Index == 0).ToList().ForEach(
                                d => d.Index = Joysticks.Max(dm => dm.Index) + 1
                            );
                    }
                }

                if (Joysticks.Exists(j => j.Type == DeviceType.Virtual && j.VirtualJoystick == null))
                {
                    foreach (var joystick in Joysticks.Where(j => j.Type == DeviceType.Virtual))
                    {
                        joystick.OnButtonDown += new EventHandler<CustomEventArgs<DirectInputData>>(gameController_OnButtonDown);
                    }
                    if (Joysticks.Exists(j => j.Name.Contains("vJoy") == true))
                    {
                        for (uint i = 1; i <= (_settings.generalMaxVjoyNum==0?16:_settings.generalMaxVjoyNum); i++)
                        {
                            if (!Joysticks.Exists(j => j.VirtualJoystick != null && j.VirtualJoystick.DeviceID == i))
                            {

                                var vjoy = new VJoyEizikovich(i);

                                if (vjoy.isAcquired)
                                {
                                    vjoy.SetButton(vjoy.DeviceID, true);
                                    vjoy.Unacquire();
                                }

                            }
                        }
                    }
                    else if (Joysticks.Exists(j => j.Name.Contains("VJoy") == true))
                    {
                        for (uint i = 0; i <= (_settings.generalMaxVjoyNum == 0 ? 16 : _settings.generalMaxVjoyNum); i++)
                        {
                            try
                            {
                                var hvjoy = new VJoyHeadsoft(i);
                                if (hvjoy.isAcquired)
                                {
                                    hvjoy.SetButton(hvjoy.DeviceID, true);
                                    hvjoy.Unacquire();
                                }
                            }
                            catch {
                                Debug.Print("Refresh joysticks exception");
                            }
                        }
                    }
                }

                if (OnJoystickList != null)
                    OnJoystickList(this, EventArgs.Empty);
            }
        }
        void gameController_OnButtonDown(object sender, CustomEventArgs<DirectInputData> e)
        {
            lock (bindVirtualJoyLock)
            {
                DirectInputJoystick device = Joysticks.Where(d => d.Name == e.Data.DeviceName).FirstOrDefault();
                if (device == null)
                    return;

                var i = Joysticks.IndexOf(device);
                var devName = device.Name;
                var deviceId = (uint)e.Data.JoystickOffset - (uint)JoystickOffset.Buttons0 + 1;

                switch (devName.Substring(0, 4))
                {
                    case "vJoy":
                        Joysticks[i].VirtualJoystick = new VJoyEizikovich(deviceId);
                        break;
                    case "VJoy":
                        Joysticks[i].VirtualJoystick = new VJoyHeadsoft(deviceId-1);
                        break;
                }

                device.OnButtonPress -= gameController_OnButtonDown;
                device.VirtualJoystick.SetButton(device.VirtualJoystick.DeviceID, false);
            }
        }

        private DeviceType GetDeviceType( string name )
        {
            return virtualTags.Where(vt => name.ToLower().Contains(vt)).Count() > 0 ? DeviceType.Virtual : DeviceType.Physical;
        }

        public List<DirectInputJoystick> Joysticks
        {
            get;
            set;
        }
        public List<DirectInputKeyboard> Keyboards
        {
            get;
            set;
        }
        public List<DirectInputMouse> Mouses
        {
            get;
            set;
        }
    }

}
