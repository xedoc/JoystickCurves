using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
namespace JoystickCurves
{
    public class CurvePoints
    {
        private List<Point> _drawPoints;
        private List<PointF> _rawPoints;
        public CurvePoints()
        {
            DrawWidth = 1;
            DrawHeight = 1;
            _rawPoints = new List<PointF>();
            _drawPoints = new List<Point>();
            
        }
        public CurvePoints( int width, int height )
        {
            if (width <= 0 ) throw new Exception("Zero width isn't allowed!");
            if (height <= 0) throw new Exception("Zero height isn't allowed!");

            _rawPoints = new List<PointF>();
            _drawPoints = new List<Point>();

            DrawWidth = width;
            DrawHeight = height;
        }
        [XmlAttribute]
        public int DrawWidth
        {
            get;
            set;
        }
        [XmlAttribute]
        public int DrawHeight
        {
            get;
            set;
        }
        [XmlElement]
        public List<Point> DrawPoints
        {
            get
            {
                return _drawPoints;
            }
            set
            {
                if (value == null)
                    throw new Exception("Null drawpoints!");
                if (value.Count == 0)
                    throw new Exception("Empty drawpoints!");

                _rawPoints.Clear();

                for (var i = 0; i < value.Count; i++)
                {
                    Point p = value[i];                    
                    PointF pf = new PointF(Utils.PTop(1,p.X,DrawWidth), Utils.PTop(1,p.Y,DrawHeight));
                    _rawPoints.Add(pf);
                }
                _drawPoints = value;
            }

        }
        [XmlIgnore]
        public List<PointF> RawPoints
        {
            get
            {
                return _rawPoints;
            }
            set
            {
                if (value == null)
                    throw new Exception("Null rawpoints!");
                if (value.Count == 0)
                    throw new Exception("Empty rawpoints!");

                _drawPoints.Clear();

                for( var i = 0; i < value.Count; i ++)
                {
                    PointF pf = value[i];
                    Point p = new Point((int)Utils.PTop(DrawWidth,pf.X,1), (int)Utils.PTop(DrawHeight,pf.Y, DrawHeight));
                    _drawPoints.Add( p );                    
                }
                _rawPoints = value;
            }
        }

        
    }
}
