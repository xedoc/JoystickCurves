using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Serialization;
namespace JoystickCurves 
{
    public class Profile
    {
        public Profile()
        {
            Title = "New Profile";
            Tabs = new List<ProfileTab>();
        }
        public Profile(string title)
        {
            Tabs = new List<ProfileTab>();
            Title = title;
        }
        [XmlAttribute]
        public string Title
        {
            get;
            set;
        }        
        [XmlElement]
        public List<ProfileTab> Tabs
        {
            get;
            set;
        }
        public static implicit operator String(Profile p)
        {
            return p == null ? String.Empty : p.Title;
        }
        public static implicit operator Profile(String p)
        {
            return new Profile(p);
        }
        public override string ToString()
        {
            return Title;
        }

    }
    public class ProfileTab
    {
        public ProfileTab()
        {
            CurvePoints = new CurvePoints();
            CurvePoints.RawPoints.Add(new System.Drawing.PointF(1.0f, 1.0f));
            CurvePoints.DrawPoints.Add(new System.Drawing.Point(1, 1));
        }
        [XmlAttribute]
        public String TabTitle
        {
            get;set;
        }
        [XmlAttribute]
        public String SourceDevice
        {
            get;set;
        }
        [XmlAttribute]
        public String DestinationDevice
        {
            get;set;
        }
        [XmlAttribute]
        public String SourceAxis
        {
            get;set;
        }
        [XmlAttribute]
        public String DestinationAxis
        {
            get;set;
        }
        [XmlElement]
        public CurvePoints CurvePoints
        {
            get;set;
        }

    }
}
