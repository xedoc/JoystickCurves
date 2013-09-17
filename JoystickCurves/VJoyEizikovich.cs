using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vJoyInterfaceWrap;
using System.Threading;
using System.Diagnostics;

namespace JoystickCurves
{
    class VJoyEizikovich : VirtualJoystick
    {
        #region Private properties
        private vJoy _joystick;
        private UInt32 _deviceid;
        private const int AXISLIMIT = 32767;
        private Dictionary<HID_USAGES, int[]> minMaxAxis;
        private Dictionary<HID_USAGES, int> lastValue;
        private object lockSetAxis = new object();
        private object lockAcquire = new object();
        private const int POLLINTERVAL = 3000;
        private Timer _acquireTimer;
        private object lockMinMax = new object();
        #endregion

        #region Public accessors/methods/properties
        new public event EventHandler<EventArgs> OnAcquire;

        public VJoyEizikovich(uint id) : base(id)
        {
            Init();

            if (!Enabled)
                return;

            _deviceid = id;
            Acquire();
            _acquireTimer = new Timer(new TimerCallback(acquire_Tick), null, POLLINTERVAL, POLLINTERVAL);

        }
        public override void acquire_Tick( object o)
        {
                Acquire();

        }
        public override void Init()
        {
            minMaxAxis = new Dictionary<HID_USAGES, int[]>();
            lastValue = new Dictionary<HID_USAGES, int>();
            isAcquired = false;
            _joystick = new vJoy();
        }

        public override String Name
        {
            get;
            set;
        }
        public override void Reset()
        {
            _joystick.ResetAll();
        }
        //public vJoy Joystick
        //{
        //    get { return _joystick; }
        //    set { _joystick = value; }
        //}
        public override void Unacquire()
        {
            try
            {
                _joystick.RelinquishVJD(_deviceid);
            }
            catch { }
        }
        public override bool Acquire()
        {
            lock (lockAcquire)
            {
                var status = _joystick.GetVJDStatus(_deviceid);
                switch (status)
                {
                    case VjdStat.VJD_STAT_OWN:
                        return true;
                    case VjdStat.VJD_STAT_FREE:
                        break;
                    case VjdStat.VJD_STAT_BUSY:
                        return false;
                    case VjdStat.VJD_STAT_MISS:
                        return false;
                    default:
                        return false;
                };

                _joystick.AcquireVJD(_deviceid);

                var axisList = new HID_USAGES[] { HID_USAGES.HID_USAGE_X, HID_USAGES.HID_USAGE_Y, HID_USAGES.HID_USAGE_RZ };
                var v = 0;
                foreach (var a in axisList)
                {
                    v = AxisMinValue(a);
                    SetAxis(0, a);
                }

                var btnNumber = _joystick.GetVJDButtonNumber(_deviceid);
                for (uint i = 0; i < btnNumber; i++)
                    _joystick.SetBtn(false, _deviceid, i);

                if (OnAcquire != null)
                    OnAcquire(this, EventArgs.Empty);

                isAcquired = true;
                return true;
            }

        }
        public override int AxisMaxValue(HID_USAGES hidusage)
        {
            //long maxValue = 0;
            //long minValue = 0;
            //if (!minMaxAxis.ContainsKey(hidusage))
            //{
            //    _joystick.GetVJDAxisMax(_deviceid, hidusage, ref maxValue);
            //    _joystick.GetVJDAxisMin(_deviceid, hidusage, ref minValue);
            //    minMaxAxis.Add(hidusage, new int[] { (int)minValue, (int)maxValue });
            //}
            return AXISLIMIT;
        }
        public override int AxisMinValue(HID_USAGES hidusage)
        {
            //long maxValue = 0;
            //long minValue = 0;
            //if (!minMaxAxis.ContainsKey(hidusage))
            //{
            //    _joystick.GetVJDAxisMax(_deviceid, hidusage, ref maxValue);
            //    _joystick.GetVJDAxisMin(_deviceid, hidusage, ref minValue);
            //    minMaxAxis.Add(hidusage, new int[] { (int)minValue, (int)maxValue });
            //}
            return -AXISLIMIT;
        }

        //-32767 .. 32767
        public override void SetAxis(int value, HID_USAGES axis)
        {
            lock(lockSetAxis)
            {
                try
                {
                    var val = (value + AXISLIMIT) / 2;
                    _joystick.SetAxis(val, _deviceid, axis);
                }
                catch { }
            }
        }
        public override void SetContPOV(int value, uint pov_number)
        {
            _joystick.SetContPov(value, _deviceid, pov_number);
        }
        public override void SetDiscPOV(int value, uint pov_number)
        {
            _joystick.SetDiscPov(value, _deviceid, pov_number);
        }
        public override Int32 X
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_X); }
        }
        public override int MaxX
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_X); }
        }
        public override int MinX
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_X); }
        }

        public override Int32 Y
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_Y); }
        }
        public override int MaxY
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_Y); }
        }
        public override int MinY
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_Y); }
        }

        public override Int32 Z
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_Z); }
        }
        public override int MaxZ
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_Z); }
        }
        public override int MinZ
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_Z); }
        }

        public override Int32 RX
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_RX); }
        }
        public override int MaxRX
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_RX); }
        }
        public override int MinRX
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_RX); }
        }

        public override Int32 RY
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_RY); }
        }
        public override int MaxRY
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_RY); }
        }
        public override int MinRY
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_RY); }
        }

        public override Int32 RZ
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_RZ); }
        }
        public override int MaxRZ
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_RZ); }
        }
        public override int MinRZ
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_RZ); }
        }


        public override Int32 SL0
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_SL0); }
        }
        public override int MaxSL0
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_SL0); }
        }
        public override int MinSL0
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_SL0); }
        }

        public override Int32 SL1
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_SL1); }
        }
        public override int MaxSL1
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_SL1); }
        }
        public override int MinSL1
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_SL1); }
        }



        public override Int32 WHL
        {
            set { SetAxis(value, HID_USAGES.HID_USAGE_WHL); }
        }
        public override int MaxWHL
        {
            get { return AxisMaxValue(HID_USAGES.HID_USAGE_WHL); }
        }
        public override int MinWHL
        {
            get { return AxisMinValue(HID_USAGES.HID_USAGE_WHL); }
        }


        public override Int32 ContPOV1
        {
            set { SetContPOV(value, 1); }
        }
        public override Int32 ContPOV2
        {
            set { SetContPOV(value, 2); }
        }
        public override Int32 ContPOV3
        {
            set { SetContPOV(value, 3); }
        }
        public override Int32 ContPOV4
        {
            set { SetContPOV(value, 4); }
        }
        public override Int32 DiscPOV1
        {
            set { SetDiscPOV(value, 1); }
        }
        public override Int32 DiscPOV2
        {
            set { SetDiscPOV(value, 2); }
        }
        public override Int32 DiscPOV3
        {
            set { SetDiscPOV(value, 3); }
        }
        public override Int32 DiscPOV4
        {
            set { SetDiscPOV(value, 4); }
        }
        public override UInt32 DeviceID
        {
            get { 
                return _deviceid; 
            }
        }
        public override bool Enabled
        {
            get
            {
                return _joystick.vJoyEnabled();
            }
        }
        public override string Manufacturer
        {
            get
            {
                return _joystick.GetvJoyManufacturerString();
            }
        }
        public override string Product
        {
            get { return _joystick.GetvJoyProductString(); }
        }
        public override string SerialNumber
        {
            get { return _joystick.GetvJoySerialNumberString(); }
        }

        public override bool SetButton(uint number, bool btnDown)
        {
            return _joystick.SetBtn(btnDown, _deviceid, number);
        }

        public override bool isAcquired
        {
            get;
            set;
        }

        #endregion

    }
}
