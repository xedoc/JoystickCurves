using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SharpDX.DirectInput;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace JoystickCurves
{
    public class HotKeys
    {
        public event EventHandler<EventArgs> OnNextProfile;
        public event EventHandler<EventArgs> OnPrevProfile;
        public event EventHandler<EventArgs> OnDecSensitivity;
        public event EventHandler<EventArgs> OnIncSensitivity;
        public event EventHandler<HotKeyArgs> OnSetLowSensitivity;
        public event EventHandler<HotKeyArgs> OnSetMediumSensitivity;
        public event EventHandler<HotKeyArgs> OnSetHighSensitivity;
        public event EventHandler<EventArgs> OnChange;

        private ObservableCollection<HotKey> _keys;
        public HotKeys()
        { 

        }

        [XmlElement(ElementName = "HotKey")]
        public ObservableCollection<HotKey> Keys
        {
            get;
            set;
        }
        public void AddHotKey(HotKey hotkey)
        {

            if (hotkey == null)
                return;

            RemoveHotkey(hotkey);
            Keys.Add(hotkey);

            if (OnChange != null)
                OnChange(this, EventArgs.Empty);
        }
        public void RemoveHotkey(HotKey hotkey)
        {
            var todelete = Keys.FirstOrDefault(hk => hk.Title == hotkey.Title);
            if (todelete != null)
                Keys.Remove(todelete);
        }
        public void ProcessData( DirectInputData data )
        {
            HotKey hotkey = null;
            var hotKeysByType = Keys.Where( hk => hk.Key.Type == data.Type );

            if( hotKeysByType.Count() <= 0 )
                return;


            switch (data.Type)
            {
                case DIDataType.Joystick:
                    hotkey = hotKeysByType.FirstOrDefault(hk => hk.Key.JoystickOffset == data.JoystickOffset);
                    break;

                case DIDataType.Mouse:
                    hotkey = hotKeysByType.FirstOrDefault(hk => hk.Key.MouseOffset == data.MouseOffset);
                    break;

                case DIDataType.Keyboard:
                    hotkey = hotKeysByType.FirstOrDefault(hk => hk.Key.KeyboardKey == data.KeyboardKey);
                    break;
            }

            if (hotkey == null)
                return;

            switch (hotkey.HotKeyType)
            {
                case HotKeyType.NextProfile:
                    if (OnNextProfile != null) OnNextProfile(this, EventArgs.Empty);
                    break;
                case HotKeyType.PrevProfile:
                    if (OnPrevProfile != null) OnPrevProfile(this, EventArgs.Empty);
                    break;
                case HotKeyType.DecSensitivity:
                    if (OnDecSensitivity != null) OnDecSensitivity(this, EventArgs.Empty);
                    break;
                case HotKeyType.IncSensitivity:
                    if (OnIncSensitivity != null) OnIncSensitivity(this, EventArgs.Empty);
                    break;
                case HotKeyType.SetLowSensitivity:
                    if (OnSetLowSensitivity != null) OnSetLowSensitivity(this, new HotKeyArgs(hotkey));
                    break;
                case HotKeyType.SetMediumSensitivity:
                    if (OnSetMediumSensitivity != null) OnSetMediumSensitivity(this, new HotKeyArgs(hotkey));
                    break;
                case HotKeyType.SetHighSensitivity:
                    if (OnSetHighSensitivity != null) OnSetHighSensitivity(this, new HotKeyArgs(hotkey));
                    break;
            }
        }
    }
    public enum HotKeyType
    {
        NextProfile,
        PrevProfile,
        DecSensitivity,
        IncSensitivity,
        SetLowSensitivity,
        SetMediumSensitivity,
        SetHighSensitivity
    }
    public class HotKey
    {
        private HotKeyType _type;

        [XmlElement(ElementName="DirectInputData")]
        public DirectInputData Key
        {
            get;set;
        }
        [XmlIgnore]
        public String Title
        {
            get;set;
        }
        [XmlIgnore]
        public String DisplayTitle
        {
            get
            {
                var result = Title;
                if (Key != null)
                {
                    var device = Key.DeviceName;
                    var button = "";
                    switch( Key.Type )
                    {
                        case DIDataType.Joystick:
                            button = Key.JoystickOffset.ToString();
                            break;
                        case DIDataType.Keyboard:
                            button = Key.KeyboardKey.ToString();
                            break;
                        case DIDataType.Mouse:
                            button = Key.MouseOffset.ToString();
                            break;

                    }
                    return String.Format("{0}\t{1}\t{2}", Title, device, button);
                }
                return result;
            }
        }
        [XmlIgnore]
        public HotKeyType HotKeyType
        {
            get { return _type; }
            set
            {
                _type = value;
                switch (value)
                {
                    case HotKeyType.DecSensitivity:
                        Title = "Decrease sensitivity";
                        break;
                    case HotKeyType.IncSensitivity:
                        Title = "Increase sensitivity";
                        break;
                    case HotKeyType.NextProfile:
                        Title = "Next profile";
                        break;
                    case HotKeyType.PrevProfile:
                        Title = "Previous profile";
                        break;
                    case HotKeyType.SetLowSensitivity:
                        Title = "Low sensitivity";
                        break;
                    case HotKeyType.SetMediumSensitivity:
                        Title = "Medium sensitivity";
                        break;
                    case HotKeyType.SetHighSensitivity:
                        Title = "High sensitivity";
                        break;
                }

            }
        }
        [XmlAttribute(AttributeName = "HotKeyType")]
        public String TypeString
        {
            get { return HotKeyType.ToString(); }
            set { HotKeyType v; Enum.TryParse<HotKeyType>(value, out v); HotKeyType = v; }
        }
        [XmlAttribute]
        public float FloatValue1
        {
            get;
            set;
        }
        [XmlAttribute]
        public int IntValue1
        {
            get;
            set;
        }
        [XmlAttribute]
        public bool BoolValue1
        {
            get;
            set;
        }
        [XmlAttribute]
        public bool StringValue1
        {
            get;
            set;
        }
        [XmlAttribute]
        public bool HoldToActivate
        {
            get;
            set;
        }

    }
    public class HotKeyArgs : EventArgs
    {
        public HotKeyArgs(HotKey hotkey)
        {
            HotKey = hotkey;
        }
        public HotKey HotKey
        {
            get;
            set;
        }
    }
}
