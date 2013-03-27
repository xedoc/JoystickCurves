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
    public partial class AimReticle : Panel
    {
        public AimReticle()
        {

            InitializeComponent();
            this.HandleCreated += new EventHandler(AimReticle_HandleCreated);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        public Reticle ReticleAppearance
        {
            get;
            set;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }


        public int X
        {
            get { return Location.X; }
            set { Location = new Point(value, Location.Y); }
        }
        public int Y
        {
            get { return Location.Y; }
            set { Location = new Point(Location.X, value); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0, 0)), this.ClientRectangle);

            var pen = new Pen(Color.FromArgb(0, 255, 0));
            var top = new Point(Size.Width / 2, 0);
            var bottom = new Point(Size.Width / 2, Size.Height);
            var left = new Point(0, Size.Height / 2);
            var right = new Point(Size.Width, Size.Height / 2);

            if (ReticleAppearance == Reticle.Cross)
            {
                e.Graphics.DrawLine(pen, top, bottom);
                e.Graphics.DrawLine(pen, left, right);
            }
            else if (ReticleAppearance == Reticle.VerticalLine)
            {
                e.Graphics.DrawLine(pen, top, bottom);
            }
        }

        void AimReticle_HandleCreated(object sender, EventArgs e)
        {
            Parent.Invalidate();            
        }
    }
}
