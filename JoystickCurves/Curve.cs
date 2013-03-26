using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace WTJoystickCurves
{
    class Curve : Panel
    {
        private Point[] _points = new Point[10];
        private DragRectangle[] _dragPanels = null;
        private bool dontMove = false;
        private float[] _ypoints;
        private ToolTip _tooltip;
        public Curve()
        {
            this.Width = XStep * 10;
            this.Height = YScale;
            this.Paint += new PaintEventHandler(Curve_Paint);

            TopOffset = 0;
            YScale = 100;

            _tooltip = new ToolTip();
            _tooltip.AutoPopDelay = 2000;
            _tooltip.InitialDelay = 0;
            _tooltip.ReshowDelay = 0;
            _tooltip.ShowAlways = true;

        }
        public int LeftOffset
        {
            get;
            set;
        }
        public int TopOffset
        {
            get;
            set;
        }
        private void Draw()
        {
            if (_ypoints == null)
                return;

            for (var i = 0; i < _ypoints.Length; i++)
            {
                var x = i * XStep;
                var y = (int)((1-_ypoints[i]) * YScale)+TopOffset;
                _points[i] = new Point(x + LeftOffset, y);
            }

            if (_dragPanels == null)
            {
                InitDragPoints();
            }

            Invalidate();

        }
        private void InitDragPoints()
        {
            _dragPanels = new DragRectangle[10];
            for (var i = 0; i < _points.Length; i++)
            {
                var p = _points[i];
                p.Offset(-3, -3);
                _dragPanels[i] = new DragRectangle();
                _dragPanels[i].Location = p;
                _dragPanels[i].Index = i;
                _dragPanels[i].MinY = -3 + TopOffset;
                _dragPanels[i].MaxY = YScale + TopOffset - 3;
                _dragPanels[i].Move += new EventHandler(Curve_DragRectangleMove);
                _dragPanels[i].MouseUp += new MouseEventHandler(Curve_MouseUp);
                _dragPanels[i].MouseDown += new MouseEventHandler(Curve_MouseDown);

                this.Controls.Add(_dragPanels[i]);
            }
        }

        void Curve_MouseDown(object sender, MouseEventArgs e)
        {
            DragRectangle dragRect = (DragRectangle)sender;
            var toolText = String.Format("{0}", _ypoints[dragRect.Index]);
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
            newLocation.Offset(3, 3);
            _ypoints[dragRect.Index] = (float)((YScale - newLocation.Y + TopOffset) / (float)YScale);
            var toolText = String.Format("{0}", _ypoints[dragRect.Index]);
            _tooltip.Show(toolText, dragRect,2,2);

            Draw();
        }
        public int YScale
        {
            get;
            set;
        }
        public float[] YPoints
        {
            get { return _ypoints; }
            set {
                if (value == null)
                    return;
                if( value.Length == 10 ) 
                { 
                    _ypoints = value; 
                    Draw();
                    dontMove = true;
                    if (_dragPanels != null)
                    {
                        for (var i = 0; i < _points.Length; i++)
                        {
                            var p = _points[i];
                            p.Offset(-3, -3);
                            _dragPanels[i].Location = p;
                        }
                    }
                    dontMove = false;


                }
            }
        }
        public Point[] Points
        {
            get { return _points; }
            set { 
                if( value.Length == 10 )
                {
                    _points = value;
                    Draw();
                }
            }
        }
        public int XStep
        {
            get;
            set;
        }
        void Curve_Paint(object sender, PaintEventArgs e)
        {
            Pen gridPen = new Pen(Color.LightGray);
            for (var i = TopOffset; i <= YScale + TopOffset; i += (YScale / 10))
            {
                var left = new Point(0, i );
                var right = new Point(this.Right, i );
                e.Graphics.DrawLine(gridPen, left, right);
            }

            Pen curvePen = new Pen(Color.Blue);
            e.Graphics.DrawCurve(curvePen, _points, 0.0f);
        }
        
    }
}
