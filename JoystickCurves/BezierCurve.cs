using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace JoystickCurves
{
    public class BezierCurve : Panel
    {
        private int _pointCount;
        private CurvePoints _points;
        private DragRectangle[] _dragPoints = null;
        private bool dontMove = false;
        private ToolTip _tooltip;

        public BezierCurve()
        {
            _tooltip = new ToolTip();
            _tooltip.AutoPopDelay = 200000;
            _tooltip.InitialDelay = 0;
            _tooltip.ReshowDelay = 0;
            _tooltip.ShowAlways = true;
            this.HandleCreated += new EventHandler(BezierCurve_HandleCreated);
        }

        void BezierCurve_HandleCreated(object sender, EventArgs e)
        {
            PointCount = 4;
            _points = new CurvePoints();
            InitCurve();
            Padding = new Padding(10, 10, 10, 10);
            HorizontalLines = 5;
            this.Paint += new PaintEventHandler(Curve_Paint);
            this.Resize += new EventHandler(BezierCurve_Resize);
        }

        void BezierCurve_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        public int HorizontalLines
        {
            get;
            set;
        }
        public int PointCount
        {
            get{ return _pointCount; }
            set{ _pointCount = value; }
        }

        public CurvePoints Points
        {
            get { return _points; }
            set {                 
                _points = value;
                InitCurve();
           
            }
        }
        private int StepX
        {
            get { return (Size.Width - (Padding.Left + Padding.Right)) / (_pointCount - 1); }
        }

        private void InitCurve()
        {

            var x = Padding.Left;
            var y = Padding.Top;
            _points.DrawWidth = Size.Width - Padding.Left - Padding.Right;
            _points.DrawHeight = Size.Height - Padding.Top - Padding.Bottom;

            if (_points.DrawPoints.Count == 0 )
            {
                _points.DrawPoints = Enumerable.Range(0, 2).Select(p => new Point(x, y)).ToList();
            
                for (var i = 1; i < _pointCount - 1; i++)
                {
                    x += StepX;
                    _points.DrawPoints = _points.DrawPoints.Concat(Enumerable.Range(0, 3).Select(
                            p => new Point( x, y ) 
                        )).ToList();
                }
                x += StepX;
                _points.DrawPoints = (_points.DrawPoints.Concat(Enumerable.Range(0, 2).Select(p => new Point(x,y)))).ToList();
            }

            if (_dragPoints == null)
            {
                InitDragPoints();
            }

            Invalidate();

        }
        
        private void InitDragPoints()
        {
            _dragPoints = Enumerable.Range(0, _points.DrawPoints.Count()).Select(dp => new DragRectangle()).ToArray();
            var pointType = DragPointType.NormalPoint;

            var xOffset = _dragPoints[0].Width / -2;
            var yOffset = _dragPoints[0].Height / -2;
            var MaxX = Padding.Left + xOffset;
            var MinX = Padding.Left + xOffset;
            for (var i = 0; i < _points.DrawPoints.Count; i++)
            {
                var bp = _points.DrawPoints[i];
                var p = bp;
                p.Offset(xOffset, yOffset);
                _dragPoints[i].Location = p;
                _dragPoints[i].Index = i;
                _dragPoints[i].PointType = pointType;
                _dragPoints[i].MinY = Padding.Top + yOffset;
                _dragPoints[i].MaxY = Size.Height - Padding.Bottom - Padding.Top - yOffset + 1;
                _dragPoints[i].Move += new EventHandler(Curve_DragRectangleMove);
                _dragPoints[i].MouseUp += new MouseEventHandler(Curve_MouseUp);
                _dragPoints[i].MouseDown += new MouseEventHandler(Curve_MouseDown);

                if (pointType == DragPointType.NormalPoint)
                {
                    MaxX = p.X;
                    MinX = p.X;
                    pointType = DragPointType.ControlPoint;
                }
                else if (pointType == DragPointType.ControlPoint)
                {
                    if( _dragPoints[i-1].PointType == DragPointType.NormalPoint )
                    {
                        MaxX = p.X + StepX;
                        MinX = p.X;
                        p.Offset(_dragPoints[i].Width * 2,0);
                        _dragPoints[i].Location = p;
                    }
                    else
                    {
                        MaxX = p.X;
                        MinX = _dragPoints[i-1].MinX;
                        pointType = DragPointType.NormalPoint;
                        p.Offset(_dragPoints[i - 1].Width * -2, 0);
                        _dragPoints[i].Location = p;

                    }
                }
                _dragPoints[i].MaxX = MaxX;
                _dragPoints[i].MinX = MinX;
                this.Controls.Add(_dragPoints[i]);
            }
        }

        void Curve_MouseDown(object sender, MouseEventArgs e)
        {
            DragRectangle dragRect = (DragRectangle)sender;
            var toolText = String.Format("{0:0.00}%", Math.Abs(100.0f - Utils.PTop(100, _points.DrawPoints[dragRect.Index].Y - Padding.Top, Size.Height - Padding.Top - Padding.Bottom - 1)));
            _tooltip.Show(toolText, dragRect, dragRect.Width, dragRect.Height);
        }

        void Curve_MouseUp(object sender, MouseEventArgs e)
        {
            DragRectangle dragRect = (DragRectangle)sender;
            _tooltip.Hide(dragRect);
        }

        void Curve_DragRectangleMove(object sender, EventArgs e)
        {
            if (dontMove)
                return;

            DragRectangle dragRect = (DragRectangle)sender;
            Point newLocation = dragRect.Location;
            newLocation.Offset(dragRect.Width/2, dragRect.Height/2);
            _points.DrawPoints[dragRect.Index] = new Point(newLocation.X, newLocation.Y);

            var toolText = String.Format("{0:0.00}%", Math.Abs(100.0f - Utils.PTop(100, _points.DrawPoints[dragRect.Index].Y - Padding.Top, Size.Height - Padding.Top - Padding.Bottom - 1)));
            _tooltip.Show(toolText, dragRect,dragRect.Width,dragRect.Height);

            Invalidate();

        }
        void Curve_Paint(object sender, PaintEventArgs e)
        {

            var dashValues = new Single[2] { 5, 5 };
            
            Pen gridPen = new Pen(Color.LightGray);
            Pen bluePen = new Pen(Color.Blue,2);
            Pen grayPen = new Pen(Color.Black);
            gridPen.DashPattern = dashValues;
            grayPen.DashPattern = dashValues;
            for (var i = Padding.Top; i < (Size.Height - Padding.Bottom); i += ((Size.Height - (Padding.Top + Padding.Bottom)) / (HorizontalLines - 1)) )
            {
                var left = new Point(Padding.Left, i );
                var right = new Point(Size.Width - (Padding.Left + Padding.Right), i );
                if (right.X < left.X)
                    right = left;
                e.Graphics.DrawLine(gridPen, left, right);
            }


            
            if (_points.DrawPoints != null)
            {
                e.Graphics.DrawBeziers(bluePen, _points.DrawPoints.ToArray());
                var p1 = new Point();
                var p2 = new Point();
                foreach( DragRectangle dp in _dragPoints )
                {
                    if (dp.PointType == DragPointType.ControlPoint)
                    {
                        p1 = _points.DrawPoints[dp.Index];

                        if (_dragPoints[dp.Index - 1].PointType == DragPointType.NormalPoint)
                        {
                            p2 = _points.DrawPoints[dp.Index - 1];
                        }
                        else if (_dragPoints[dp.Index + 1].PointType == DragPointType.NormalPoint)
                        {
                            p2 = _points.DrawPoints[dp.Index + 1];
                        }
                        e.Graphics.DrawLine(grayPen, p1, p2);
                    }
                }
            }

        }
        
    }
}
