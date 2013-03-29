using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace JoystickCurves
{
    public class AxisBinding
    {
        public GameController SourceDevice
        {
            get;
            set;
        }
        public VirtualJoystick DestinationDevice
        {
            get;
            set;
        }
        public Axis SourceAxis
        {
            get;
            set;
        }
        public VirtualAxis DestinationAxis
        {
            get;
            set;
        }

        public CurvePoints CurvePoints
        {
            get;
            set;
        }
        

    }
}
