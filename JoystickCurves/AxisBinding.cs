using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace JoystickCurves
{
    public class AxisBinding
    {
        public AxisBinding()
        {
            SourceDevice = new GameController(new Microsoft.DirectX.DirectInput.DeviceInstance(), GameControllerType.Physical);
            DestinationDevice = new VirtualJoystick(1);
            SourceAxis = new Axis(-1, 1);
            DestinationAxis = new VirtualAxis();
            CurvePoints = new CurvePoints(1, 1);

        }

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
