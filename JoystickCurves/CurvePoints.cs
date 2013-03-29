using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace JoystickCurves
{
    public class CurvePoints
    {
        private Point[] _drawPoints;
        private PointF[] _rawPoints;
        public CurvePoints( int width, int height )
        {
            if (width <= 0 ) throw new Exception("Zero width isn't allowed!");
            if (height <= 0) throw new Exception("Zero height isn't allowed!");

            DrawWidth = width;
            DrawHeight = height;
        }
        public int DrawWidth
        {
            get;
            set;
        }
        public int DrawHeight
        {
            get;
            set;
        }

        public Point[] DrawPoints
        {
            get
            {
                return _drawPoints;
            }
            set
            {
                if (value == null)
                    throw new Exception("Null drawpoints!");
                if (value.Length == 0)
                    throw new Exception("Empty drawpoints!");
                
                _rawPoints = new PointF[value.Length];

                for (var i = 0; i < value.Length; i++)
                {
                    Point p = value[i];
                    
                    PointF pf = new PointF((float)p.X / (float)DrawWidth, (float)p.Y / (float)DrawHeight);
                    _rawPoints[i] = pf;
                }   

            }

        }

        public PointF[] RawPoints
        {
            get
            {
                return _rawPoints;
            }
            set
            {
                if (value == null)
                    throw new Exception("Null rawpoints!");
                if (value.Length == 0)
                    throw new Exception("Empty rawpoints!");
                
                _drawPoints = new Point[value.Length];

                for( var i = 0; i < value.Length; i ++)
                {
                    PointF pf = value[i];
                    Point p = new Point( (int)(pf.X * (float)DrawWidth), (int)(pf.Y * (float)DrawHeight) );
                    _drawPoints[i] = p;                    
                }              
            }
        }

        
    }
}
