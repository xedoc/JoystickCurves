﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Serialization;

namespace JoystickCurves 
{
    [Serializable]
    public class Profile : XmlSerializableBase<Profile>
    {
        private const string NOTSET = "Not set";

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
        public string BindGame
        {
            get;
            set;
        }
        [XmlAttribute]
        public string BindVehicle
        {
            get;
            set;
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

        [XmlIgnore]
        public List<String> SourceDeviceList
        {
            get {
                return Tabs.Select(t => t.SourceDevice).Distinct().Where( sd => sd != NOTSET).ToList();
            }
        }
        [XmlIgnore]
        public List<String> DestinationDeviceList
        {
            get
            {
                return Tabs.Select(t => t.DestinationDevice).Distinct().Where(sd => sd != NOTSET).ToList();
            }
        }
        [XmlIgnore]
        public List<String> SourceAxisList
        {
            get
            {
                return Tabs.Select(t => t.SourceAxis).Distinct().Where(sd => sd != NOTSET).ToList();
            }
        }
        [XmlIgnore]
        public List<String> DestinationAxisList
        {
            get
            {
                return Tabs.Select(t => t.DestinationAxis).Distinct().Where(sd => sd != NOTSET).ToList();
            }
        }
        [XmlElement]
        public String JoystickHotKey
        {
            get;
            set;
        }
        [XmlElement]
        public String KeyboardHotKey
        {
            get;
            set;
        }
        [XmlElement]
        public String MouseHotKey
        {
            get;
            set;
        }
        [XmlElement]
        public String HotKeyJoystickName
        {
            get;
            set;
        }
        [XmlElement]
        public String HotKeyMouseName
        {
            get;
            set;
        }
        [XmlElement]
        public String HotKeyKeyboardName
        {
            get;
            set;
        }

        public Profile GetCopy()
        {
            Profile profile = new Profile(this.Title);
            foreach (ProfileTab p in this.Tabs.ToList())
            {
                profile.Tabs.Add(new ProfileTab()
                {
                    CurvePoints = new BezierCurvePoints(p.CurvePoints.PointsCount)
                    {
                        RawPoints = p.CurvePoints.RawPoints.ToList(),                        
                        DrawHeight = p.CurvePoints.DrawHeight,
                        CurveResponseType = p.CurvePoints.CurveResponseType,
                        DrawWidth = p.CurvePoints.DrawWidth
                    },
                    DestinationAxis = p.DestinationAxis,
                    DestinationDevice = p.DestinationDevice,
                    SourceAxis = p.SourceAxis,
                    SourceDevice = p.SourceDevice,
                    TabTitle = p.TabTitle,
                    PreserveAxisRange = p.PreserveAxisRange,
                    Correction = p.Correction,
                    FilterLevel = p.FilterLevel
                });
            }
            profile.HotKeyJoystickName = this.HotKeyJoystickName;
            profile.HotKeyKeyboardName = this.HotKeyKeyboardName;
            profile.HotKeyMouseName = this.HotKeyMouseName;
            profile.JoystickHotKey = this.JoystickHotKey;
            profile.MouseHotKey = this.MouseHotKey;
            profile.KeyboardHotKey = this.KeyboardHotKey;

            return profile;
        }
        public void CopyTo( ref Profile profile)
        {
            profile.Tabs.Clear();
            foreach (ProfileTab p in this.Tabs.ToList())
            {
                profile.Tabs.Add(new ProfileTab()
                {
                    CurvePoints = new BezierCurvePoints(p.CurvePoints.PointsCount)
                    {
                        RawPoints = p.CurvePoints.RawPoints.ToList(),
                        DrawHeight = p.CurvePoints.DrawHeight,
                        CurveResponseType = p.CurvePoints.CurveResponseType,
                        DrawWidth = p.CurvePoints.DrawWidth
                    },
                    DestinationAxis = p.DestinationAxis,
                    DestinationDevice = p.DestinationDevice,
                    SourceAxis = p.SourceAxis,
                    SourceDevice = p.SourceDevice,
                    TabTitle = p.TabTitle,
                    PreserveAxisRange = p.PreserveAxisRange,
                    Correction = p.Correction,
                    FilterLevel = p.FilterLevel
                });
            }
            profile.HotKeyJoystickName = this.HotKeyJoystickName;
            profile.HotKeyKeyboardName = this.HotKeyKeyboardName;
            profile.HotKeyMouseName = this.HotKeyMouseName;
            profile.JoystickHotKey = this.JoystickHotKey;
            profile.MouseHotKey = this.MouseHotKey;
            profile.KeyboardHotKey = this.KeyboardHotKey;
        }
    }
    [Serializable]
    public class ProfileTab : XmlSerializableBase<ProfileTab>
    {
        private object lockPoints = new object();
        private BezierCurvePoints _curvePoints;

        public ProfileTab()
        {

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
        [XmlAttribute]
        public double Correction
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FilterLevel
        {
            get;
            set;
        }
        [XmlAttribute]
        public bool PreserveAxisRange
        {
            get;
            set;
        }

        [XmlElement]
        public BezierCurvePoints CurvePoints
        {
            get { 
                lock( lockPoints )
                {
                    return _curvePoints;
                }
            }
            set
            {
                lock (lockPoints)
                {
                    _curvePoints = value;
                }
            }
        }


    }
}
