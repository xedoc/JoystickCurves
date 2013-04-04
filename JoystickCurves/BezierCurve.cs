using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;

namespace JoystickCurves
{
    public class BezierCurve : Panel
    {
        private BezierCurvePoints _points;
        private DragRectangle[] _dragPoints = null;
        private ToolTip _tooltip;
        private Rectangle _drawRectangle;
        private string _toolText;
        private Point newPoint = new Point();
        private System.Threading.Timer frameUpdateTimer;
        public event EventHandler<EventArgs> OnCurveChange;
       
        public BezierCurve()
        {
            _tooltip = new ToolTip();
            _tooltip.AutoPopDelay = 200000;
            _tooltip.InitialDelay = 0;
            _tooltip.ReshowDelay = 0;
            _tooltip.ShowAlways = true;
            _tooltip.UseAnimation = false;

            _points = new BezierCurvePoints();

            this.HandleCreated += new EventHandler(BezierCurve_HandleCreated);
            //this.SetStyle( ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        void BezierCurve_HandleCreated(object sender, EventArgs e)
        {
            _drawRectangle = new Rectangle(Padding.Left, Padding.Top, Size.Width - (Padding.Left + Padding.Right), Size.Height - (Padding.Top + Padding.Bottom));
            InitCurve();

            this.Paint += new PaintEventHandler(Curve_Paint);
            this.Resize += new EventHandler(BezierCurve_Resize);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            frameUpdateTimer = new System.Threading.Timer(new TimerCallback(frameUpdate_Tick), null, 0, 16);
        }

        private void frameUpdate_Tick(object o)
        {
            Invalidate();
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

        public BezierCurvePoints Points
        {
            get { return _points; }
            set {                 
                _points = value;
                _points.ScaleDrawPoints();
                InitCurve();
            }
        }

        public int PointsCount
        {
            get;
            set;
        }
        private void InitCurve()
        {
            if (_drawRectangle.Width == 0 || _drawRectangle.Height == 0)
                return;

            _points.DrawWidth = _drawRectangle.Width;
            _points.DrawHeight = _drawRectangle.Height;

            InitDragPoints();

            Invalidate();

        }
        
        private void InitDragPoints()
        {
            this.Controls.Clear();

            if (_points.DrawPoints.Count == 0)
                return;

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
                p.Offset(xOffset + Padding.Top, yOffset + Padding.Left);
                _dragPoints[i].Location = p;
                _dragPoints[i].Index = i;
                _dragPoints[i].PointType = pointType;
                _dragPoints[i].MinY = Padding.Top + yOffset;
                _dragPoints[i].MaxY = _drawRectangle.Height - yOffset;// - yOffset; //.Height - Padding.Bottom - Padding.Top - yOffset + 2;
                _dragPoints[i].Move += new EventHandler(Curve_DragRectangleMove);
                _dragPoints[i].MouseUp += new MouseEventHandler(Curve_MouseUp);
                _dragPoints[i].MouseDown += new MouseEventHandler(Curve_MouseDown);
                if (pointType == DragPointType.NormalPoint)
                {
                    _dragPoints[i].MaxX = p.X;
                    _dragPoints[i].MinX = p.X;
                    if (i > 2)
                    {
                        _dragPoints[i - 1].MaxX = _dragPoints[i].MaxX;
                        _dragPoints[i - 1].MinX = _dragPoints[i - 3].MaxX;                        
                        _dragPoints[i - 2].MaxX = _dragPoints[i].MaxX;
                        _dragPoints[i - 2].MinX = _dragPoints[i - 3].MaxX;
                    }
                    pointType = DragPointType.ControlPoint;
                }
                else if (   pointType == DragPointType.ControlPoint &&
                            _dragPoints[i - 1].PointType != DragPointType.NormalPoint )
                {
                        pointType = DragPointType.NormalPoint;
                }
                this.Controls.Add(_dragPoints[i]);
            }
        }
        void ShowToolTip( DragRectangle dragRect)
        {
            var newToolText = String.Format("{0:0.00}%", Math.Abs(100.0f - Utils.PTop(100, _points.DrawPoints[dragRect.Index].Y, _points.DrawHeight)));
            if (_toolText != newToolText)
            {
                _tooltip.Show(newToolText, this, Size.Width / 2 - 20, Size.Height);
                _toolText = newToolText;
            }
        }
        void Curve_MouseDown(object sender, MouseEventArgs e)
        {
            DragRectangle dragRect = (DragRectangle)sender;
            ShowToolTip(dragRect);
        }

        void Curve_MouseUp(object sender, MouseEventArgs e)
        {
            DragRectangle dragRect = (DragRectangle)sender;
            _tooltip.Hide(dragRect);
        }
        public void ResetCurve()
        {
            Points.Reset();
            InitCurve();
            if (OnCurveChange != null)
                OnCurveChange(this, EventArgs.Empty);
        }
        public void StopUpdates()
        {
            frameUpdateTimer.Change(Timeout.Infinite, Timeout.Infinite);
            this.Paint -= new PaintEventHandler(Curve_Paint);
            this.Resize -= new EventHandler(BezierCurve_Resize);
        }
        void Curve_DragRectangleMove(object sender, EventArgs e)
        {
            DragRectangle dragRect = (DragRectangle)sender;
            Point newLocation = dragRect.Location;
            newLocation.Offset(dragRect.Width/2, dragRect.Height/2);
            if (!newLocation.Equals(_points.DrawPoints[dragRect.Index]))
            {
                newPoint.X = newLocation.X - Padding.Left;
                newPoint.Y = newLocation.Y - Padding.Top;               

                if( dragRect.Location.Y == dragRect.MaxY )
                    newPoint.Y = _points.DrawHeight;
                else if (dragRect.Location.Y == dragRect.MinY)
                    newPoint.Y = 0;

                _points.DrawPoints[dragRect.Index] = newPoint;
                _points.ScaleRawPoints(dragRect.Index);

                if (OnCurveChange != null)
                   OnCurveChange(this, EventArgs.Empty);

                ShowToolTip(dragRect);

            }

        }
        private void DrawHorizontalLines(PaintEventArgs e)
        {
            var dashValues = new Single[2] { 5, 5 };
            Pen gridPen = new Pen(Color.LightGray);

            gridPen.DashPattern = dashValues;
            for (var i = 0; i < _drawRectangle.Height; i += (_drawRectangle.Height / (HorizontalLines - 1)))
            {
                var left = new Point(Padding.Left, i + Padding.Top);
                var right = new Point(_drawRectangle.Width + Padding.Left, i + Padding.Top);
                if (right.X < left.X)
                    right = left;
                e.Graphics.DrawLine(gridPen, left, right);
            }

        }
        private void DrawHelperLInes(PaintEventArgs e)
        {
            if (_dragPoints == null)
                return;
            var dashValues = new Single[2] { 5, 5 };
            Pen grayPen = new Pen(Color.DarkGray, 1);
            grayPen.DashPattern = dashValues;

            var p1 = new Point();
            var p2 = new Point();
            foreach (DragRectangle dp in _dragPoints)
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
                    p1.Offset(Padding.Left, Padding.Top);
                    p2.Offset(Padding.Left, Padding.Top);
                    e.Graphics.DrawLine(grayPen, p1, p2);
                }
            }
        }
        private void DrawCurve(PaintEventArgs e)
        {
            var dashValues = new Single[2] { 5, 5 };
            Pen bluePen = new Pen(Color.Blue, 2);
            Pen grayPen = new Pen(Color.DarkGray, 1);
            grayPen.DashPattern = dashValues;

            if (_points.DrawPoints != null && _points.DrawPoints.Count >= 4)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawBeziers(bluePen, _points.DrawPoints.Select( p => new Point(p.X + Padding.Left, p.Y + Padding.Top)).ToArray());
                DrawHelperLInes(e);
            }
        }
        void Curve_Paint(object sender, PaintEventArgs e)
        {
            DrawHorizontalLines(e);
            DrawCurve(e);
        }
    }
}
