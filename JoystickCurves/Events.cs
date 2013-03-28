using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoystickCurves
{
    public class CustomEventArgs<T> : EventArgs
    {
        public CustomEventArgs(T param)
        {
            Data = param;
        }
        public T Data
        {
            get;
            set;
        }
    }

}
