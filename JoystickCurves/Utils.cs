using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace JoystickCurves
{
    public static class Utils
    {
        public delegate void SetPropCallback(Control ctrl, string propName, object value);
        public static void SetProperty<TControl, TValue>(this TControl ctrl, string propName, TValue value) where TControl : Control
        {
            if (ctrl.InvokeRequired)
            {
                var d = new SetPropCallback(SetProperty);
                ctrl.Invoke(d, new object[] { ctrl, propName, value });
            }
            else
            {
                Type t = ctrl.GetType();
                t.InvokeMember(propName, BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.Public, null, ctrl, new object[] { value });
            }
        }

        public static float PTop(float bottomleft, float topright, float bottomright)
        {
            return bottomleft * topright / bottomright;
        }
        public static float PBottom(float topleft, float topright, float bottomright)
        {
            return topleft * bottomright / topright;
        }
    }

}
