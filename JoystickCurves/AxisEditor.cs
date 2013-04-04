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
        private object lockRead = new object();
        CurveType _curveType;

        public enum CurveType
        {
            Bezier,
            Cardinal
        }
        public AxisEditor()
        {
            InitializeComponent();
            Type = CurveType.Bezier;
            CurrentCurve = curveResponse.Points;
            _selectedDestAxis = NOTSET;
            _selectedDestDevice = NOTSET;
            _selectedSourceAxis = NOTSET;
            _selectedSourceDevice = NOTSET;
        }
        public CurveType Type
        {
            get { return _curveType; }
            set
            {
                _curveType = value;
            }


        }
        public int Index
        {
            get;
            set;
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
        public void StopUpdates()
        {
            curveResponse.StopUpdates();
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
                Utils.SetProperty<TabPage, String>((TabPage)Parent,"Text", _selectedSourceAxis);
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
            set { curveResponse.Points = value;
            if (OnChange != null)
                OnChange(this, EventArgs.Empty);
            }
        }
        public void ResetCurve()
        {
            curveResponse.ResetCurve();
        }
        public String CurrentSourceAxis
        {
            get { return _selectedSourceAxis; }
            set { _selectedSourceAxis = String.IsNullOrEmpty(value) ? NOTSET : value; }
        }
        public String CurrentDestAxis
        {
            get { return _selectedDestAxis; }
            set { _selectedDestAxis = String.IsNullOrEmpty(value) ? NOTSET : value; }
        }
        public String CurrentSourceDevice
        {
            get { return _selectedSourceDevice; }
            set { _selectedSourceDevice = String.IsNullOrEmpty(value) ? NOTSET : value; }
        }
        public String CurrentDestDevice
        {
            get { return _selectedDestDevice; }
            set { _selectedDestDevice = String.IsNullOrEmpty(value) ? NOTSET : value; }
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
                TabTitle = axisEditor.CurrentSourceAxis
            };

            return profileTab;
        }
        private void Change(object sender, EventArgs e)
        {

            CurrentDestAxis = (string)Utils.GetProperty<ComboBox>(comboDestAxis, "SelectedItem");
            CurrentDestDevice = (string)Utils.GetProperty<ComboBox>(comboDestDevice, "SelectedItem");
            CurrentSourceAxis = (string)Utils.GetProperty<ComboBox>(comboSourceAxis, "SelectedItem");
            CurrentSourceDevice = (string)Utils.GetProperty<ComboBox>(comboSourceDevice, "SelectedItem");

            Title = CurrentSourceAxis;

            if (OnChange != null)
                OnChange(this, EventArgs.Empty);
        }

        private void curveResponse_OnCurveChange(object sender, EventArgs e)
        {
            if (OnChange != null)
                OnChange(this, EventArgs.Empty);
        }
    }
}
