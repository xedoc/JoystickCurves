using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JoystickCurves 
{
    [Serializable()]
    public class Profile
    {
        [UserScopedSetting]
        public string Title
        {
            get;
            set;
        }
        public List<AxisBinding> AxisSettings
        {
            get;
            set;
        }
    }
}
