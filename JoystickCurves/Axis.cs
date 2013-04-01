using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace JoystickCurves
{
    
    public class Axis
    {
        private JoystickOffset _joystickOffset;

        private const string UNKNOWN = "Unknown";
        private string _name;
        public Axis()
        {
            Min = -1;
            Max = 1;
            Value = 0;
        }        
        public Axis(int min, int max)
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
                JoystickOffset result;
                Enum.TryParse<JoystickOffset>(value, out result);
                if ((int)result == 0)
                    _name = UNKNOWN;
                else
                {
                    DirectInputID = result;
                    _name = value;
                }

            }
        }
        public JoystickOffset DirectInputID
        {
            get { return _joystickOffset; }
            set
            {
                _name = value.ToString();
                _joystickOffset = value;
            }        
        }
        public static implicit operator String(Axis ax)
        {
            return ax == null ? String.Empty : ax.Name;
        }
        public static implicit operator Axis(String name)
        {
            return new Axis() { Name = name };
        }
    }
}
