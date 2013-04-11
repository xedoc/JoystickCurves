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
    public partial class InputBox : Form
    {

        private static InputBox newInputBox;

        private static string returnString;

        public InputBox()
        {
            InitializeComponent();
        }
        public static string Show(string inputBoxText)
        {
            newInputBox = new InputBox();
            newInputBox.label1.Text = inputBoxText;
            newInputBox.ShowDialog();
            return returnString;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            returnString = textBox1.Text;
            newInputBox.Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            returnString = string.Empty;
            newInputBox.Dispose();
        }
    }
}
