using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace JoystickCurves
{
    
    public class JoystickData
    {
        private JoystickOffset _joystickOffset;

        private const string UNKNOWN = "Unknown";
        private string _name;
        private HID_USAGES _virtualID;
        public JoystickData()
        {
            Min = -1;
            Max = 1;
            Value = 0;
        }        
        public JoystickData(int min, int max)
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
        public JoystickOffset DirectInputID
        {
            get { return _joystickOffset; }
            set
            {
                _name = value.ToString();
                _joystickOffset = value;
                _virtualID = DIUtils.VirtualID(_name);
            }        
        }
        public static implicit operator String(JoystickData ax)
        {
            return ax == null ? String.Empty : ax.Name;
        }
        public static implicit operator JoystickData(String name)
        {
            return new JoystickData() { Name = name };
        }
    }
}
