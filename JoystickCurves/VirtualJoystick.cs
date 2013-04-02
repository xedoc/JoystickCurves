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
        private const int AXISLIMIT = 32767;
        private Dictionary<HID_USAGES, int[]> minMaxAxis;
        private Dictionary<HID_USAGES, int> lastValue;
        private object lockSetAxis = new object();
        vJoy.JoystickState state;
        #endregion

        #region Public accessors/methods/properties
        public VirtualJoystick(uint id)
        {
            minMaxAxis = new Dictionary<HID_USAGES, int[]>();
            lastValue = new Dictionary<HID_USAGES, int>();
            _joystick = new vJoy();
            if (!Enabled)
                throw new Exception("VJoy isn't enabled! Check driver installation!");


            Acquire(id);
        }
        public void Reset()
        {
            _joystick.ResetVJD(_deviceid);
        }
        public vJoy Joystick
        {
            get { return _joystick; }
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

            state = new vJoy.JoystickState();

            var axisList = new HID_USAGES[] { HID_USAGES.HID_USAGE_X, HID_USAGES.HID_USAGE_Y, HID_USAGES.HID_USAGE_RZ };
            var v = 0;
            foreach (var a in axisList)
            {
                v = AxisMinValue(a);
                SetAxis(0, a);
            }


        }
        private int AxisMaxValue(HID_USAGES hidusage)
        {
            long maxValue = 0;
            long minValue = 0;
            if (!minMaxAxis.ContainsKey(hidusage))
            {
                _joystick.GetVJDAxisMax(_deviceid, hidusage, ref maxValue);
                _joystick.GetVJDAxisMin(_deviceid, hidusage, ref minValue);
                minMaxAxis.Add(hidusage, new int[] { (int)minValue, (int)maxValue });
            }
            return AXISLIMIT;
        }
        private int AxisMinValue(HID_USAGES hidusage)
        {
            long maxValue = 0;
            long minValue = 0;
            if (!minMaxAxis.ContainsKey(hidusage))
            {
                _joystick.GetVJDAxisMax(_deviceid, hidusage, ref maxValue);
                _joystick.GetVJDAxisMin(_deviceid, hidusage, ref minValue);
                minMaxAxis.Add(hidusage, new int[] { (int)minValue, (int)maxValue });
            }
            return -AXISLIMIT;
        }

        //-32767 .. 32767
        public void SetAxis(int value, HID_USAGES axis)
        {
            lock(lockSetAxis)
            {
                try
                {
                    var val = (value + AXISLIMIT) / 2;
                    if (!lastValue.Keys.Contains(axis))
                        lastValue.Add(axis, val);

                    if (val != lastValue[axis])
                        _joystick.SetAxis(val, _deviceid, axis);
                }
                catch { }
            }
        }
        public void SetContPOV(int value, uint pov_number)
        {
            _joystick.SetContPov(value, _deviceid, pov_number);
        }
        public void SetDiscPOV(int value, uint pov_number)
        {
            _joystick.SetDiscPov(value, _deviceid, pov_number);
        }
        public Int32 X
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_X); }
        }
        public int MaxX
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_X); }
        }
        public int MinX
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_X); }
        }

        public Int32 Y
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_Y); }
        }
        public int MaxY
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_Y); }
        }
        public int MinY
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_Y); }
        }

        public Int32 Z
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_Z); }
        }
        public int MaxZ
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_Z); }
        }
        public int MinZ
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_Z); }
        }

        public Int32 RX
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_RX); }
        }
        public int MaxRX
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_RX); }
        }
        public int MinRX
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_RX); }
        }

        public Int32 RY
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_RY); }
        }
        public int MaxRY
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_RY); }
        }
        public int MinRY
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_RY); }
        }
        
        public Int32 RZ
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_RZ); }
        }
        public int MaxRZ
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_RZ); }
        }
        public int MinRZ
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_RZ); }
        }


        public Int32 SL0
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_SL0); }
        }
        public int MaxSL0
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_SL0); }
        }
        public int MinSL0
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_SL0); }
        }

        public Int32 SL1
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_SL1); }
        }
        public int MaxSL1
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_SL1); }
        }
        public int MinSL1
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_SL1); }
        }



        public Int32 WHL
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_WHL); }
        }
        public int MaxWHL
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_WHL); }
        }
        public int MinWHL
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_WHL); }
        }


        public Int32 ContPOV1
        {
            set { SetContPOV(value, 1); }
        }
        public Int32 ContPOV2
        {
            set { SetContPOV(value, 2); }
        }
        public Int32 ContPOV3
        {
            set { SetContPOV(value, 3); }
        }
        public Int32 ContPOV4
        {
            set { SetContPOV(value, 4); }
        }
        public Int32 DiscPOV1
        {
            set { SetDiscPOV(value, 1); }
        }
        public Int32 DiscPOV2
        {
            set { SetDiscPOV(value, 2); }
        }
        public Int32 DiscPOV3
        {
            set { SetDiscPOV(value, 3); }
        }
        public Int32 DiscPOV4
        {
            set { SetDiscPOV(value, 4); }
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
        #endregion

    }

}
