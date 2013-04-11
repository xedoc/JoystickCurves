using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using System.Xml.Serialization;
using System.Collections;
namespace JoystickCurves
{
    [Serializable]
    public class ProfileManager : XmlSerializableBase<ProfileManager>
    {
        public ProfileManager()
        {
            Profiles = new List<Profile>();

        }
        [XmlAttribute]
        public String Title
        {
            get;
            set;
        }
        [XmlElement]
        public List<Profile> Profiles
        {
            get;
            set;
        }        

    }
}
