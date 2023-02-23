using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.DirectInput;

namespace JoystickCurves
{
    public static class DIUtils
    {
        public static JoystickOffset JoyID( String name )
        {
            if (String.IsNullOrEmpty(name) )
                return 0;

            var pair = AllNames.FirstOrDefault(n => n.Value.ToLower() == name.ToLower());
            if( pair.Equals(new KeyValuePair<JoystickOffset,String>()) )
                return 0;
            else
                return pair.Key;
        }
        public static MouseOffset MouseID(String name)
        {
            if (String.IsNullOrEmpty(name))
                return 0;

            var pair = AllNamesMouse.FirstOrDefault(n => n.Value.ToLower() == name.ToLower());
            if (pair.Equals(new KeyValuePair<MouseOffset, String>()))
                return 0;
            else
                return pair.Key;
        }
        public static HID_USAGES VirtualID(String name)
        {
            if (String.IsNullOrEmpty(name))
                return 0;

            if (AllNames.ContainsValue(name))
                return VirtualIDs[JoyID(name)];
            else
                return 0;
        }
        public static String VirtualName(HID_USAGES id)
        {
            JoystickOffset offset = VirtualIDs.Where(vid => vid.Value == id).FirstOrDefault().Key;
            return AllNames[offset];
        }

        public static String Name(JoystickOffset id)
        {
            var pair = AllNames.FirstOrDefault(n => n.Key == id);
            if (pair.Equals(new KeyValuePair<JoystickOffset, String>()))
                return "Unknown";
            else
                return pair.Value;
        }
        public static String Name(MouseOffset id)
        {
            var pair = AllNamesMouse.FirstOrDefault(n => n.Key == id);
            if (pair.Equals(new KeyValuePair<MouseOffset, String>()))
                return "Unknown";
            else
                return pair.Value;
        }
        public static String[] AxisNames
        {
            get
            {
                return new String[] {
                AllNames[JoystickOffset.X],
                AllNames[JoystickOffset.Y],
                AllNames[JoystickOffset.Z],
                AllNames[JoystickOffset.RotationX],
                AllNames[JoystickOffset.RotationY],
                AllNames[JoystickOffset.RotationZ],
                AllNames[JoystickOffset.Sliders0],
                AllNames[JoystickOffset.Sliders1]
                };
            }
        }

        public static String[] ButtonNames
        {
            get { return AllNames.Where(n => n.Value.ToLower().StartsWith("button")).Select(n => n.Value).ToArray(); }
        }
        public static String[] SliderNames
        {
            get { return AllNames.Where(n => n.Value.ToLower().StartsWith("slider")).Select(n => n.Value).ToArray(); }
        }
        public static readonly System.Collections.Generic.Dictionary<JoystickOffset, HID_USAGES> VirtualIDs =
            new Dictionary<JoystickOffset, HID_USAGES>() {
                {JoystickOffset.X, HID_USAGES.HID_USAGE_X},
                {JoystickOffset.Y, HID_USAGES.HID_USAGE_Y},
                {JoystickOffset.Z, HID_USAGES.HID_USAGE_Z},
                {JoystickOffset.RotationX, HID_USAGES.HID_USAGE_RX},
                {JoystickOffset.RotationY, HID_USAGES.HID_USAGE_RY},
                {JoystickOffset.RotationZ, HID_USAGES.HID_USAGE_RZ},
                {JoystickOffset.Sliders0, HID_USAGES.HID_USAGE_SL0},
                {JoystickOffset.Sliders1, HID_USAGES.HID_USAGE_SL1},
                {JoystickOffset.PointOfViewControllers0, HID_USAGES.HID_USAGE_POV},
                {JoystickOffset.PointOfViewControllers1, HID_USAGES.HID_USAGE_POV},
                {JoystickOffset.PointOfViewControllers2, HID_USAGES.HID_USAGE_POV},
                {JoystickOffset.PointOfViewControllers3, HID_USAGES.HID_USAGE_POV},
            };
        public static readonly System.Collections.Generic.Dictionary<JoystickOffset, String> AllNames =
            new Dictionary<JoystickOffset, string>() {
                {JoystickOffset.X, "Roll"},
                {JoystickOffset.Y, "Pitch"},
                {JoystickOffset.Z, "Throttle"},
                {JoystickOffset.RotationX, "Rotary 1"},
                {JoystickOffset.RotationY, "Rotary 2"},
                {JoystickOffset.RotationZ, "Yaw"},
                {JoystickOffset.Sliders0, "Slider 1"},
                {JoystickOffset.Sliders1, "Slider 2"},
                {JoystickOffset.PointOfViewControllers0, "POV 1"},
                {JoystickOffset.PointOfViewControllers1, "POV 2"},
                {JoystickOffset.PointOfViewControllers2, "POV 3"},
                {JoystickOffset.PointOfViewControllers3, "POV 4"},
                {JoystickOffset.Buttons0, "Button 0"},
                {JoystickOffset.Buttons1, "Button 1"},
                {JoystickOffset.Buttons2, "Button 2"},
                {JoystickOffset.Buttons3, "Button 3"},
                {JoystickOffset.Buttons4, "Button 4"},
                {JoystickOffset.Buttons5, "Button 5"},
                {JoystickOffset.Buttons6, "Button 6"},
                {JoystickOffset.Buttons7, "Button 7"},
                {JoystickOffset.Buttons8, "Button 8"},
                {JoystickOffset.Buttons9, "Button 9"},
                {JoystickOffset.Buttons10, "Button 10"},
                {JoystickOffset.Buttons11, "Button 11"},
                {JoystickOffset.Buttons12, "Button 12"},
                {JoystickOffset.Buttons13, "Button 13"},
                {JoystickOffset.Buttons14, "Button 14"},
                {JoystickOffset.Buttons15, "Button 15"},
                {JoystickOffset.Buttons16, "Button 16"},
                {JoystickOffset.Buttons17, "Button 17"},
                {JoystickOffset.Buttons18, "Button 18"},
                {JoystickOffset.Buttons19, "Button 19"},
                {JoystickOffset.Buttons20, "Button 20"},
                {JoystickOffset.Buttons21, "Button 21"},
                {JoystickOffset.Buttons22, "Button 22"},
                {JoystickOffset.Buttons23, "Button 23"},
                {JoystickOffset.Buttons24, "Button 24"},
                {JoystickOffset.Buttons25, "Button 25"},
                {JoystickOffset.Buttons26, "Button 26"},
                {JoystickOffset.Buttons27, "Button 27"},
                {JoystickOffset.Buttons28, "Button 28"},
                {JoystickOffset.Buttons29, "Button 29"},
                {JoystickOffset.Buttons30, "Button 30"},
                {JoystickOffset.Buttons31, "Button 31"},
                {JoystickOffset.Buttons32, "Button 32"},
                {JoystickOffset.Buttons33, "Button 33"},
                {JoystickOffset.Buttons34, "Button 34"},
                {JoystickOffset.Buttons35, "Button 35"},
                {JoystickOffset.Buttons36, "Button 36"},
                {JoystickOffset.Buttons37, "Button 37"},
                {JoystickOffset.Buttons38, "Button 38"},
                {JoystickOffset.Buttons39, "Button 39"},
                {JoystickOffset.Buttons40, "Button 40"},
                {JoystickOffset.Buttons41, "Button 41"},
                {JoystickOffset.Buttons42, "Button 42"},
                {JoystickOffset.Buttons43, "Button 43"},
                {JoystickOffset.Buttons44, "Button 44"},
                {JoystickOffset.Buttons45, "Button 45"},
                {JoystickOffset.Buttons46, "Button 46"},
                {JoystickOffset.Buttons47, "Button 47"},
                {JoystickOffset.Buttons48, "Button 48"},
                {JoystickOffset.Buttons49, "Button 49"},
                {JoystickOffset.Buttons50, "Button 50"},
                {JoystickOffset.Buttons51, "Button 51"},
                {JoystickOffset.Buttons52, "Button 52"},
                {JoystickOffset.Buttons53, "Button 53"},
                {JoystickOffset.Buttons54, "Button 54"},
                {JoystickOffset.Buttons55, "Button 55"},
                {JoystickOffset.Buttons56, "Button 56"},
                {JoystickOffset.Buttons57, "Button 57"},
                {JoystickOffset.Buttons58, "Button 58"},
                {JoystickOffset.Buttons59, "Button 59"},
                {JoystickOffset.Buttons60, "Button 60"},
                {JoystickOffset.Buttons61, "Button 61"},
                {JoystickOffset.Buttons62, "Button 62"},
                {JoystickOffset.Buttons63, "Button 63"},
                {JoystickOffset.Buttons64, "Button 64"},
                {JoystickOffset.Buttons65, "Button 65"},
                {JoystickOffset.Buttons66, "Button 66"},
                {JoystickOffset.Buttons67, "Button 67"},
                {JoystickOffset.Buttons68, "Button 68"},
                {JoystickOffset.Buttons69, "Button 69"},
                {JoystickOffset.Buttons70, "Button 70"},
                {JoystickOffset.Buttons71, "Button 71"},
                {JoystickOffset.Buttons72, "Button 72"},
                {JoystickOffset.Buttons73, "Button 73"},
                {JoystickOffset.Buttons74, "Button 74"},
                {JoystickOffset.Buttons75, "Button 75"},
                {JoystickOffset.Buttons76, "Button 76"},
                {JoystickOffset.Buttons77, "Button 77"},
                {JoystickOffset.Buttons78, "Button 78"},
                {JoystickOffset.Buttons79, "Button 79"},
                {JoystickOffset.Buttons80, "Button 80"},
                {JoystickOffset.Buttons81, "Button 81"},
                {JoystickOffset.Buttons82, "Button 82"},
                {JoystickOffset.Buttons83, "Button 83"},
                {JoystickOffset.Buttons84, "Button 84"},
                {JoystickOffset.Buttons85, "Button 85"},
                {JoystickOffset.Buttons86, "Button 86"},
                {JoystickOffset.Buttons87, "Button 87"},
                {JoystickOffset.Buttons88, "Button 88"},
                {JoystickOffset.Buttons89, "Button 89"},
                {JoystickOffset.Buttons90, "Button 90"},
                {JoystickOffset.Buttons91, "Button 91"},
                {JoystickOffset.Buttons92, "Button 92"},
                {JoystickOffset.Buttons93, "Button 93"},
                {JoystickOffset.Buttons94, "Button 94"},
                {JoystickOffset.Buttons95, "Button 95"},
                {JoystickOffset.Buttons96, "Button 96"},
                {JoystickOffset.Buttons97, "Button 97"},
                {JoystickOffset.Buttons98, "Button 98"},
                {JoystickOffset.Buttons99, "Button 99"},
                {JoystickOffset.Buttons100, "Button 100"},
                {JoystickOffset.Buttons101, "Button 101"},
                {JoystickOffset.Buttons102, "Button 102"},
                {JoystickOffset.Buttons103, "Button 103"},
                {JoystickOffset.Buttons104, "Button 104"},
                {JoystickOffset.Buttons105, "Button 105"},
                {JoystickOffset.Buttons106, "Button 106"},
                {JoystickOffset.Buttons107, "Button 107"},
                {JoystickOffset.Buttons108, "Button 108"},
                {JoystickOffset.Buttons109, "Button 109"},
                {JoystickOffset.Buttons110, "Button 110"},
                {JoystickOffset.Buttons111, "Button 111"},
                {JoystickOffset.Buttons112, "Button 112"},
                {JoystickOffset.Buttons113, "Button 113"},
                {JoystickOffset.Buttons114, "Button 114"},
                {JoystickOffset.Buttons115, "Button 115"},
                {JoystickOffset.Buttons116, "Button 116"},
                {JoystickOffset.Buttons117, "Button 117"},
                {JoystickOffset.Buttons118, "Button 118"},
                {JoystickOffset.Buttons119, "Button 119"},
                {JoystickOffset.Buttons120, "Button 120"},
                {JoystickOffset.Buttons121, "Button 121"},
                {JoystickOffset.Buttons122, "Button 122"},
                {JoystickOffset.Buttons123, "Button 123"},
                {JoystickOffset.Buttons124, "Button 124"},
                {JoystickOffset.Buttons125, "Button 125"},
                {JoystickOffset.Buttons126, "Button 126"},
                {JoystickOffset.Buttons127, "Button 127"}
                // {JoystickOffset.Buttons128, "Button 128"}
            };
        public static readonly System.Collections.Generic.Dictionary<MouseOffset, String> AllNamesMouse =
            new Dictionary<MouseOffset, string>() {
                {MouseOffset.X, "Roll"},
                {MouseOffset.Y, "Pitch"},
                {MouseOffset.Z, "Throttle"},
                {MouseOffset.Buttons0, "Button 0"},
                {MouseOffset.Buttons1, "Button 1"},
                {MouseOffset.Buttons2, "Button 2"},
                {MouseOffset.Buttons3, "Button 3"},
                {MouseOffset.Buttons4, "Button 4"},
                {MouseOffset.Buttons5, "Button 5"},
                {MouseOffset.Buttons6, "Button 6"},
                {MouseOffset.Buttons7, "Button 7"}
            };


    }
}
