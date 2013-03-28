using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace JoystickCurves
{
    class AxisBinding
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
        public VirtualAxis SourceAxis
        {
            get;
            set;
        }
        public VirtualAxis DestinationAxis
        {
            get;
            set;
        }      
        

    }
}
