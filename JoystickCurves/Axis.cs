using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace JoystickCurves
{
    public class Axis
    {
        public ControlState<int> X
        {
            get;
            set;
        }
        public ControlState<int> Y
        {
            get;
            set;
        }
        public ControlState<int> Z
        {
            get;
            set;
        }
        public ControlState<int> RX
        {
            get;
            set;
        }
        public ControlState<int> RZ
        {
            get;
            set;
        }

    }
}
