using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JoystickCurves
{
    public partial class AxisEditor : UserControl
    {
        private BindingSource _virtualBSource, _physicalBSource;
        public AxisEditor()
        {
            InitializeComponent();

        }

        public List<GameController> VirtualControllers
        {
            set
            {
                if (_virtualBSource == null)
                {
                    _virtualBSource = new BindingSource();
                    _virtualBSource.DataSource = value;
                }
                SetDataSource(comboDestDevice, null);
                SetDataSource(comboDestDevice, _virtualBSource, "Name", "Name");
            }
        }
        public List<GameController> PhysicalControllers
        {
            set
            {
                if (_physicalBSource == null)
                {
                    _physicalBSource = new BindingSource();
                    _physicalBSource.DataSource = value;
                }
                SetDataSource(comboSourceDevice, null);
                SetDataSource(comboSourceDevice, _physicalBSource, "Name", "Name");
            }
        }

        delegate void SetComboDataSource(ComboBox cbox, BindingSource source, string displayMember, string valueMember);
        public void SetDataSource(ComboBox cbox, BindingSource source, string displayMember = "", string valueMember = "")
        {
            if (this.InvokeRequired)
            {
                SetComboDataSource dlgt = new SetComboDataSource(SetDataSource);
                Invoke(dlgt, new object[] { cbox, source, displayMember, valueMember });
            }
            else
            {
                if (source == null)
                    cbox.DataSource = null;
                else
                    cbox.DataSource = source.DataSource;

                cbox.DisplayMember = displayMember;
                cbox.ValueMember = valueMember;

            }
        }
    }
}
