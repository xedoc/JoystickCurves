using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace JoystickCurves
{
    class AxisSettings
    {
        public GameController SourceDevice
        {
            get;
            set;
        }
        public GameController DestinationDevice
        {
            get;
            set;
        }
        public Axis SourceAxis
        {
            get;
            set;
        }
        public Axis DestinationAxis
        {
            get;
            set;
        }      
        

    }
}
