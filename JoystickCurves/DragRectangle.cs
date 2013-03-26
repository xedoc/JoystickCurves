using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WTJoystickCurves
{
    class DragRectangle: Panel
    {
        private Point _Offset;
        public DragRectangle()
        {
            Width = 6;
            Height = 6;
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

                if (newY >= MinY && newY <= MaxY)
                {
                    this.Location = new Point(this.Location.X, newY);
                }
                else if( newY > MaxY )
                {
                    this.Location = new Point(this.Location.X, MaxY);
                }
                else if (newY < MinY)
                {
                    this.Location = new Point(this.Location.X, MinY);
                }
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
