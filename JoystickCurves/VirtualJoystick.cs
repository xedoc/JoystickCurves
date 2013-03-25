using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vJoyInterfaceWrap;

namespace JoystickCurves
{
    public class VirtualJoystick
    {
        #region Private properties
        private vJoy _joystick;
        private UInt32 _deviceid;
        
        private Axis _axis;
        private MultiControlState<bool> _buttons;
        private MultiControlState<int> _continuousPovs;
        private MultiControlState<int> _discretePovs;

        #endregion

        #region Public accessors/methods/properties
        public VirtualJoystick(uint id)
        {
            _joystick = new vJoy();

            if (!Enabled)
                throw new Exception("VJoy isn't enabled! Check driver installation!");

            Acquire(id);
        }
        public void Reset()
        {
            _joystick.ResetVJD(_deviceid);
        }
        public void Acquire(uint id)
        {
            var status = _joystick.GetVJDStatus(id);
            switch (status)
            {
                case VjdStat.VJD_STAT_OWN:
                    break;
                case VjdStat.VJD_STAT_FREE:
                    break;
                case VjdStat.VJD_STAT_BUSY:
                    throw new Exception(String.Format("vJoy Device {0} is already owned by another feeder. Cannot continue", id));
                case VjdStat.VJD_STAT_MISS:
                    throw new Exception(String.Format("vJoy Device {0} is not installed or disabled. Cannot continue.", id));
                default:
                    throw new Exception(String.Format("vJoy Device {0} general error. Cannot continue.", id));
            };
            _deviceid = id;
            _joystick.AcquireVJD(_deviceid);
            _joystick.ResetVJD(_deviceid);

        }
        public Axis Axis
        {
            get {
                if (_axis == null)
                    _axis = new Axis();

                _axis.X = CreateAxisControlState(HID_USAGES.HID_USAGE_X);
                _axis.Y = CreateAxisControlState(HID_USAGES.HID_USAGE_Y);
                _axis.Z = CreateAxisControlState(HID_USAGES.HID_USAGE_Z);
                _axis.RX = CreateAxisControlState(HID_USAGES.HID_USAGE_RX);
                _axis.RZ = CreateAxisControlState(HID_USAGES.HID_USAGE_RZ); 
                return _axis;
            }
            set
            {
                if (value != null)
                    _axis = value;
            }

        }
        public MultiControlState<bool> Buttons
        {
            get
            {
                if (_buttons == null)
                {
                    _buttons = new MultiControlState<bool>(_joystick.GetVJDButtonNumber(_deviceid), SetButton);
                }
                return _buttons;

            }
            set
            {
                if (value != null)
                    _buttons = value;

            }
        }
        public MultiControlState<int> ContinuousPovs
        {
            get
            {
                if (_continuousPovs == null)
                {
                    _continuousPovs = new MultiControlState<int>(_joystick.GetVJDContPovNumber(_deviceid), SetContinuousPOV);
                }
                return _continuousPovs;

            }
            set
            {
                if (value != null)
                    _continuousPovs = value;

            }
        }

        public MultiControlState<int> DiscretePovs
        {
            get
            {
                if (_discretePovs == null)
                {
                    _discretePovs = new MultiControlState<int>(_joystick.GetVJDDiscPovNumber(_deviceid), SetDiscretePov );
                }
                return _discretePovs;

            }
            set
            {
                if (value != null)
                    _discretePovs = value;

            }
        }

        public UInt32 DeviceID
        {
            get { 
                return _deviceid; 
            }
        }
        public bool Enabled
        {
            get
            {
                return _joystick.vJoyEnabled();
            }
        }
        public string Manufacturer
        {
            get
            {
                return _joystick.GetvJoyManufacturerString();
            }
        }
        public string Product
        {
            get { return _joystick.GetvJoyProductString(); }
        }
        public string SerialNumber
        {
            get { return _joystick.GetvJoySerialNumberString(); }
        }

        #endregion

        #region Private methods/accessors

        private void SetButton(ControlState<bool> button)
        {
            _joystick.SetBtn(button.Value, _deviceid, button.Index);
        }

        private void SetContinuousPOV(ControlState<int> pov)
        {
            _joystick.SetContPov(pov.Value, _deviceid, pov.Index);
        }

        private void SetDiscretePov(ControlState<int> pov)
        {
            _joystick.SetDiscPov(pov.Value, _deviceid, pov.Index);
        }
        private void SetAxis(ControlState<int> axis)
        {
            _joystick.SetAxis(axis.Value, _deviceid, axis.HidUsage);
        }

        private ControlState<int> CreateAxisControlState(HID_USAGES usage)
        {
            return new ControlState<int>()
            {
                Enabled = _joystick.GetVJDAxisExist(_deviceid, usage),
                HidUsage = usage,
                Action = SetAxis
            };
        }
        #endregion


    }

    public class Axis
    {
        public ControlState<int> X
        {
            get;
            set;
        }
        public ControlState<int> Y
        {
            get;
            set;
        }
        public ControlState<int> Z
        {
            get;
            set;
        }
        public ControlState<int> RX
        {
            get;
            set;
        }
        public ControlState<int> RZ
        {
            get;
            set;
        }

    }

    public class ControlState<T>
    {
        public bool Enabled
        {
            get;
            set;
        }

        public T Value
        {
            get;
            set;
        }
        public uint Index
        {
            get;
            set;
        }
        public HID_USAGES HidUsage
        {
            get;
            set;
        }
        public Action<ControlState<T>> Action
        {
            get;
            set;
        }
        public void Set( T value )
        {
            Value = value;
            Action(this);
        }
    }

    public class MultiControlState<T>
    {
        public MultiControlState(int count, Action<ControlState<T>> setAction)
        {
            Count = count;
            State = new List<ControlState<T>>();
            for( uint i = 1; i <= count; i++ )
                State.Add( new ControlState<T>() { Enabled = true, Index = i, Action = setAction});                        
        }
        public int Count
        {
            get;
            set;
        }
        public ControlState<T> this[int index]
        {
            get { return State[index]; }
            set { State[index] = value; }
        }
        public List<ControlState<T>> State
        {
            get;
            set;
        }
    }

}
