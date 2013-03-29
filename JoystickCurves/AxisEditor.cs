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
        private const string NOTSET = "Not set";
        private const string GC_DISPMEMBER = "Name";
        private const string GC_VALMEMBER = "Name";
        private const string AXIS_DISPMEMBER = "Name";
        private const string AXIS_VALMEMBER = "Name";

        private CurvePoints _points;
        private GameController _destController, _sourceController;
        private Axis _sourceAxis, _destAxis;
        private BindingSource _destContrBSource, _sourceContrBSource, _destAxisBSource, _sourceAxisBSource;
        public AxisEditor()
        {
            InitializeComponent();

        }

        public GameController SelectedSourceController
        {
            get;
            set;
        }
        public GameController SelectedDestinationController
        {
            get;
            set;
        }
        public List<String> SourceControllers
        {
            set
            {
                value.Insert(0, new GameController(NOTSET));
                if (_destContrBSource == null)
                {
                    _destContrBSource = new BindingSource();
                    _destContrBSource.DataSource = value;
                }                
                Utils.SetComboDataSource(comboDestDevice, _destContrBSource);
            }
        }
        public List<String> DestinationControllers
        {
            set
            {
                value.Insert(0, new GameController(NOTSET));
                if (_sourceContrBSource == null)
                {
                    _sourceContrBSource = new BindingSource();
                    _sourceContrBSource.DataSource = value;
                }
                Utils.SetComboDataSource(comboSourceDevice, _sourceContrBSource);
            }
        }

        public List<String> SourceAxis
        {
            set
            {
                if (value == null)
                    return;
                if (_sourceAxisBSource == null)
                {
                    _sourceAxisBSource = new BindingSource();
                    _sourceAxisBSource.DataSource = value;
                }
                Utils.SetComboDataSource(comboSourceAxis, _sourceAxisBSource);
            }

        }
        public List<String> DestinationAxis
        {
            set
            {
                if (value == null)
                    return;
                if (_destAxisBSource == null)
                {
                    _destAxisBSource = new BindingSource();
                    _destAxisBSource.DataSource = value;
                }
                Utils.SetComboDataSource(comboDestAxis, _destAxisBSource);
            }
        }
        public CurvePoints Curve
        {
            get { return _points; }
            set { _points = value; } 
        }


    }
}
