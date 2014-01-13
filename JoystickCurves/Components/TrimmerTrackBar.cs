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
            Percent = Value;
        }
        public double Value
        {
            get{ return (double)numericTrimValue.Value; }
            set { numericTrimValue.Value = (decimal)value; }
        }

        public double Percent
        {
            get { return percent; }
            set {
                percent = value; Value = percent;
                    if (OnChange != null)
                        OnChange(this, EventArgs.Empty);
            }
        }

        private void numericTrimValue_ValueChanged(object sender, EventArgs e)
        {
            Percent = (double)numericTrimValue.Value;
        }


    }
}
