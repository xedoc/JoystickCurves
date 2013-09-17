using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Threading;
using System.Diagnostics;

namespace JoystickCurves
{

    public enum KeyState
    {
        Up = 0,
        Down = 128
    }
    public enum DeviceType
    {
        Virtual,
        Physical,
        NotSet
    }
    public class DirectInputDevice
    {
        private const int POLL_INTERVAL = 10;
        private string _name;

        public bool Acquired
        {
            get;
            set;
        }
        public DeviceInstance DeviceInstance
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }
        public override string ToString()
        {
            return Name;
        }
        public DeviceType Type
        {
            get;
            set;
        }
        public string Name
        {
            get
            {
                if (Type == DeviceType.NotSet)
                    return _name;

                if (Index > 0)
                    return String.Format("{0} #{1}", DeviceInstance.InstanceName, Index);
                else
                    return DeviceInstance.InstanceName;
            }
            set
            {
                _name = value;
            }
        }
        public Guid ProductGuid
        {
            get { return DeviceInstance.ProductGuid; }
        }
        public string ProductName
        {
            get { return DeviceInstance.ProductName; }
        }
        public Guid Guid
        {
            get { return DeviceInstance.InstanceGuid; }
        }

        public static implicit operator String(DirectInputDevice gc)
        {
            return gc == null ? String.Empty : gc.Name;
        }
    }
}
