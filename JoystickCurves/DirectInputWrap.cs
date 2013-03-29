using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace JoystickCurves
{
    public static class DIUtils
    {
        public static JoystickOffset ID( String name )
        {
            var pair = AllNames.FirstOrDefault(n => n.Value.ToLower() == name.ToLower());
            if( pair.Equals(new KeyValuePair<JoystickOffset,String>()) )
                return 0;
            else
                return pair.Key;
        }
        public static String Name(JoystickOffset id)
        {
            var pair = AllNames.FirstOrDefault(n => n.Key == id);
            if( pair.Equals(new KeyValuePair<JoystickOffset,String>()) )
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
                AllNames[JoystickOffset.RX],
                AllNames[JoystickOffset.RY],
                AllNames[JoystickOffset.RZ],
                AllNames[JoystickOffset.Slider0],
                AllNames[JoystickOffset.Slider1]
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
        public static readonly System.Collections.Generic.Dictionary<JoystickOffset, String> AllNames =
            new Dictionary<JoystickOffset, string>() {
                {JoystickOffset.X, "Roll"},
                {JoystickOffset.Y, "Pitch"},
                {JoystickOffset.Z, "Throttle"},
                {JoystickOffset.RX, "Brakes/Rotary 1"},
                {JoystickOffset.RY, "Brakes/Rotary 2"},
                {JoystickOffset.RZ, "Yaw"},
                {JoystickOffset.Slider0, "Slider 1"},
                {JoystickOffset.Slider1, "Slider 2"},
                {JoystickOffset.PointOfView0, "POV 1"},
                {JoystickOffset.PointOfView1, "POV 2"},
                {JoystickOffset.PointOfView2, "POV 3"},
                {JoystickOffset.PointOfView3, "POV 4"},
                {JoystickOffset.Button0, "Button 0"},
                {JoystickOffset.Button1, "Button 1"},
                {JoystickOffset.Button2, "Button 2"},
                {JoystickOffset.Button3, "Button 3"},
                {JoystickOffset.Button4, "Button 4"},
                {JoystickOffset.Button5, "Button 5"},
                {JoystickOffset.Button6, "Button 6"},
                {JoystickOffset.Button7, "Button 7"},
                {JoystickOffset.Button8, "Button 8"},
                {JoystickOffset.Button9, "Button 9"},
                {JoystickOffset.Button10, "Button 10"},
                {JoystickOffset.Button11, "Button 11"},
                {JoystickOffset.Button12, "Button 12"},
                {JoystickOffset.Button13, "Button 13"},
                {JoystickOffset.Button14, "Button 14"},
                {JoystickOffset.Button15, "Button 15"},
                {JoystickOffset.Button16, "Button 16"},
                {JoystickOffset.Button17, "Button 17"},
                {JoystickOffset.Button18, "Button 18"},
                {JoystickOffset.Button19, "Button 19"},
                {JoystickOffset.Button20, "Button 20"},
                {JoystickOffset.Button21, "Button 21"},
                {JoystickOffset.Button22, "Button 22"},
                {JoystickOffset.Button23, "Button 23"},
                {JoystickOffset.Button24, "Button 24"},
                {JoystickOffset.Button25, "Button 25"},
                {JoystickOffset.Button26, "Button 26"},
                {JoystickOffset.Button27, "Button 27"},
                {JoystickOffset.Button28, "Button 28"},
                {JoystickOffset.Button29, "Button 29"},
                {JoystickOffset.Button30, "Button 30"},
                {JoystickOffset.Button31, "Button 31"},
                {JoystickOffset.Button32, "Button 32"},
                {JoystickOffset.Button33, "Button 33"},
                {JoystickOffset.Button34, "Button 34"},
                {JoystickOffset.Button35, "Button 35"},
                {JoystickOffset.Button36, "Button 36"},
                {JoystickOffset.Button37, "Button 37"},
                {JoystickOffset.Button38, "Button 38"},
                {JoystickOffset.Button39, "Button 39"},
                {JoystickOffset.Button40, "Button 40"},
                {JoystickOffset.Button41, "Button 41"},
                {JoystickOffset.Button42, "Button 42"},
                {JoystickOffset.Button43, "Button 43"},
                {JoystickOffset.Button44, "Button 44"},
                {JoystickOffset.Button45, "Button 45"},
                {JoystickOffset.Button46, "Button 46"},
                {JoystickOffset.Button47, "Button 47"},
                {JoystickOffset.Button48, "Button 48"},
                {JoystickOffset.Button49, "Button 49"},
                {JoystickOffset.Button50, "Button 50"},
                {JoystickOffset.Button51, "Button 51"},
                {JoystickOffset.Button52, "Button 52"},
                {JoystickOffset.Button53, "Button 53"},
                {JoystickOffset.Button54, "Button 54"},
                {JoystickOffset.Button55, "Button 55"},
                {JoystickOffset.Button56, "Button 56"},
                {JoystickOffset.Button57, "Button 57"},
                {JoystickOffset.Button58, "Button 58"},
                {JoystickOffset.Button59, "Button 59"},
                {JoystickOffset.Button60, "Button 60"},
                {JoystickOffset.Button61, "Button 61"},
                {JoystickOffset.Button62, "Button 62"},
                {JoystickOffset.Button63, "Button 63"},
                {JoystickOffset.Button64, "Button 64"},
                {JoystickOffset.Button65, "Button 65"},
                {JoystickOffset.Button66, "Button 66"},
                {JoystickOffset.Button67, "Button 67"},
                {JoystickOffset.Button68, "Button 68"},
                {JoystickOffset.Button69, "Button 69"},
                {JoystickOffset.Button70, "Button 70"},
                {JoystickOffset.Button71, "Button 71"},
                {JoystickOffset.Button72, "Button 72"},
                {JoystickOffset.Button73, "Button 73"},
                {JoystickOffset.Button74, "Button 74"},
                {JoystickOffset.Button75, "Button 75"},
                {JoystickOffset.Button76, "Button 76"},
                {JoystickOffset.Button77, "Button 77"},
                {JoystickOffset.Button78, "Button 78"},
                {JoystickOffset.Button79, "Button 79"},
                {JoystickOffset.Button80, "Button 80"},
                {JoystickOffset.Button81, "Button 81"},
                {JoystickOffset.Button82, "Button 82"},
                {JoystickOffset.Button83, "Button 83"},
                {JoystickOffset.Button84, "Button 84"},
                {JoystickOffset.Button85, "Button 85"},
                {JoystickOffset.Button86, "Button 86"},
                {JoystickOffset.Button87, "Button 87"},
                {JoystickOffset.Button88, "Button 88"},
                {JoystickOffset.Button89, "Button 89"},
                {JoystickOffset.Button90, "Button 90"},
                {JoystickOffset.Button91, "Button 91"},
                {JoystickOffset.Button92, "Button 92"},
                {JoystickOffset.Button93, "Button 93"},
                {JoystickOffset.Button94, "Button 94"},
                {JoystickOffset.Button95, "Button 95"},
                {JoystickOffset.Button96, "Button 96"},
                {JoystickOffset.Button97, "Button 97"},
                {JoystickOffset.Button98, "Button 98"},
                {JoystickOffset.Button99, "Button 99"},
                {JoystickOffset.Button100, "Button 100"},
                {JoystickOffset.Button101, "Button 101"},
                {JoystickOffset.Button102, "Button 102"},
                {JoystickOffset.Button103, "Button 103"},
                {JoystickOffset.Button104, "Button 104"},
                {JoystickOffset.Button105, "Button 105"},
                {JoystickOffset.Button106, "Button 106"},
                {JoystickOffset.Button107, "Button 107"},
                {JoystickOffset.Button108, "Button 108"},
                {JoystickOffset.Button109, "Button 109"},
                {JoystickOffset.Button110, "Button 110"},
                {JoystickOffset.Button111, "Button 111"},
                {JoystickOffset.Button112, "Button 112"},
                {JoystickOffset.Button113, "Button 113"},
                {JoystickOffset.Button114, "Button 114"},
                {JoystickOffset.Button115, "Button 115"},
                {JoystickOffset.Button116, "Button 116"},
                {JoystickOffset.Button117, "Button 117"},
                {JoystickOffset.Button118, "Button 118"},
                {JoystickOffset.Button119, "Button 119"},
                {JoystickOffset.Button120, "Button 120"},
                {JoystickOffset.Button121, "Button 121"},
                {JoystickOffset.Button122, "Button 122"},
                {JoystickOffset.Button123, "Button 123"},
                {JoystickOffset.Button124, "Button 124"},
                {JoystickOffset.Button125, "Button 125"},
                {JoystickOffset.Button126, "Button 126"},
                {JoystickOffset.Button127, "Button 127"},
                {JoystickOffset.Button128, "Button 128"}
            };


    }
}
