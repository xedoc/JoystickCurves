using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JoystickCurves.Components
{
    public partial class MacroEditor : UserControl
    {
        public MacroEditor()
        {
            InitializeComponent();
        }
        public int Index
        {
            get;
            set;
        }
    }
}
