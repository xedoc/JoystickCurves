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
    public partial class ValueInput : UserControl
    {
        ValueType _valueType;
        public ValueInput()
        {
            InitializeComponent();

        }
        public enum ValueType
        {
            Float,
            Int,
            String,
            Bool
        }
        public float FloatMax
        {
            get { return (float)numericValue.Maximum; }
            set{numericValue.Maximum = (decimal)value;}
        }
        public float FloatMin
        {
            get { return (float)numericValue.Minimum; }
            set {numericValue.Minimum = (decimal)value;}
        }
        public int IntMax
        {
            get { return (int)numericValue.Minimum; }
            set {numericValue.Minimum = (int)value;}
        }
        public int IntMin
        {
            get { return (int)numericValue.Minimum; }
            set { numericValue.Minimum = (int)value; }
        }
        public bool BoolValue
        {
            get { return checkBool.Checked; }
            set { checkBool.Checked = value; }
        }
        public int IntValue
        {
            get { return (int)numericValue.Value; }
            set { numericValue.Value = (decimal)value; }
        }
        public float FloatValue
        {
            get { return (float)numericValue.Value; }
            set { numericValue.Value = (decimal)value; }
        }
        public String StringValue
        {
            get { return text.Text; }
            set { text.Text = value; }
        }
        public float FloatStep
        {
            get { return (float)numericValue.Increment; }
            set { numericValue.Increment = (decimal)value; }
        }
        public int IntStep
        {
            get { return (int)numericValue.Increment; }
            set { numericValue.Increment = (int)value; }
        }
        public String Label
        {
            get
            {
                switch (Type)
                {
                    case ValueType.Bool:
                        return checkBool.Text;
                    case ValueType.Float:
                        return labelNumeric.Text;
                    case ValueType.Int:
                        return labelNumeric.Text;
                    case ValueType.String:
                        return labelText.Text;
                    default:
                        return labelNumeric.Text;
                }
            }
            set
            {
                switch (Type)
                {
                    case ValueType.Bool:
                        checkBool.Text = value;
                        break;
                    case ValueType.Float:
                        labelNumeric.Text = value;
                        break;
                    case ValueType.Int:
                        labelNumeric.Text = value;
                        break;
                    case ValueType.String:
                        labelText.Text = value;
                        break;
                    default:
                        labelNumeric.Text = value;
                        break;
                }
            }
        }

        public ValueType Type
        {
            get { return _valueType; }
            set
            {
                panelBool.Visible = false;
                panelNumeric.Visible = false;
                panelString.Visible = false;
               
                _valueType = value;
                switch (_valueType)
                {
                    case ValueType.Float:
                        panelNumeric.Visible = true;
                        numericValue.Increment = (decimal)0.01f;
                        numericValue.DecimalPlaces = 2;
                        break;
                    case ValueType.Int:
                        panelNumeric.Visible = true;
                        numericValue.Increment = 1;
                        numericValue.DecimalPlaces = 0;
                        break;
                    case ValueType.String:
                        panelString.Visible = true;
                        break;
                    case ValueType.Bool:
                        panelBool.Visible = true;
                        break;

                }
             
            }
        }
    }
}
