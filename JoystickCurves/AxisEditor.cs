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
    public enum CurveResponseType
    {
        Multiplier,
        Values
    }
    public enum CurveShapeType
    {
        Bezier,
        Cardinal
    }
    public partial class AxisEditor : UserControl
    {
        private const string NOTSET = "Not set";
        private const string GC_DISPMEMBER = "Name";
        private const string GC_VALMEMBER = "Name";
        private const string AXIS_DISPMEMBER = "Name";
        private const string AXIS_VALMEMBER = "Name";
        private const string DEFITEM = "Looking for joysticks...";
        private List<String> _sourceAxis, _destAxis, _sourceDevices, _destDevices;
        private BindingSource _destContrBSource, _sourceContrBSource, _destAxisBSource, _sourceAxisBSource;
        private string _selectedDestDevice, _selectedDestAxis, _selectedSourceDevice, _selectedSourceAxis;
        public event EventHandler<EventArgs> OnChange;
        private object lockRead = new object();
        private CurveResponseType _curveResponseType;
        CurveShapeType _curveType;



        public AxisEditor()
        {
            InitializeComponent();
            
            comboDestAxis.Items.Add(DEFITEM);
            comboDestDevice.Items.Add(DEFITEM);
            comboSourceAxis.Items.Add(DEFITEM);
            comboSourceDevice.Items.Add(DEFITEM);

            comboDestDevice.SelectedIndex = 0;
            comboDestAxis.SelectedIndex = 0;
            comboDestDevice.SelectedIndex = 0;
            comboSourceAxis.SelectedIndex = 0;
            comboSourceDevice.SelectedIndex = 0;

            ShapeType = CurveShapeType.Bezier;
            CurrentCurve = curveResponse.Points;
            _selectedDestAxis = NOTSET;
            _selectedDestDevice = NOTSET;
            _selectedSourceAxis = NOTSET;
            _selectedSourceDevice = NOTSET;

            trimmerTrackBar.OnChange += new EventHandler<EventArgs>(trimmerTrackBar_OnChange);
        }

        void trimmerTrackBar_OnChange(object sender, EventArgs e)
        {
            if (OnChange != null)
                OnChange(this, EventArgs.Empty);
        }
        public CurveResponseType CurveResponseType
        {
            get { return _curveResponseType; }
            set
            {
                switch (value)
                {
                    case CurveResponseType.Multiplier:
                        {
                            if( CurrentCurve.RawPoints.Count <= 0 )
                                return;
                            _curveResponseType = JoystickCurves.CurveResponseType.Multiplier;
                            groupBoxCurve.Text = "Response Curve (Multiplier)";
                        }
                        break;
                    case CurveResponseType.Values:
                        {
                            if( CurrentCurve.RawPoints.Count <= 0 )
                                return;
                            _curveResponseType = JoystickCurves.CurveResponseType.Values;
                            groupBoxCurve.Text = "Response Curve (Absolute Value)";
                        }
                        break;

                }

                CurrentCurve.CurveResponseType = _curveResponseType;

                if (OnChange != null)
                    OnChange(this, EventArgs.Empty);
            }
        }

        public CurveShapeType ShapeType
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
                value.Insert(0, new DirectInputJoystick(NOTSET));
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
                value.Insert(0, new DirectInputJoystick(NOTSET));
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
            switch (CurveResponseType)
            {
                case JoystickCurves.CurveResponseType.Multiplier:
                    {
                        CurrentCurve.RawPoints[0] = new PointF(0.0f, 0.0f);
                        CurrentCurve.RawPoints[CurrentCurve.RawPoints.Count - 1] = new PointF(1.0f, 0.0f);
                        CurrentCurve.Streighten();
                    }
                    break;
                case JoystickCurves.CurveResponseType.Values:
                    {
                        CurrentCurve.RawPoints[0] = new PointF(0.0f, 1.0f);
                        CurrentCurve.RawPoints[CurrentCurve.RawPoints.Count - 1] = new PointF(1.0f, 0.0f);
                        CurrentCurve.Streighten();
                    }
                    break;
            }

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

        public bool PreserveAxisRange
        {
            get { return checkBoxPreserveAxisRange.Checked; }
            set { 
                    Utils.SetProperty<CheckBox, bool>(checkBoxPreserveAxisRange, "Checked", value);
                    if (OnChange != null)
                        OnChange(this, EventArgs.Empty);
            }
        }

        public double Correction
        {
            get { return trimmerTrackBar.Percent; }
            set {   trimmerTrackBar.Percent = value;
            }
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
                Correction = axisEditor.Correction,
                PreserveAxisRange = axisEditor.PreserveAxisRange,                
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
