using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace JoystickCurves
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
            var h = this.Handle;

            Debug.Listeners.Add(new TextWriterTraceListener(new LogStreamWriter(textBox)));
        }

        private void Debug_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }


    public class LogStreamWriter : System.IO.TextWriter
    {
        private TextBox _textbox;
        public LogStreamWriter(TextBox textbox)
        {
            _textbox = textbox;
        }
        public override void WriteLine(string value)
        {
            base.WriteLine(value);
            _textbox.Text = _textbox.Text + value + Environment.NewLine;

            _textbox.Parent.Show();
        }
        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
