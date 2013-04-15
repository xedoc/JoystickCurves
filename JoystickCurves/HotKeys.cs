using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.DirectX.DirectInput;
using System.Xml.Serialization;

namespace JoystickCurves
{
    public class HotKeys
    {
        public event EventHandler<EventArgs> OnNextProfile;
        public event EventHandler<EventArgs> OnPrevProfile;
        public event EventHandler<EventArgs> OnDecSensitivity;
        public event EventHandler<EventArgs> OnIncSensitivity;
        public event EventHandler<HotKeyArgs> OnSetSensitivity;

        public HotKeys()
        {
            Keys = new HashSet<HotKey>();
        }
        [XmlElement(ElementName="HotKey")]
        public HashSet<HotKey> Keys
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
        }
        public void RemoveHotkey(HotKey hotkey)
        {
            Keys.RemoveWhere(hk => hk.Title == hk.Title);
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

            switch (hotkey.Type)
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
                case HotKeyType.SetSensitivity:
                    if (OnSetSensitivity!= null) OnSetSensitivity(this, new HotKeyArgs( hotkey ));
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
        SetSensitivity
    }
    public class HotKey
    {
        [XmlElement(ElementName="DirectInputData")]
        public DirectInputData Key
        {
            get;set;
        }
        [XmlAttribute]
        public String Title
        {
            get;set;
        }
        [XmlIgnore]
        public HotKeyType Type
        {
            get;
            set;
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
