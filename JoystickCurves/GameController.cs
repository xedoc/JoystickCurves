using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Threading;
using System.Diagnostics;
namespace JoystickCurves
{
    public enum GameControllerType
    {
        Virtual,
        Physical,
        NotSet
    }
    public class GameController
    {

        public event EventHandler<EventArgs> OnAcquire;
        public event EventHandler<CustomEventArgs<JoystickData>> OnAxisChange;
        public event EventHandler<CustomEventArgs<JoystickData>> OnButtonChange;
        private const int POLL_INTERVAL = 10;
        private const int AXIS_RANGE = 32767;
        private JoystickOffset[] JoystickAxis = new JoystickOffset[] { JoystickOffset.X, 
                                                     JoystickOffset.Y, 
                                                     JoystickOffset.Z, 
                                                     JoystickOffset.RX, 
                                                     JoystickOffset.RY, 
                                                     JoystickOffset.RZ, 
                                                     JoystickOffset.Slider0, 
                                                     JoystickOffset.Slider1 };

        private Timer _pollTimer;
        private Device _device;
        private DeviceInstance _devInstance;
        private string _name;
        private Dictionary<JoystickOffset, Action<JoystickData>> _actionMap = new Dictionary<JoystickOffset,Action<JoystickData>>();
        private VirtualJoystick _virtualJoystick;
        private void emptyAction(JoystickData joyData) {}

        public GameController(String name)
        {
            Name = name;
            Type = GameControllerType.NotSet;
            Acquired = false;
        }
        public GameController( DeviceInstance dev, GameControllerType type )
        {
            _devInstance = dev;
            Type = type;
            Acquired = false;
            
            _pollTimer = new Timer(new TimerCallback(poll_Tick), null, Timeout.Infinite, Timeout.Infinite);
            OnAcquire += new EventHandler<EventArgs>(GameController_OnAcquire);
        }
        public static implicit operator String(GameController gc)
        {
            return gc == null ? String.Empty : gc.Name;
        }
        public override string ToString()
        {
            return Name;
        }
        public bool Acquired
        {
            get;
            set;
        }

        public VirtualJoystick VirtualJoystick
        {
            get;
            set;
        }
        private void poll_Tick(object o)
        {
            Action<JoystickData> action;

            _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);

            _device.Poll();

            var queue = _device.GetBufferedData();
            if (queue != null)
            {
                foreach (BufferedData data in queue)
                {
                    CustomEventArgs<int> param = new CustomEventArgs<int>(data.Data);
                    JoystickOffset dataType = (JoystickOffset)data.Offset;

                    _actionMap.TryGetValue(dataType, out action);
                    if (JoystickAxis.Contains(dataType))
                    {
                        JoystickData joyData = new JoystickData(MinAxisValue, MaxAxisValue)
                        {
                            Value = data.Data,
                            DirectInputID = dataType,
                            DeviceName = Name

                        };
                        if (action != null)
                        {
                            action(joyData);
                        }
                        if (OnAxisChange != null)
                            OnAxisChange(_device, new CustomEventArgs<JoystickData>(joyData));
                    }
                    else if (dataType >= JoystickOffset.Button0 && dataType <= JoystickOffset.Button128)
                    {
                        JoystickData joyData = new JoystickData()
                        {
                            Value = data.Data,
                            DirectInputID = dataType,
                            DeviceName = Name

                        };
                        if (OnButtonChange != null)
                            OnButtonChange(_device, new CustomEventArgs<JoystickData>(joyData));
                    }
                }
            }
            _pollTimer.Change(POLL_INTERVAL, POLL_INTERVAL);                
        }
        private void GameController_OnAcquire(object sender, EventArgs e)
        {
            Acquired = true;
        }

        public int Index
        {
            get;
            set;
        }
        public string Name
        {
            get
            {
                if (Type == GameControllerType.NotSet)
                    return _name;

                if (Index > 0)
                    return String.Format("{0} #{1}", _devInstance.InstanceName, Index);
                else
                    return _devInstance.InstanceName;
            }
            set
            {
                _name = value;
            }
        }
        public int MinAxisValue
        {
            get { return -AXIS_RANGE; }
        }
        public int MaxAxisValue
        {
            get { return AXIS_RANGE; }
        }
        public string ProductName
        {
            get { return _devInstance.ProductName; }
        }
        public Guid ProductGuid
        {
            get { return _devInstance.ProductGuid; }
        }
        public Guid Guid
        {
            get { return _devInstance.InstanceGuid; }
        }
  
        public GameControllerType Type
        {
            get;
            set;
        }
        public int NumberAxes
        {
            get { return _device.Caps.NumberAxes; }
        }
        public int NumberButtons
        {
            get { return _device.Caps.NumberButtons; }
        }
        public int NumberPOVs
        {
            get { return _device.Caps.NumberPointOfViews;}
        }
        public void Unacquire()
        {
            _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);
            Thread.Sleep(100);
            _device.Unacquire();
        }

        public void Acquire()
        {
            _device = new Device(Guid);
            _device.SetDataFormat(DeviceDataFormat.Joystick);
            _device.Properties.BufferSize = 16;
            _device.SetCooperativeLevel(Process.GetCurrentProcess().MainWindowHandle, 
                CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);

            foreach (DeviceObjectInstance d in _device.Objects)
            {
                if ((d.ObjectId & (int)DeviceObjectTypeFlags.Axis) != 0)
                    _device.Properties.SetRange(ParameterHow.ById, d.ObjectId, new InputRange(MinAxisValue, MaxAxisValue));
            }
            _device.Acquire();

           
            _pollTimer.Change(0, POLL_INTERVAL);
            
            if (OnAcquire != null)
                OnAcquire(this, EventArgs.Empty);
        }
        public void CleanActions(Action<JoystickData> action)
        {
            foreach (var k in _actionMap.Keys)
            {
                if (_actionMap[k] == action)
                    _actionMap.Remove(k);
            }
        }
        public void SetActions(Dictionary<JoystickOffset, Action<JoystickData>> actions)
        {
            if (actions == null || actions.Count <= 0)
                return;

            foreach (var v in actions.Values.Distinct())
            {
                if (_actionMap.ContainsValue(v))
                {
                    foreach (var k in _actionMap.Keys)
                    {
                        if (_actionMap[k] == v)
                            _actionMap.Remove(k);
                    }
                }
            }
            foreach (var k in actions.Keys)
            {
                if (_actionMap.Keys.Contains(k))
                    _actionMap[k] = actions[k];
                else
                    _actionMap.Add(k, actions[k]);
            }
        }
       
        public void Set(JoystickOffset offset, int value)
        {
            if (Type != GameControllerType.Virtual)
                return;

            if (offset >= JoystickOffset.Button0 && offset <= JoystickOffset.Button128)
            {
                _virtualJoystick.SetButton((uint)(offset - JoystickOffset.Button0), value == 128 ? true : false);
                return;
            }

            switch (offset)
            {
                case JoystickOffset.X:
                    _virtualJoystick.X = value;
                    break;
                case JoystickOffset.Y:
                    _virtualJoystick.Y = value;
                    break;
                case JoystickOffset.Z:
                    _virtualJoystick.Z = value;
                    break;
                case JoystickOffset.RX:
                    _virtualJoystick.RX = value;
                    break;
                case JoystickOffset.RY:
                    _virtualJoystick.RY = value;
                    break;
                case JoystickOffset.RZ:
                    _virtualJoystick.RZ = value;
                    break;
                case JoystickOffset.Slider0:
                    _virtualJoystick.SL0 = value;
                    break;
                case JoystickOffset.Slider1:
                    _virtualJoystick.SL1 = value;
                    break;
            }


        }

    }
}
