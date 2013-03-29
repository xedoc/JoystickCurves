using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace JoystickCurves
{
    public enum Reticle
    {
        Cross,
        VerticalLine
    }
    public partial class JoystickTester : Panel
    {
        public const int RETICLE_SIZE = 20;
        private Point _physicalRudderLocation;
        private Point _physicalHandleLocation;
        private Point _virtualRudderLocation;
        private Point _virtualHandleLocation;


        public JoystickTester()
        {
            InitializeComponent();
            Init();
        }

        public JoystickTester(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }

        private void Init()
        {
           this.HandleCreated += new EventHandler(JoystickTester_HandleCreated);
           this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
           Invalidate();
        }

        void JoystickTester_HandleCreated(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(JoystickTester_Paint);
            PhysicalAxisPitch = new Axis();
            PhysicalAxisRoll = new Axis();
            PhysicalAxisRudder = new Axis();

            VirtualAxisPitch = new Axis();
            VirtualAxisRoll = new Axis();
            VirtualAxisRudder = new Axis();
            Invalidate();
        }

        public Rectangle HandleBounds
        {
            get;
            set;
        }
        public Rectangle RudderBounds
        {
            get;
            set;
        }
        public Point PhysicalRudderLocation
        {
            get { return _physicalRudderLocation; }
            set {
                if (!_physicalRudderLocation.Equals(value))
                {
                    _physicalRudderLocation = value;
                    Invalidate();
                }
            }
        }
        public Point PhysicalHandleLocation
        {
            get { return _physicalHandleLocation; }
            set
            {
                if (!_physicalHandleLocation.Equals(value))
                {
                    _physicalHandleLocation = value;
                    Invalidate();
                }
            }
        }
        public Point VirtualRudderLocation
        {
            get { return _virtualRudderLocation; }
            set
            {
                if (!_virtualRudderLocation.Equals(value))
                {
                    _virtualRudderLocation = value;
                    Invalidate();
                }
            }

        }
        public Point VirtualHandleLocation
        {
            get { return _virtualHandleLocation; }
            set
            {
                if (!_virtualHandleLocation.Equals(value))
                {
                    _virtualHandleLocation = value;
                    Invalidate();
                }
            }

        }
        public Axis PhysicalAxisRoll
        {
            set {
                PhysicalHandleLocation = new Point(
                    HandleBounds.X + CoordByAxisValue(value, PhysicalHandleLocation.X, HandleBounds.Width), 
                    PhysicalHandleLocation.Y);
            }
        }
        public Axis PhysicalAxisPitch
        {
            set
            {
                PhysicalHandleLocation = new Point(
                    PhysicalHandleLocation.X, 
                    HandleBounds.Y + CoordByAxisValue(value, PhysicalHandleLocation.Y, HandleBounds.Height)
                    );
            }
        }

        public Axis PhysicalAxisRudder
        {
            set {
                PhysicalRudderLocation = new Point(
                    RudderBounds.X + CoordByAxisValue(value, PhysicalRudderLocation.X, RudderBounds.Width), 
                    RudderBounds.Y
                    );
            }
        }
        public Axis VirtualAxisRoll
        {
            set
            {
                VirtualHandleLocation = new Point(
                    HandleBounds.X + CoordByAxisValue(value, VirtualHandleLocation.X, HandleBounds.Width), 
                    VirtualHandleLocation.Y
                    );
            }
        }
        public Axis VirtualAxisPitch
        {
            set
            {
                VirtualHandleLocation = new Point(
                    VirtualHandleLocation.X, 
                    HandleBounds.Y + CoordByAxisValue(value, VirtualHandleLocation.Y, HandleBounds.Height)
                    );
            }
        }

        public Axis VirtualAxisRudder
        {
            set
            {
                VirtualRudderLocation = new Point( 
                    RudderBounds.X + CoordByAxisValue(value, VirtualRudderLocation.X, RudderBounds.Width), 
                    RudderBounds.Y
                    );
            }
        }

        private int CoordByAxisValue(Axis value, int coord, int bound)
        {
            int newCoord = (int)Utils.PTop(bound, value.Value - value.Min, value.Max - value.Min);
            Debug.Print(value.Value.ToString() + " " + coord.ToString() + " " + newCoord);
            return newCoord;
        }
        void JoystickTester_Paint(object sender, PaintEventArgs e)
        {
            var dashValues = new Single[] { 1, 1};
            var dashCircleValues = new Single[] { 5, 5};
            var smallestSide = Math.Min(Size.Width - Margin.Left - Margin.Right, Size.Height - Margin.Top - Margin.Bottom);
            var drawArea = new Rectangle(Margin.Left,Margin.Top,smallestSide,smallestSide);

            var green = 80;
            var gridPen = new Pen(Color.FromArgb(0, green, 0));
            gridPen.Width = 1;
            gridPen.DashPattern = dashCircleValues;

            e.Graphics.DrawEllipse(gridPen, drawArea);

            for (var i = 0; i < 5; i++)
            {
                var delta = smallestSide/10;
                delta*= i;
                green += 10;
                gridPen.Color = Color.FromArgb(0, green, 0);
                gridPen.DashPattern = dashCircleValues;
                e.Graphics.DrawEllipse(gridPen, new Rectangle(drawArea.X + delta, drawArea.Y + delta, drawArea.Width - delta * 2, drawArea.Height - delta * 2));
            }
            
            gridPen = new Pen(Color.FromArgb(0,120,0));
            var topLeft = new Point(Margin.Left, Margin.Top);
            var topMiddle = new Point(smallestSide / 2 + Margin.Left, Margin.Top);
            var topRight = new Point(smallestSide + Margin.Left, Margin.Top);
            var leftMiddle = new Point(Margin.Left, smallestSide / 2 + Margin.Top);
            var bottomLeft = new Point(Margin.Left, smallestSide + Margin.Top);
            var bottomMiddle = new Point(smallestSide / 2 + Margin.Left, smallestSide + Margin.Top);
            var bottomRight = new Point(smallestSide + Margin.Left, smallestSide + Margin.Top);
            var rightMiddle = new Point(smallestSide + Margin.Left, smallestSide / 2 + Margin.Top);
            var center = new Point(smallestSide/2 + Margin.Left, smallestSide/2 + Margin.Top);

            gridPen.DashPattern = dashValues;

            //Diagonal
            e.Graphics.DrawLine(gridPen, topLeft, new Point(center.X - 10, center.Y - 10));
            e.Graphics.DrawLine(gridPen, topRight, new Point(center.X + 10, center.Y - 10));
            e.Graphics.DrawLine(gridPen, bottomLeft, new Point(center.X - 10, center.Y + 10));
            e.Graphics.DrawLine(gridPen, bottomRight, new Point(center.X + 10, center.Y + 10));

            e.Graphics.DrawLine(gridPen, topMiddle, new Point(center.X, center.Y - 10));
            e.Graphics.DrawLine(gridPen, bottomMiddle, new Point(center.X, center.Y + 10));                        
            e.Graphics.DrawLine(gridPen, leftMiddle, new Point(center.X - 10, center.Y));
            e.Graphics.DrawLine(gridPen, rightMiddle, new Point(center.X + 10, center.Y));

            //Yaw
            
            var pointLeftRudder = new Point(Margin.Left, Size.Height - 20);
            var pointRightRudder = new Point(smallestSide + Margin.Left, Size.Height - 20);
            var rudderWidth = pointRightRudder.X - pointLeftRudder.X;
            e.Graphics.DrawLine(gridPen, pointLeftRudder, pointRightRudder);

            HandleBounds = new Rectangle(topLeft.X - RETICLE_SIZE / 2, topLeft.Y - RETICLE_SIZE / 2, smallestSide, smallestSide);
            RudderBounds = new Rectangle(pointLeftRudder.X - RETICLE_SIZE / 2, pointLeftRudder.Y - RETICLE_SIZE / 2, rudderWidth, RETICLE_SIZE);



            var pen = new Pen(Color.FromArgb(0, 150, 0));
            DrawReticle(e.Graphics, Reticle.VerticalLine, PhysicalRudderLocation, pen);
            DrawReticle(e.Graphics, Reticle.Cross, PhysicalHandleLocation, pen);

            pen = new Pen(Color.FromArgb(0, 255, 0));
            DrawReticle(e.Graphics, Reticle.VerticalLine, VirtualRudderLocation, pen);
            DrawReticle(e.Graphics, Reticle.Cross, VirtualHandleLocation, pen);
        }
        public void DrawReticle(Graphics g, Reticle ReticleAppearance, Point loc, Pen pen)
        {
            pen.Width = 3;
            var top = new Point(loc.X + RETICLE_SIZE/2, loc.Y);
            var bottom = new Point(loc.X + RETICLE_SIZE / 2, loc.Y + RETICLE_SIZE);
            var left = new Point(loc.X, loc.Y + RETICLE_SIZE / 2);
            var right = new Point(loc.X + RETICLE_SIZE, loc.Y + RETICLE_SIZE/ 2);

            if (ReticleAppearance == Reticle.Cross)
            {
                g.DrawLine(pen, top, bottom);
                g.DrawLine(pen, left, right);
            }
            else if (ReticleAppearance == Reticle.VerticalLine)
            {
                g.DrawLine(pen, top, bottom);
            }
        }


    }
}
