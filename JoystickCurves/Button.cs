using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.DirectInput;

namespace JoystickCurves
{
    public class Button
    {
        private JoystickOffset _joystickOffset;
        private string _name;
        public int Value
        {
            get;
            set;
        }
        public string Name
        {
            get { return _name; }
            set
            {
                JoystickOffset result;
                Enum.TryParse<JoystickOffset>(value, out result);
                if ((int)result == 0)
                    _name = "Unknown";
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
    }
}
