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

        private List<String> _sourceAxis, _destAxis, _sourceDevices, _destDevices;
        private BindingSource _destContrBSource, _sourceContrBSource, _destAxisBSource, _sourceAxisBSource;
        private string _selectedDestDevice, _selectedDestAxis, _selectedSourceDevice, _selectedSourceAxis;
        public event EventHandler<EventArgs> OnChange;

        public AxisEditor()
        {
            InitializeComponent();
            CurrentCurve = curveResponse.Points;
            _selectedDestAxis = NOTSET;
            _selectedDestDevice = NOTSET;
            _selectedSourceAxis = NOTSET;
            _selectedSourceDevice = NOTSET;
        }
        public List<String> SourceControllers
        {
            get { return _sourceDevices; }
            set
            {
                if (value == null)
                    return;
                value.Insert(0, new GameController(NOTSET));
                if (_sourceContrBSource == null)
                {
                    _sourceContrBSource = new BindingSource();
                    _sourceContrBSource.DataSource = value;
                }
                _sourceDevices = value;
                Utils.SetComboDataSource(comboSourceDevice, _sourceContrBSource);

                if( String.IsNullOrEmpty(_selectedSourceDevice))
                    return;

                
                Utils.SetProperty<ComboBox,String>(comboSourceDevice, "SelectedItem", _selectedSourceDevice);
            }
        }
        public List<String> DestinationControllers
        {
            get { return _destDevices; }
            set
            {
                if (value == null)
                    return;
                value.Insert(0, new GameController(NOTSET));
                if (_destContrBSource == null)
                {
                    _destContrBSource = new BindingSource();
                    _destContrBSource.DataSource = value;
                }
                _destDevices = value;
                Utils.SetComboDataSource(comboDestDevice, _destContrBSource);

                Utils.SetProperty<ComboBox, String>(comboDestDevice, "SelectedItem", _selectedDestDevice);
            }
        }

        public List<String> SourceAxis
        {
            get { return _sourceAxis; }
            set
            {
                if (value == null)
                    return;

                value.Insert(0, NOTSET);
                if (_sourceAxisBSource == null)
                {
                    _sourceAxisBSource = new BindingSource();
                    _sourceAxisBSource.DataSource = value;
                }
                _sourceAxis = value;
                Utils.SetComboDataSource(comboSourceAxis, _sourceAxisBSource);

                Utils.SetProperty<ComboBox, String>(comboSourceAxis, "SelectedItem", _selectedSourceAxis);
            }

        }
        public List<String> DestinationAxis
        {
            get { return _destAxis; }
            set
            {
                if (value == null)
                    return;

                value.Insert(0, NOTSET);
                if (_destAxisBSource == null)
                {
                    _destAxisBSource = new BindingSource();
                    _destAxisBSource.DataSource = value;
                }
                _destAxis = value;
                Utils.SetComboDataSource(comboDestAxis, _destAxisBSource);

                Utils.SetProperty<ComboBox, String>(comboDestAxis, "SelectedItem", _selectedDestAxis);
            }
        }
        public BezierCurvePoints CurrentCurve
        {
            get { return curveResponse.Points; }
            set { curveResponse.Points = value; }
        }
        public String CurrentSourceAxis
        {
            get { return _selectedSourceAxis; }
            set { _selectedSourceAxis = value; }
        }
        public String CurrentDestAxis
        {
            get { return _selectedDestAxis; }
            set { _selectedDestAxis = value; }
        }
        public String CurrentSourceDevice
        {
            get { return _selectedSourceDevice; }
            set { _selectedSourceDevice = value; }
        }
        public String CurrentDestDevice
        {
            get { return _selectedDestDevice; }
            set { _selectedDestDevice = value; }
        }
        public String Title
        {
            get;
            set;
        }
        public static implicit operator ProfileTab(AxisEditor axisEditor)
        {            
            axisEditor.CurrentCurve.ScaleRawPoints();
            var profileTab = new ProfileTab()
            {
                CurvePoints = axisEditor.CurrentCurve,
                DestinationAxis = axisEditor.CurrentDestAxis,
                DestinationDevice = axisEditor.CurrentDestDevice,
                SourceAxis = axisEditor.CurrentSourceAxis,
                SourceDevice = axisEditor.CurrentSourceDevice,
                TabTitle = axisEditor.Title
            };

            return profileTab;
        }
        private void Change(object sender, EventArgs e)
        {
            CurrentDestAxis = comboDestAxis.SelectedItem as string;
            CurrentDestDevice = comboDestDevice.SelectedItem as string;
            CurrentSourceAxis = comboSourceAxis.SelectedItem as string;
            CurrentSourceDevice = comboSourceDevice.SelectedItem as string;
            Title = CurrentSourceAxis;

            if (OnChange != null)
                OnChange(this, EventArgs.Empty);
        }
    }
}
