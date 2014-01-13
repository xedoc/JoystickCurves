using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoystickCurves
{
    public class AxisFilterMedian
    {
        private CircularBuffer<short> _values;
        public AxisFilterMedian(int length)
        {
            _values = new CircularBuffer<short>(length);
            Length = length;
        }
        public short Add( short value )
        {
            _values.Add(value);            
            return Median;
        }
        public bool IsFilled
        {
            get { return _values.IsFilled; }
        }
        public short Median
        {
            get { return Length > 0 ? Utils.Median(_values.Buffer) : (short)0; }
        }
        public int Length
        {
            get;
            set;
        }


    }
}
