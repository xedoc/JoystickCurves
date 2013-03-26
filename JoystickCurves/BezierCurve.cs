using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace WTJoystickCurves
{
    public class BezierCurve : Panel
    {
        private int _pointCount;
        private Point[] _bezierPoints;
        private DragRectangle[] _dragPanels = null;
        private bool dontMove = false;
        private ToolTip _tooltip;

        public BezierCurve()
        {
            _tooltip = new ToolTip();
            _tooltip.AutoPopDelay = 2000;
            _tooltip.InitialDelay = 0;
            _tooltip.ReshowDelay = 0;
            _tooltip.ShowAlways = true;
            this.HandleCreated += new EventHandler(BezierCurve_HandleCreated);
        }

        void BezierCurve_HandleCreated(object sender, EventArgs e)
        {
            PointCount = 4;
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
        private int StepX
        {
            get { return (Size.Width - (Padding.Left + Padding.Right)) / (_pointCount - 1); }
        }

        private void InitCurve()
        {
            var x = Padding.Left;
            var y = Padding.Top;
            _bezierPoints = Enumerable.Range(0, 2).Select(p => new Point(x,y)).ToArray();
            
            for (var i = 1; i < _pointCount - 1; i++)
            {
                x += StepX;
                _bezierPoints = _bezierPoints.Concat(Enumerable.Range(0, 3).Select(
                        p => new Point( x, y ) 
                    )).ToArray();
            }
            x += StepX;
            _bezierPoints = (_bezierPoints.Concat(Enumerable.Range(0, 2).Select(p => new Point(x,y)))).ToArray();

            if (_dragPanels == null)
            {
                InitDragPoints();
            }

            Invalidate();

        }
        private void InitDragPoints()
        {
            _dragPanels = Enumerable.Range(0, _bezierPoints.Count()).Select(dp => new DragRectangle()).ToArray();
            var pointType = DragPointType.NormalPoint;

            var xOffset = _dragPanels[0].Width / -2;
            var yOffset = _dragPanels[0].Height / -2;
            var MaxX = Padding.Left + xOffset;
            var MinX = Padding.Left + xOffset;
            for (var i = 0; i < _bezierPoints.Length; i++)
            {
                var bp = _bezierPoints[i];
                var p = bp;
                p.Offset(xOffset, yOffset);
                _dragPanels[i].Location = p;
                _dragPanels[i].Index = i;
                _dragPanels[i].PointType = pointType;
                _dragPanels[i].MinY = Padding.Top + yOffset;
                _dragPanels[i].MaxY = Size.Height - Padding.Bottom - Padding.Top - yOffset;
                _dragPanels[i].Move += new EventHandler(Curve_DragRectangleMove);
                _dragPanels[i].MouseUp += new MouseEventHandler(Curve_MouseUp);
                _dragPanels[i].MouseDown += new MouseEventHandler(Curve_MouseDown);

                if (pointType == DragPointType.NormalPoint)
                {
                    MaxX = p.X;
                    MinX = p.X;
                    pointType = DragPointType.ControlPoint;
                }
                else if (pointType == DragPointType.ControlPoint)
                {
                    if( _dragPanels[i-1].PointType == DragPointType.NormalPoint )
                    {
                        MaxX = p.X + StepX;
                        MinX = p.X;
                        p.Offset(_dragPanels[i].Width * 2,0);
                        _dragPanels[i].Location = p;
                    }
                    else
                    {
                        MaxX = p.X;
                        MinX = _dragPanels[i-1].MinX;
                        pointType = DragPointType.NormalPoint;
                        p.Offset(_dragPanels[i - 1].Width * -2, 0);
                        _dragPanels[i].Location = p;

                    }
                }
                _dragPanels[i].MaxX = MaxX;
                _dragPanels[i].MinX = MinX;
                this.Controls.Add(_dragPanels[i]);
            }
        }

        void Curve_MouseDown(object sender, MouseEventArgs e)
        {
            DragRectangle dragRect = (DragRectangle)sender;
            var toolText = String.Format("{0}", _bezierPoints[dragRect.Index]);
            _tooltip.Show(toolText, dragRect);
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
            _bezierPoints[dragRect.Index].X = newLocation.X;
            _bezierPoints[dragRect.Index].Y = newLocation.Y;
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


            
            if (_bezierPoints != null)
            {
                e.Graphics.DrawBeziers(bluePen, _bezierPoints);
                var p1 = new Point();
                var p2 = new Point();
                foreach( DragRectangle dp in _dragPanels )
                {
                    if (dp.PointType == DragPointType.ControlPoint)
                    {
                        p1 = _bezierPoints[dp.Index];

                        if (_dragPanels[dp.Index - 1].PointType == DragPointType.NormalPoint)
                        {
                            p2 = _bezierPoints[dp.Index - 1];
                        }
                        else if (_dragPanels[dp.Index + 1].PointType == DragPointType.NormalPoint)
                        {
                            p2 = _bezierPoints[dp.Index + 1];
                        }
                        e.Graphics.DrawLine(grayPen, p1, p2);
                    }
                }
            }

        }
        
    }
}
