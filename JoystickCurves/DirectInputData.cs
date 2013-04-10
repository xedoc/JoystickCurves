using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace JoystickCurves
{
    
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
        }        
        public DirectInputData(int min, int max)
        {
            if (min == max)
                throw new Exception("Minimum and maximum value of axies should not be equal!");

            Min = min;
            Max = max;
            Value = 0;
        }
        public string DeviceName
        {
            get;
            set;
        }
        public int Value
        {
            get;
            set;
        }
        public int PercentValue
        {
            get { return (int)Utils.PTop(100, Value + Max, Max - Min); }
            set { Value = (int)((value + Max) * 100.0f / (Max - Min)); }
        }
        public int Max
        {
            get;
            set;
        }
        public int Min
        {
            get;
            set;
        }
        public string Name
        {
            get{ return _name; }
            set{
                _joystickOffset = DIUtils.ID(value);
                _virtualID = DIUtils.VirtualID(value);
                _name = value;
 
            }
        }
        public HID_USAGES VirtualID
        {
            get { return _virtualID; }
            set {
                _name = DIUtils.VirtualName(value);
                _virtualID = value;
                _joystickOffset = DIUtils.ID(_name);
            }
        }
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

        public MouseOffset MouseOffset
        {
            get { return _mouseOffset; }
            set
            {
                _name = value.ToString();
                _mouseOffset = value;
            }
        }

        public Key KeyboardKey
        {
            get { return _key; }
            set
            {
                _name = value.ToString();
                _key = value;
            }
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
