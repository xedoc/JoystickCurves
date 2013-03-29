using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace JoystickCurves
{
    public enum DragPointType
    {
        NormalPoint,
        ControlPoint
    }
    class DragRectangle: Panel
    {
        private Point _Offset;
        private DragPointType _pointType;
        public DragRectangle()
        {
            Width = 9;
            Height = 9;
            _Offset = Point.Empty;
            BackColor = Color.Red;
            this.MouseDown += new MouseEventHandler(DragRectangle_MouseDown);
            this.MouseUp += new MouseEventHandler(DragRectangle_MouseUp);
            this.MouseMove += new MouseEventHandler(DragRectangle_MouseMove);
            this.MouseEnter += new EventHandler(DragRectangle_MouseEnter);
            this.MouseLeave += new EventHandler(DragRectangle_MouseLeave);
        }
        public int MaxY
        {
            get;
            set;
        }
        public int MinY
        {
            get;
            set;
        }
        public int MaxX
        {
            get;
            set;
        }
        public int MinX
        {
            get;
            set;
        }
        public DragPointType PointType
        {
            get { return _pointType; }
            set
            {
                _pointType = value;
                if (_pointType == DragPointType.ControlPoint)
                {
                    BackColor = Color.DarkGray;
                }
            }
        }
        void DragRectangle_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        void DragRectangle_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Cross;
        }

        void DragRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Offset != Point.Empty)
            {

                Point newlocation = this.Location;

                var newY = newlocation.Y + e.Y - _Offset.Y;
                var newX = newlocation.X + e.X - _Offset.X;

                if (newY > MaxY)
                    newY = MaxY;
                else if (newY < MinY)
                    newY = MinY;

                if (newX > MaxX)
                    newX = MaxX;
                else if (newX < MinX)
                    newX = MinX;

                this.Location = new Point(newX, newY);
            }
        }

        void DragRectangle_MouseUp(object sender, MouseEventArgs e)
        {
            _Offset = Point.Empty;

        }

        void DragRectangle_MouseDown(object sender, MouseEventArgs e)
        {
            _Offset = new Point(e.X, e.Y);
        }
        public int Index
        {
            get;
            set;
        }
    }
}
