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
        public event EventHandler<CustomEventArgs<Axis>> OnAxisChange;
        public event EventHandler<CustomEventArgs<Button>> OnButtonChange;
        private const int POLL_INTERVAL = 50;
        private const int AXIS_RANGE = 32767;
        private JoystickOffset[] JoystickAxes = new JoystickOffset[] { JoystickOffset.X, 
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
        private void poll_Tick(object o)
        {
            _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);

            //_device.Poll();

            var queue = _device.GetBufferedData();
            if (queue == null)
            {
                _pollTimer.Change(POLL_INTERVAL, POLL_INTERVAL);
                return;
            }
            foreach (BufferedData data in queue)
            {
                CustomEventArgs<int> param = new CustomEventArgs<int>(data.Data);
                JoystickOffset dataType = (JoystickOffset)data.Offset;

                if (JoystickAxes.Contains(dataType))
                {
                    Axis axis = new Axis(MinAxisValue, MaxAxisValue) {
                        Value = data.Data,
                        DirectInputID = dataType,
                        DeviceName = Name

                    };
                    if (OnAxisChange != null) 
                        OnAxisChange(_device, new CustomEventArgs<Axis>(axis));
                }
                else if (dataType >= JoystickOffset.Button0 && dataType <= JoystickOffset.Button128)
                {
                    Button button = new Button()
                    {
                        Value = data.Data,
                        DirectInputID = dataType
                    };
                    if (OnButtonChange != null)
                        OnButtonChange(_device, new CustomEventArgs<Button>(button));
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

           
            _pollTimer.Change(POLL_INTERVAL, POLL_INTERVAL);
            
            if (OnAcquire != null)
                OnAcquire(this, EventArgs.Empty);
        }

    }
}
