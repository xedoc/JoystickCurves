using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
namespace JoystickCurves
{
    public partial class TrimmerTrackBar : UserControl
    {
        private double percent;
        public event EventHandler<EventArgs> OnChange;
        public TrimmerTrackBar()
        {
            InitializeComponent();
            Percent = Math.Round(100.0f * (double)Value / (double)Maximum, 2);
            var text = Percent.ToString("0.00", CultureInfo.InvariantCulture) + "%";
            labelPercent.Text = Percent > 0 ? "+" + text : text;
        }
        public int Minimum
        {
            get {return trackBar1.Minimum;}
            set { trackBar1.Minimum = value; }
        }
        public int Maximum
        {
            get{ return trackBar1.Maximum;}
            set { trackBar1.Maximum = value; }
        }
        public int Value
        {
            get{ return trackBar1.Value;}
            set { trackBar1.Value = value; 
            
            }
        }

        public double Percent
        {
            get { return percent; }
            set { 
                    percent = value; Value = (int)((double)Maximum * (double)percent / 100.0f);
                    if (OnChange != null)
                        OnChange(this, EventArgs.Empty);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Percent = Math.Round(100.0f * (double)Value / (double)Maximum, 2);
            var text = Percent.ToString("0.00", CultureInfo.InvariantCulture) + "%";
            labelPercent.Text = Percent > 0 ? "+" + text : text;
            
        }

    }
}
