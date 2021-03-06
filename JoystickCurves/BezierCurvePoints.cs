﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
namespace JoystickCurves
{
    public class BezierCurvePoints
    {
        private List<Point> _drawPoints;
        private List<PointF> _rawPoints;
        private int _drawWidth, _drawHeight;
        private int _pointsCount;
        private const int DEFAULTPOINTSCOUNT = 13;
        private object lockPoints = new object();
        public event EventHandler<EventArgs> OnChange;
        public BezierCurvePoints()
        {

            InitPoints(DEFAULTPOINTSCOUNT);           
        }
        public BezierCurvePoints( int pointsCount )
        {
            InitPoints(pointsCount);
        }
        private void InitPoints(int pointsCount)
        {
            if (pointsCount < 4) throw new Exception("Not enough points for Bezier curve!");

            _drawWidth = 100000;
            _drawHeight = 100000;
            _drawPoints = new List<Point>();
            _rawPoints = new List<PointF>();
            PointsCount = pointsCount;
            
        }
        
        public void Reset()
        {
            _rawPoints = Enumerable.Range(0, _pointsCount).AsEnumerable().Select((dp, i) => new PointF((float)i * (1.0f / (_pointsCount - 1)), 0)).ToList();
            ScaleDrawPoints();
        }
        [XmlIgnore]
        public int PointsCount
        {
            get { return _pointsCount; }
            set { _pointsCount = value; }
        }
        [XmlAttribute]
        public CurveResponseType CurveResponseType
        {
            get;
            set;
        }

        [XmlAttribute]
        public int DrawWidth
        {
            get { return _drawWidth; }
            set 
            {
                if (value != _drawWidth)
                {
                    _drawWidth = value;
                    ScaleDrawPoints();
                }
            }
        }
        [XmlAttribute]
        public int DrawHeight
        {
            get { return _drawHeight; }
            set
            {
                if (value != _drawHeight)
                {
                    _drawHeight = value;
                    ScaleDrawPoints();
                }
            }
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
                    return;
                if (value.Count == 0)
                    return;

                _drawPoints = value;
                //ScaleRawPoints();
            }

        }
        [XmlElement]
        public List<PointF> RawPoints
        {
            get
            {

                return _rawPoints;
            }
            set
            {
                if (value == null)
                    return;
                if (value.Count == 0)
                    return;
                
                _rawPoints = value;
                ScaleDrawPoints();
            }

        }

        public BezierCurvePoints GetCopy()
        {
            var newBezier = new BezierCurvePoints(PointsCount);
            newBezier.DrawWidth = DrawWidth;
            newBezier.DrawHeight = DrawHeight;
            newBezier.RawPoints = RawPoints.ToList();
            return newBezier;
        }
        public void ScaleRawPoints(int index = -1)
        {
            lock (lockPoints)
            {
                if (_drawPoints.Count == 0)
                    return;
                if (index == -1)
                {
                    if( _rawPoints == null )
                        _rawPoints = new List<PointF>();

                    for (var i = 0; i < _pointsCount; i++)
                    {
                        Point p = _drawPoints[i];
                        PointF pf = new PointF(Utils.PTop(1, p.X, DrawWidth), Utils.PTop(1, p.Y, DrawHeight));
                        var before = _rawPoints[i];
                        _rawPoints[i] = pf;
                    }
                }
                else
                {
                    var before = _rawPoints[index];
                    _rawPoints[index] = new PointF(Utils.PTop(1, _drawPoints[index].X, DrawWidth), Utils.PTop(1, _drawPoints[index].Y, DrawHeight));
                }
            }
        }
        public PointF InterpolateLinear( PointF a, PointF b, PointF c )
        {
            if ((c.X - b.X) == 0)
            {
                return new PointF(a.X, (b.Y + c.Y) / 2);
            }
            return new PointF(a.X,b.Y + (a.X - b.X) * (c.Y - b.Y) / (c.X - b.X));
        
        }
        public void Streighten()
        {
            if( _rawPoints.Count < 3 )
                return;
            for (int i = 1; i < _rawPoints.Count - 1; i++ )
            {
                _rawPoints[i] = InterpolateLinear( _rawPoints[i], _rawPoints[0], _rawPoints[_rawPoints.Count-1]);
            }
            ScaleDrawPoints();
        }
        PointF Interpolate(PointF a, PointF b, float t)
        {
            return new PointF(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
        }
        public float GetY(float x)
        {
            if (_rawPoints.Count <= 0)
                return 1;

            lock (lockPoints)
            {
                if (x < 0 || x > 1.0f)
                    throw new Exception("X must be set between 0 and 1");

                if (x == 1.0f)
                    return 1 - _rawPoints[_rawPoints.Count - 1].Y;
                if (x == 0)
                    return 1 - _rawPoints[0].Y;

                var i = _rawPoints.AsEnumerable().Select((p, j) => new { Index = j, Value = p.X }).Where(a => a.Index % 3 == 0 && a.Value <= x).LastOrDefault().Index;

                if (i + 4 > _rawPoints.Count)
                    return _rawPoints[_rawPoints.Count - 1].Y;
                
                var firstPoint = _rawPoints[i];
                
                x = (x - firstPoint.X) * 4.0f;
                var offsetPoints = _rawPoints.GetRange(i, 4).Select(p => new PointF(p.X, p.Y)).ToArray();

                var y = 1 - Bezier(offsetPoints[0], offsetPoints[1], offsetPoints[2], offsetPoints[3], x).Y;
                if (y < 0) y = 0;
                else if (y > 1) y = 1;
                return y;
            }
        }
        PointF Bezier(PointF a, PointF b, PointF c, PointF d, float t)
        {
            PointF ab, bc, cd, abbc, bccd;
            ab = Interpolate(a, b, t);           
            bc = Interpolate(b, c, t);          
            cd = Interpolate(c, d, t);        
            abbc = Interpolate(ab, bc, t);      
            bccd = Interpolate(bc, cd, t);      
            return Interpolate(abbc, bccd, t);   
        }



        public void ScaleDrawPoints(int index = -1)
        {
            if (_rawPoints.Count == 0)
                return;
            if (index == -1)
            {
                if (_drawPoints == null || _drawPoints.Count < _pointsCount)
                {
                    _drawPoints = Enumerable.Range(0, _pointsCount).AsEnumerable().Select((dp, i) => new Point(0,0)).ToList();
                }
                for (var i = 0; i < _pointsCount; i++)
                {
                    PointF pf = _rawPoints[i];
                    var before = _drawPoints[i];
                    Point p = new Point((int)Utils.PTop(DrawWidth, pf.X, 1), (int)Utils.PTop(DrawHeight, pf.Y, 1));
                    _drawPoints[i] = p;

                }
            }
            else
            {
                var before = _drawPoints[index];
                _drawPoints[index] = new Point((int)Utils.PTop(DrawWidth, _rawPoints[index].X, 1), (int)Utils.PTop(DrawHeight, _rawPoints[index].Y, 1));
            }
            if (OnChange != null)
                OnChange(this,EventArgs.Empty);
        }

        
    }
}
