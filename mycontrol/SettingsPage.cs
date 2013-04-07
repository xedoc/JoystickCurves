using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace mycontrol
{
    [DesignTimeVisible(false)]
    public partial class SettingsPage : Panel
    {
        #region Private properties
        
        #endregion
        public SettingsPage()
        {
            InitializeComponent();
        }
        public TreeNode ParentNode
        {
            get;
            set;
        }
        public bool isActive
        {
            get;
            set;
        }
        public override string  Text
        {
	        get 
	        {

		         return base.Text;
	        }
	        set 
	        {
                ParentNode.Text = base.Text;
		        base.Text = value;
	        }
        }
    }
}
