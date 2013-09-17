using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vJoyInterfaceWrap;
using System.Threading;
using System.Diagnostics;
namespace JoystickCurves
{
    public class VirtualJoystick
    {
        #region Private properties
        private UInt32 _deviceid;
        private const int AXISLIMIT = 32767;
        private object lockSetAxis = new object();
        private object lockAcquire = new object();
        private const int POLLINTERVAL = 3000;
        private Timer _acquireTimer;
        private object lockMinMax = new object();
        #endregion

        #region Public accessors/methods/properties
        public virtual event EventHandler<EventArgs> OnAcquire;

        public VirtualJoystick(uint id)
        {
        }
        public virtual void acquire_Tick(object o)
        {
        }
        public virtual void Init()
        {

        }

        public virtual String Name
        {
            get;
            set;
        }
        public virtual void Reset()
        {
        }
        public virtual void Unacquire()
        {
        }
        public virtual bool Acquire()
        {
            return true;
        }
        public virtual int AxisMaxValue(HID_USAGES hidusage)
        {
            return AXISLIMIT;
        }
        public virtual int AxisMinValue(HID_USAGES hidusage)
        {
            return -AXISLIMIT;
        }
        public virtual void SetAxis(int value, HID_USAGES axis)
        {

        }
        public virtual void SetContPOV(int value, uint pov_number)
        {

        }
        public virtual void SetDiscPOV(int value, uint pov_number)
        {
        }
        public virtual Int32 X
        {
            get;
            set;
        }
        public virtual int MaxX
        {
            get;
            set;
        }
        public virtual int MinX
        {
            get;
            set;
        }
        public virtual Int32 Y
        {
            get;
            set;
        }
        public virtual int MaxY
        {
            get;
            set;
        }
        public virtual int MinY
        {
            get;
            set;
        }
        public virtual Int32 Z
        {
            get;
            set;
        }
        public virtual int MaxZ
        {
            get;
            set;
        }
        public virtual int MinZ
        {
            get;
            set;
        }
        public virtual Int32 RX
        {
            get;
            set;
        }
        public virtual int MaxRX
        {
            get;
            set;
        }
        public virtual int MinRX
        {
            get;
            set;
        }
        public virtual Int32 RY
        {
            get;
            set;
        }
        public virtual int MaxRY
        {
            get;
            set;
        }
        public virtual int MinRY
        {
            get;
            set;
        }
        public virtual Int32 RZ
        {
            get;
            set;
        }
        public virtual int MaxRZ
        {
            get;
            set;
        }
        public virtual int MinRZ
        {
            get;
            set;
        }
        public virtual Int32 SL0
        {
            get;
            set;
        }
        public virtual int MaxSL0
        {
            get;
            set;
        }
        public virtual int MinSL0
        {
            get;
            set;
        }
        public virtual Int32 SL1
        {
            get;
            set;
        }
        public virtual int MaxSL1
        {
            get;
            set;
        }
        public virtual int MinSL1
        {
            get;
            set;
        }
        public virtual Int32 WHL
        {
            get;
            set;
        }
        public virtual int MaxWHL
        {
            get;
            set;
        }
        public virtual int MinWHL
        {
            get;
            set;
        }
        public virtual Int32 ContPOV1
        {
            get;
            set;
        }
        public virtual Int32 ContPOV2
        {
            get;
            set;
        }
        public virtual Int32 ContPOV3
        {
            get;
            set;
        }
        public virtual Int32 ContPOV4
        {
            get;
            set;
        }
        public virtual Int32 DiscPOV1
        {
            get;
            set;
        }
        public virtual Int32 DiscPOV2
        {
            get;
            set;
        }
        public virtual Int32 DiscPOV3
        {
            get;
            set;
        }
        public virtual Int32 DiscPOV4
        {
            get;
            set;
        }
        public virtual UInt32 DeviceID
        {
            get;
            set;
        }
        public virtual bool Enabled
        {
            get;
            set;
        }
        public virtual string Manufacturer
        {
            get;
            set;
        }
        public virtual string Product
        {
            get;
            set;
        }
        public virtual string SerialNumber
        {
            get;
            set;
        }

        public virtual bool SetButton(uint number, bool btnDown)
        {
            return true;
        }

        public virtual bool isAcquired
        {
            get;
            set;
        }

        #endregion

        #region Private methods/accessors
        #endregion

    }

}
