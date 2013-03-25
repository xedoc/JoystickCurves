using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JoystickCurves
{
    public partial class MainForm : Form
    {
        private VirtualJoystick vjoy;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (vjoy == null)
                return;

            if (!vjoy.Enabled)
                return;

            vjoy.Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                vjoy = new VirtualJoystick((uint)numericUpDown1.Value);
                vjoy.Buttons[0].Set(true);
                vjoy.ContinuousPovs[0].Set(3000);
                vjoy.Axis.X.Set(1500);
                vjoy.Axis.Y.Set(1);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            MessageBox.Show("Values are set check control panel");
        }       
    }
}
