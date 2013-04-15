using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Xml.Serialization;

namespace JoystickCurves
{
    public enum DIDataType
    {
        NotSet,
        Joystick,
        Mouse,
        Keyboard
    }
    public class DirectInputData
    {
        private JoystickOffset _joystickOffset;
        private MouseOffset _mouseOffset;
        private Key _key;

        private const string UNKNOWN = "Unknown";
        private string _name;
        private HID_USAGES _virtualID;
        public DirectInputData()
        {
            Min = -1;
            Max = 1;
            Value = 0;
            Type = DIDataType.Joystick;
        }
        [XmlIgnore]
        public DIDataType Type
        {
            get;
            set;
        }
        [XmlAttribute(AttributeName="DeviceType")]
        public String TypeString
        {
            get { return Type.ToString(); }
            set { DIDataType v; Enum.TryParse<DIDataType>(value, out v); Type = v; }
        }
        public DirectInputData(int min, int max)
        {
            if (min == max)
                throw new Exception("Minimum and maximum value of axies should not be equal!");

            Min = min;
            Max = max;
            Value = 0;
            Type = DIDataType.Joystick;
        }
        [XmlAttribute]
        public string DeviceName
        {
            get;
            set;
        }
        [XmlIgnore]
        public int Value
        {
            get;
            set;
        }
        [XmlIgnore]
        public int PercentValue
        {
            get { return (int)Utils.PTop(100, Value + Max, Max - Min); }
            set { Value = (int)((value + Max) * 100.0f / (Max - Min)); }
        }
        [XmlIgnore]
        public int Max
        {
            get;
            set;
        }
        [XmlIgnore]
        public int Min
        {
            get;
            set;
        }
        [XmlIgnore]
        public string Name
        {
            get{ return _name; }
            set{
                _joystickOffset = DIUtils.ID(value);
                _virtualID = DIUtils.VirtualID(value);
                _name = value;
 
            }
        }
        [XmlIgnore]
        public HID_USAGES VirtualID
        {
            get { return _virtualID; }
            set {
                _name = DIUtils.VirtualName(value);
                _virtualID = value;
                _joystickOffset = DIUtils.ID(_name);
            }
        }
        [XmlIgnore]
        public JoystickOffset JoystickOffset
        {
            get { return _joystickOffset; }
            set
            {
                _name = value.ToString();
                _joystickOffset = value;
                _virtualID = DIUtils.VirtualID(_name);
            }        
        }

        [XmlAttribute(AttributeName="JoyKey")]
        public String JoystickOffsetString
        {
            get { return JoystickOffset.ToString(); }
            set { JoystickOffset v; Enum.TryParse<JoystickOffset>(value, out v); JoystickOffset = v; }
        }

        [XmlIgnore]
        public MouseOffset MouseOffset
        {
            get { return _mouseOffset; }
            set
            {
                _name = value.ToString();
                _mouseOffset = value;
            }
        }
        [XmlAttribute(AttributeName = "MouseKey")]
        public String MouseOffsetString
        {
            get { return MouseOffset.ToString(); }
            set { MouseOffset v; Enum.TryParse<MouseOffset>(value, out v); MouseOffset = v; }
        }
        [XmlIgnore]
        public Key KeyboardKey
        {
            get { return _key; }
            set
            {
                _name = value.ToString();
                _key = value;
            }
        }
        [XmlAttribute(AttributeName = "KeyboardKey")]
        public String KeyboardKeyString
        {
            get { return KeyboardKey.ToString(); }
            set { Key v; Enum.TryParse<Key>(value, out v); KeyboardKey = v; }
        }
        public static implicit operator String(DirectInputData ax)
        {
            return ax == null ? String.Empty : ax.Name;
        }
        public static implicit operator DirectInputData(String name)
        {
            return new DirectInputData() { Name = name };
        }
    }
}
