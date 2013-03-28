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
            PhysicalAxisPitch = new Axis(-1, 1);
            PhysicalAxisRoll = new Axis(-1, 1);
            PhysicalAxisRudder = new Axis(-1, 1);
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
            get;
            set;
        }
        public Point PhysicalHandleLocation
        {
            get;
            set;
        }
        public Point VirtualRudderLocation
        {
            get;
            set;
        }
        public Point VirtualHandleLocation
        {
            get;
            set;
        }
        public Axis PhysicalAxisRoll
        {
            set {

                int newX = HandleBounds.X + (int)Utils.PTop(HandleBounds.Width, value.Value - value.Min, value.Max - value.Min);
                if (newX != PhysicalHandleLocation.X)
                {
                    PhysicalHandleLocation = new Point(newX, PhysicalHandleLocation.Y);
                    Invalidate();
                }
            }
        }
        public Axis PhysicalAxisPitch
        {
            set
            {
                int newY = HandleBounds.Y + (int)Utils.PTop(HandleBounds.Height, value.Value - value.Min, value.Max - value.Min);
                if (newY != PhysicalHandleLocation.Y)
                {
                    PhysicalHandleLocation = new Point(PhysicalHandleLocation.X, newY);
                    Invalidate();
                }
            }
        }

        public Axis PhysicalAxisRudder
        {
            set {
                int newX = RudderBounds.X + (int)Utils.PTop(RudderBounds.Width, value.Value - value.Min, value.Max - value.Min);
                if (newX != PhysicalRudderLocation.X)
                {
                    PhysicalRudderLocation = new Point(newX, RudderBounds.Y);
                    Invalidate();
                }
            }
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

            HandleBounds = new Rectangle(topLeft.X - RETICLE_SIZE/2, topLeft.Y -RETICLE_SIZE/2, smallestSide,smallestSide);
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

            RudderBounds = new Rectangle(pointLeftRudder.X - RETICLE_SIZE/2, pointLeftRudder.Y - RETICLE_SIZE/2, rudderWidth, RETICLE_SIZE);

            DrawReticle(e.Graphics, Reticle.Cross, PhysicalHandleLocation);
            DrawReticle(e.Graphics, Reticle.VerticalLine, PhysicalRudderLocation);
        }
        public void DrawReticle(Graphics g, Reticle ReticleAppearance, Point loc)
        {
            var pen = new Pen(Color.FromArgb(0, 255, 0));
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
