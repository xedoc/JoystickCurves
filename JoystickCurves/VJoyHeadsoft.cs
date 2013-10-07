using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JoystickCurves
{
    public class VJoyHeadsoft : VirtualJoystick
    {
        #region Private properties
        private VJoy _joystick;
        private int _deviceid;
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

        public VJoyHeadsoft(uint id)
            : base((uint)id)
        {
            Init();

            _deviceid = (int)id;
            Acquire();

            if (!isAcquired)
                return;

            _acquireTimer = new Timer(new TimerCallback(acquire_Tick), null, Timeout.Infinite, Timeout.Infinite);

        }
        public override void acquire_Tick( object o)
        {
                //Acquire();

        }
        public override void Init()
        {
            minMaxAxis = new Dictionary<HID_USAGES, int[]>();
            lastValue = new Dictionary<HID_USAGES, int>();
            isAcquired = false;
            _joystick = new VJoy();
        }

        public override String Name
        {
            get;
            set;
        }
        public override void Reset()
        {
            _joystick.Reset();
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
                _joystick.Shutdown();
            }
            catch {
                Debug.Print("VJoyHeadsoft::Unacquire exception");
            }
        }
        public override bool Acquire()
        {
            lock (lockAcquire)
            {
                if (!_joystick.Initialize())
                    return false;

                Reset();

                if (OnAcquire != null)
                    OnAcquire(this, EventArgs.Empty);
                
                Enabled = true;
                isAcquired = true;
                return true;
            }

        }
        public override int AxisMaxValue(HID_USAGES hidusage)
        {
            return AXISLIMIT;
        }
        public override int AxisMinValue(HID_USAGES hidusage)
        {
            return -AXISLIMIT;
        }

        public override void SetContPOV(int value, uint pov_number)
        {
            //_joystick.SetPOV(DeviceID, pov_number, value);
        }
        public override void SetDiscPOV(int value, uint pov_number)
        {
            //_joystick.SetDiscPov(value, _deviceid, pov_number);
        }
        public override Int32 X
        {
            set { _joystick.SetXAxis(_deviceid, (short)value); _joystick.Update(_deviceid); }
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
            set { _joystick.SetYAxis(_deviceid, (short)value); _joystick.Update(_deviceid); }
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
            set { _joystick.SetZAxis(_deviceid, (short)value); _joystick.Update(_deviceid); }
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
            set { _joystick.SetXRotation(_deviceid, (short)value); _joystick.Update(_deviceid); }
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
            set { _joystick.SetXRotation(_deviceid, (short)value); _joystick.Update(_deviceid); }
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
            set { _joystick.SetZRotation(_deviceid, (short)value); _joystick.Update(_deviceid); }
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
            set { _joystick.SetSlider(_deviceid, (short)value); _joystick.Update(_deviceid); }
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
            set { _joystick.SetSlider(_deviceid, (short)value); _joystick.Update(_deviceid); }
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
            set { _joystick.SetDial(_deviceid, (short)value); _joystick.Update(_deviceid); }
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
                return (UInt32)_deviceid; 
            }
        }
        public override bool Enabled
        {
            get
            {
                return isAcquired;
            }
        }
        public override string Manufacturer
        {
            get
            {
                return "Headsoft";
            }
        }
        public override string Product
        {
            get { return "Headsoft VJoy"; }
        }
        public override string SerialNumber
        {
            get { return _deviceid.ToString(); }
        }

        public override bool SetButton(uint number, bool btnDown)
        {
            _joystick.SetButton(_deviceid, (int)number, btnDown);
            _joystick.Update(_deviceid);
            return true;
        }

        public override bool isAcquired
        {
            get;
            set;
        }

        #endregion

    }


    class VJoy
    {
        public enum POVType
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3,
            Nil = 4
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct JoystickState
        {
            public byte ReportId;
            public short XAxis;
            public short YAxis;
            public short ZAxis;
            public short XRotation;
            public short YRotation;
            public short ZRotation;
            public short Slider;
            public short Dial;
            public ushort POV;
            public uint Buttons;
        };

        [DllImport("VJoy.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VJoy_Initialize(StringBuilder name, StringBuilder serial);

        [DllImport("VJoy.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void VJoy_Shutdown();

        [DllImport("VJoy.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VJoy_UpdateJoyState(int id, ref JoystickState joyState);
        private object lockUpdate = new object();
        private JoystickState[] m_joyState;

        public VJoy()
        {
        }

        public bool Initialize()
        {
            m_joyState = new JoystickState[2];

            m_joyState[0] = new JoystickState();
            m_joyState[1] = new JoystickState();

            StringBuilder Name = new StringBuilder("");
            StringBuilder Serial = new StringBuilder("");

            return VJoy_Initialize(Name, Serial);
        }
        public void Shutdown()
        {
            VJoy_Shutdown();
        }

        public bool Update(int id)
        {
            lock (lockUpdate)
            {
                try
                {
                    return VJoy_UpdateJoyState(id, ref m_joyState[id]);
                }
                catch {
                    Debug.Print("VJoyHeadsoft::Update exception");
                    return false;
                }
            }
        }

        public void Reset()
        {
            m_joyState[0].ReportId = 0;
            m_joyState[0].XAxis = 0;
            m_joyState[0].YAxis = 0;
            m_joyState[0].ZAxis = 0;
            m_joyState[0].XRotation = 0;
            m_joyState[0].YRotation = 0;
            m_joyState[0].ZRotation = 0;
            m_joyState[0].Slider = 0;
            m_joyState[0].Dial = 0;
            m_joyState[0].POV = ((int)POVType.Nil << 12) | ((int)POVType.Nil << 8) | ((int)POVType.Nil << 4) | (int)POVType.Nil;
            m_joyState[0].Buttons = 0;

            m_joyState[1].ReportId = 0;
            m_joyState[1].XAxis = 0;
            m_joyState[1].YAxis = 0;
            m_joyState[1].ZAxis = 0;
            m_joyState[1].XRotation = 0;
            m_joyState[1].YRotation = 0;
            m_joyState[1].ZRotation = 0;
            m_joyState[1].Slider = 0;
            m_joyState[1].Dial = 0;
            m_joyState[1].POV = ((int)POVType.Nil << 12) | ((int)POVType.Nil << 8) | ((int)POVType.Nil << 4) | (int)POVType.Nil;
            m_joyState[1].Buttons = 0;
        }

        public short GetXAxis(int index)
        {
            return m_joyState[index].XAxis;
        }

        public void SetXAxis(int index, short value)
        {
            m_joyState[index].XAxis = value;
        }

        public short GetYAxis(int index)
        {
            return m_joyState[index].YAxis;
        }

        public void SetYAxis(int index, short value)
        {
            m_joyState[index].YAxis = value;
        }

        public short GetZAxis(int index)
        {
            return m_joyState[index].ZAxis;
        }

        public void SetZAxis(int index, short value)
        {
            m_joyState[index].ZAxis = value;
        }

        public short GetXRotation(int index)
        {
            return m_joyState[index].XRotation;
        }

        public void SetXRotation(int index, short value)
        {
            m_joyState[index].XRotation = value;
        }

        public short GetYRotation(int index)
        {
            return m_joyState[index].YRotation;
        }

        public void SetYRotation(int index, short value)
        {
            m_joyState[index].YRotation = value;
        }

        public short GetZRotation(int index)
        {
            return m_joyState[index].ZRotation;
        }

        public void SetZRotation(int index, short value)
        {
            m_joyState[index].ZRotation = value;
        }

        public short GetSlider(int index)
        {
            return m_joyState[index].Slider;
        }

        public void SetSlider(int index, short value)
        {
            m_joyState[index].Slider = value;
        }

        public short GetDial(int index)
        {
            return m_joyState[index].Dial;
        }

        public void SetDial(int index, short value)
        {
            m_joyState[index].Dial = value;
        }

        public void SetPOV(int index, int pov, POVType value)
        {
            m_joyState[index].POV &= (ushort)~((int)0xf << ((3 - pov) * 4));
            m_joyState[index].POV |= (ushort)((int)value << ((3 - pov) * 4));
        }

        public POVType GetPOV(int index, int pov)
        {
            return (POVType)((m_joyState[index].POV >> ((3 - pov) * 4)) & 0xf);
        }

        public void SetButton(int index, int button, bool value)
        {
            if (value)
                m_joyState[index].Buttons |= (uint)(1 << button);
            else
                m_joyState[index].Buttons &= (uint)~(1 << button);
        }

        public bool GetButton(int index, int button)
        {
            return ((m_joyState[index].Buttons & (1 << button)) == 1);
        }
    }

}
