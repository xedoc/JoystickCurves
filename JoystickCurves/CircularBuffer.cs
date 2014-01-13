using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoystickCurves
{
    public class CircularBuffer<T>
    {
      T[] buffer; 
      int nextFree;

      public CircularBuffer(int length)
      {
        buffer = new T[length];
        nextFree = 0;
        Length = length;
        IsFilled = false;
      }

      public void Add(T o)
      {
          if (Length <= 0)
              return;

        buffer[nextFree] = o;
        nextFree = (nextFree+1) % buffer.Length;
        if (!IsFilled && nextFree == 0)
            IsFilled = true;
      }
      public T[] Buffer
      {
          get { return buffer; }
      }
      public int Length
      {
          get;
          set;
      }
      public bool IsFilled
      {
          get;
          set;
      }
    }
}
