using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
namespace JoystickCurves
{
    [Serializable()]
    public class ProfileManager
    {
        public ProfileManager()
        {
            
        }
        public String Title
        {
            get;
            set;
        }

        public List<Profile> Profiles
        {
            get;
            set;
        }
    }
}
