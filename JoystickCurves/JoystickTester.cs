using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using Microsoft.DirectX.DirectInput;

namespace JoystickCurves
{
    public enum ReticleShape
    {
        Cross,
        VerticalLine
    }
    public partial class JoystickTester : PictureBox
    {
        public const int RETICLE_SIZE = 20;
        private Point _physicalRudderLocation;
        private Point _physicalHandleLocation;
        private Point _virtualRudderLocation;
        private Point _virtualHandleLocation;
        private Bitmap background;
        private Region invalidateRegion;
        private Bitmap virtualCross, virtualLine, physicalCross, physicalLine;
        private System.Threading.Timer frameUpdateTimer;

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
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

            frameUpdateTimer = new System.Threading.Timer(new TimerCallback(frame_Tick), null, 0, 33);
        }

        void frame_Tick(object o)
        {
            Invalidate();
        }
        void JoystickTester_HandleCreated(object sender, EventArgs e)
        {

            Paint += new PaintEventHandler(JoystickTester_Paint);
            var defaultAxis = new JoystickData();
            PhysicalAxisPitch = defaultAxis;
            PhysicalAxisRoll = defaultAxis;
            PhysicalAxisRudder = defaultAxis;

            VirtualAxisPitch = defaultAxis;
            VirtualAxisRoll = defaultAxis;
            VirtualAxisRudder = defaultAxis;

            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
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
                }
            }

        }
        public JoystickData PhysicalAxisRoll
        {
            set {
                PhysicalHandleLocation = new Point(
                    HandleBounds.X + CoordByAxisValue(value, PhysicalHandleLocation.X, HandleBounds.Width), 
                    PhysicalHandleLocation.Y);
            }
        }
        public JoystickData PhysicalAxisPitch
        {
            set
            {
                PhysicalHandleLocation = new Point(
                    PhysicalHandleLocation.X, 
                    HandleBounds.Y + CoordByAxisValue(value, PhysicalHandleLocation.Y, HandleBounds.Height)
                    );
            }
        }

        public JoystickData PhysicalAxisRudder
        {
            set {
                PhysicalRudderLocation = new Point(
                    RudderBounds.X + CoordByAxisValue(value, PhysicalRudderLocation.X, RudderBounds.Width), 
                    RudderBounds.Y
                    );
            }
        }
        public JoystickData VirtualAxisRoll
        {
            set
            {
                VirtualHandleLocation = new Point(
                    HandleBounds.X + CoordByAxisValue(value, VirtualHandleLocation.X, HandleBounds.Width), 
                    VirtualHandleLocation.Y
                    );
            }
        }
        public JoystickData VirtualAxisPitch
        {
            set
            {
                VirtualHandleLocation = new Point(
                    VirtualHandleLocation.X, 
                    HandleBounds.Y + CoordByAxisValue(value, VirtualHandleLocation.Y, HandleBounds.Height)
                    );
            }
        }

        public JoystickData VirtualAxisRudder
        {
            set
            {
                VirtualRudderLocation = new Point( 
                    RudderBounds.X + CoordByAxisValue(value, VirtualRudderLocation.X, RudderBounds.Width), 
                    RudderBounds.Y
                    );
            }
        }

        private int CoordByAxisValue(JoystickData value, int coord, int bound)
        {
            int newCoord = (int)Utils.PTop(bound, value.Value - value.Min, value.Max - value.Min);
            return newCoord;
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (background == null)
            {
                var dashValues = new Single[] { 1, 1 };
                var dashCircleValues = new Single[] { 5, 5 };
                var smallestSide = Math.Min(Size.Width - Margin.Left - Margin.Right, Size.Height - Margin.Top - Margin.Bottom);
                var drawArea = new Rectangle(Margin.Left, Margin.Top, smallestSide, smallestSide);
                var topLeft = new Point(Margin.Left, Margin.Top);
                var topMiddle = new Point(smallestSide / 2 + Margin.Left, Margin.Top);
                var topRight = new Point(smallestSide + Margin.Left, Margin.Top);
                var leftMiddle = new Point(Margin.Left, smallestSide / 2 + Margin.Top);
                var bottomLeft = new Point(Margin.Left, smallestSide + Margin.Top);
                var bottomMiddle = new Point(smallestSide / 2 + Margin.Left, smallestSide + Margin.Top);
                var bottomRight = new Point(smallestSide + Margin.Left, smallestSide + Margin.Top);
                var rightMiddle = new Point(smallestSide + Margin.Left, smallestSide / 2 + Margin.Top);
                var center = new Point(smallestSide / 2 + Margin.Left, smallestSide / 2 + Margin.Top);
                var green = 80;
                var gridPen = new Pen(Color.FromArgb(0, green, 0));
                var pointLeftRudder = new Point(Margin.Left, Size.Height - 20);
                var pointRightRudder = new Point(smallestSide + Margin.Left, Size.Height - 20);
                var rudderWidth = pointRightRudder.X - pointLeftRudder.X;
                var pen = new Pen(Color.FromArgb(0, 150, 0));

                gridPen.Width = 1;

                gridPen.DashPattern = dashCircleValues;

                background = new Bitmap(Size.Width,Size.Height);
                using (Graphics g = Graphics.FromImage(background))
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(0, 64, 0)), new Rectangle(0,0,Size.Width, Size.Height));

                    g.DrawEllipse(gridPen, drawArea);

                    for (var i = 0; i < 5; i++)
                    {
                        var delta = smallestSide / 10;
                        delta *= i;
                        green += 10;
                        gridPen.Color = Color.FromArgb(0, green, 0);
                        gridPen.DashPattern = dashCircleValues;
                        g.DrawEllipse(gridPen, new Rectangle(drawArea.X + delta, drawArea.Y + delta, drawArea.Width - delta * 2, drawArea.Height - delta * 2));
                    }

                    gridPen = new Pen(Color.FromArgb(0, 120, 0));
                    gridPen.DashPattern = dashValues;

                    //Diagonal
                    g.DrawLine(gridPen, topLeft, new Point(center.X - 10, center.Y - 10));
                    g.DrawLine(gridPen, topRight, new Point(center.X + 10, center.Y - 10));
                    g.DrawLine(gridPen, bottomLeft, new Point(center.X - 10, center.Y + 10));
                    g.DrawLine(gridPen, bottomRight, new Point(center.X + 10, center.Y + 10));

                    g.DrawLine(gridPen, topMiddle, new Point(center.X, center.Y - 10));
                    g.DrawLine(gridPen, bottomMiddle, new Point(center.X, center.Y + 10));
                    g.DrawLine(gridPen, leftMiddle, new Point(center.X - 10, center.Y));
                    g.DrawLine(gridPen, rightMiddle, new Point(center.X + 10, center.Y));

                    //Yaw

                    g.DrawLine(gridPen, pointLeftRudder, pointRightRudder);

                }
            }

            e.Graphics.DrawImage(background, 0,0);

        }
        void JoystickTester_Paint(object sender, PaintEventArgs e)
        {

            var dashValues = new Single[] { 1, 1 };
            var dashCircleValues = new Single[] { 5, 5 };
            var smallestSide = Math.Min(Size.Width - Margin.Left - Margin.Right, Size.Height - Margin.Top - Margin.Bottom);
            var drawArea = new Rectangle(Margin.Left, Margin.Top, smallestSide, smallestSide);
            var topLeft = new Point(Margin.Left, Margin.Top);
            var topMiddle = new Point(smallestSide / 2 + Margin.Left, Margin.Top);
            var topRight = new Point(smallestSide + Margin.Left, Margin.Top);
            var leftMiddle = new Point(Margin.Left, smallestSide / 2 + Margin.Top);
            var bottomLeft = new Point(Margin.Left, smallestSide + Margin.Top);
            var bottomMiddle = new Point(smallestSide / 2 + Margin.Left, smallestSide + Margin.Top);
            var bottomRight = new Point(smallestSide + Margin.Left, smallestSide + Margin.Top);
            var rightMiddle = new Point(smallestSide + Margin.Left, smallestSide / 2 + Margin.Top);
            var center = new Point(smallestSide / 2 + Margin.Left, smallestSide / 2 + Margin.Top);
            var green = 80;
            var gridPen = new Pen(Color.FromArgb(0, green, 0));
            var pointLeftRudder = new Point(Margin.Left, Size.Height - 20);
            var pointRightRudder = new Point(smallestSide + Margin.Left, Size.Height - 20);
            var rudderWidth = pointRightRudder.X - pointLeftRudder.X;
            if (virtualCross == null)
            {

                virtualCross = new Bitmap(RETICLE_SIZE, RETICLE_SIZE);
                virtualLine = new Bitmap(RETICLE_SIZE, RETICLE_SIZE);
                physicalCross = new Bitmap(RETICLE_SIZE, RETICLE_SIZE);
                physicalLine = new Bitmap(RETICLE_SIZE, RETICLE_SIZE);

                var vcG = Graphics.FromImage(virtualCross);
                var vlG = Graphics.FromImage(virtualLine);
                var pcG = Graphics.FromImage(physicalCross);
                var plG = Graphics.FromImage(physicalLine);


                HandleBounds = new Rectangle(topLeft.X - RETICLE_SIZE / 2, topLeft.Y - RETICLE_SIZE / 2, smallestSide, smallestSide);
                RudderBounds = new Rectangle(pointLeftRudder.X - RETICLE_SIZE / 2, pointLeftRudder.Y - RETICLE_SIZE / 2, rudderWidth, RETICLE_SIZE);
                
                var p0 = new Point(0, 0);
                var pen = new Pen(Color.FromArgb(0, 150, 0));
                DrawReticle(plG, ReticleShape.VerticalLine, p0, pen);
                DrawReticle(pcG, ReticleShape.Cross, p0, pen);

                pen = new Pen(Color.FromArgb(0, 255, 0));
    
                DrawReticle(vlG, ReticleShape.VerticalLine, p0, pen);
                DrawReticle(vcG, ReticleShape.Cross, p0, pen);

                vcG.Dispose();
                vlG.Dispose();
                pcG.Dispose();
                plG.Dispose();
                pen.Dispose();
                gridPen.Dispose();

                var centerPoint = new Point(center.X - RETICLE_SIZE / 2, center.Y - RETICLE_SIZE / 2);
                var centerAxis = new JoystickData() {Max = 1000, Min = -1000, Value = 0};
                PhysicalAxisPitch = centerAxis;
                PhysicalAxisRoll = centerAxis;
                PhysicalAxisRudder = centerAxis;
                VirtualAxisPitch = centerAxis;
                VirtualAxisRoll = centerAxis;
                VirtualAxisRudder = centerAxis;

            }
            e.Graphics.DrawImage(physicalCross, PhysicalHandleLocation);
            e.Graphics.DrawImage(physicalLine, PhysicalRudderLocation);
            e.Graphics.DrawImage(virtualCross, VirtualHandleLocation);
            e.Graphics.DrawImage(virtualLine, VirtualRudderLocation);

            //invalidateRegion.Union(new Rectangle(VirtualHandleLocation, new Size(RETICLE_SIZE, RETICLE_SIZE)));
            //invalidateRegion.Union(new Rectangle(VirtualRudderLocation, new Size(RETICLE_SIZE, RETICLE_SIZE)));
            //invalidateRegion.Union(new Rectangle(PhysicalHandleLocation, new Size(RETICLE_SIZE, RETICLE_SIZE)));
            //invalidateRegion.Union(new Rectangle(PhysicalRudderLocation, new Size(RETICLE_SIZE, RETICLE_SIZE)));
        }
        public void DrawReticle(Graphics g, ReticleShape ReticleAppearance, Point loc, Pen pen)
        {
            pen.Width = 3;
            var top = new Point(loc.X + RETICLE_SIZE/2, loc.Y);
            var bottom = new Point(loc.X + RETICLE_SIZE / 2, loc.Y + RETICLE_SIZE);
            var left = new Point(loc.X, loc.Y + RETICLE_SIZE / 2);
            var right = new Point(loc.X + RETICLE_SIZE, loc.Y + RETICLE_SIZE/ 2);

            if (ReticleAppearance == ReticleShape.Cross)
            {
                g.DrawLine(pen, top, bottom);
                g.DrawLine(pen, left, right);
            }
            else if (ReticleAppearance == ReticleShape.VerticalLine)
            {
                g.DrawLine(pen, top, bottom);
            }

            
        }

        public String CurrentVirtualDevice
        {
            get;
            set;
        }
        public String CurrentPhysicalDevice
        {
            get;
            set;
        }
        public JoystickOffset CurrentVirtualX
        {
            get;
            set;
        }
        public JoystickOffset CurrentVirtualY
        {
            get;
            set;
        }
        public JoystickOffset CurrentVirtualRZ
        {
            get;
            set;
        }
        public JoystickOffset CurrentPhysicalX
        {
            get;
            set;
        }
        public JoystickOffset CurrentPhysicalY
        {
            get;
            set;
        }
        public JoystickOffset CurrentPhysicalRZ
        {
            get;
            set;
        }

        public void StopUpdates()
        {
            SuspendLayout();
            Paint -= JoystickTester_Paint;
            frameUpdateTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }


    }
}
