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
    public partial class JoystickTester : Panel
    {
        public JoystickTester()
        {
            InitializeComponent();
            this.HandleCreated += new EventHandler(JoystickTester_HandleCreated);
        }

        void JoystickTester_HandleCreated(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(JoystickTester_Paint);
            Invalidate();
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
            //gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            e.Graphics.DrawLine(gridPen, new Point(Margin.Left, Size.Height - 20), new Point(smallestSide + Margin.Left, Size.Height - 20));


        }

        public JoystickTester(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.HandleCreated += new EventHandler(JoystickTester_HandleCreated);
        }
    }
}
