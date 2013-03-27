using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoystickCurves
{
    public enum GameControllerType
    {
        Virtual,
        Physical
    }
    public class GameController
    {
        public string Name
        {
            get;
            set;
        }
    }
}
