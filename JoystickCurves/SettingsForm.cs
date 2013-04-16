using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JoystickCurves
{
    public partial class SettingsForm : Form, INotifyPropertyChanged
    {
        public event EventHandler<EventArgs> OnReset;
        public event EventHandler<HotKeyArgs> OnHotKeyRequest;
        private Properties.Settings _settings;
        private BindingSource _hotkeyBindingSrc;
        private String oldLogin, oldPassword, oldToken;
        public SettingsForm(ref Properties.Settings settings)
        {
            InitializeComponent();
            _settings = settings;
            oldLogin = textBox1.Text;
            oldPassword = textBox2.Text;
            oldToken = _settings.steamToken;
            SetupHotkeyList();
            checkBoxHotKey.DataBindings.Add(new Binding("Checked", this, "WaitingHotKey", false, DataSourceUpdateMode.OnPropertyChanged, null));
        }
        private void SetupHotkeyList()
        {
            checkBoxHotKey.Enabled = false;
            if (_settings.hotKeys == null)
            {
                listHotKeys.Items.Clear();
                return;
            }

            if( _settings.hotKeys.Keys.Count <= 0 )
                return;

            if (_hotkeyBindingSrc == null)
            {
                _hotkeyBindingSrc = new BindingSource();        
                _hotkeyBindingSrc.DataSource = _settings.hotKeys.Keys;
            }


            Utils.SetDataSource<ListBox>( listHotKeys, _hotkeyBindingSrc, "Title", "Title" );
            checkBoxHotKey.Enabled = true;
            var first = _settings.hotKeys.Keys.FirstOrDefault();
            var text = "Hot Key";
            var devicetext = "";
            if( first != null && first.Key != null )
            {
                switch( first.Key.Type )
                {
                    case DIDataType.Joystick:
                        {
                            text = first.Key.JoystickOffsetString;
                            devicetext = "Joy:";
                        }
                        break;
                    case DIDataType.Keyboard: 
                        {
                            text = first.Key.KeyboardKeyString;
                            devicetext = "Key:";
                        } 
                        break;
                    case DIDataType.Mouse:
                        {
                            text = first.Key.MouseOffsetString;
                            devicetext = "Mou:";
                        }
                        break;
                }
            }
            checkBoxHotKey.Text = devicetext + text;
        }

        void hotKeys_OnChange(object sender, EventArgs e)
        {
            SetupHotkeyList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (oldLogin != textBox1.Text)
                _settings.steamToken = String.Empty;
            else
                _settings.steamToken = oldToken;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (oldPassword != textBox2.Text)
                _settings.steamToken = String.Empty;
            else
                _settings.steamLogin = oldToken;
        }
        private void InitHotkeys()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will reset all settings including curves, hot-keys etc. Continue ?", "Reset settings", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                _settings.Reset();
                _settings.hotKeys = new HotKeys();
                _hotkeyBindingSrc.DataSource = _settings.hotKeys.Keys;
                Utils.SetDataSource<ListBox>(listHotKeys, _hotkeyBindingSrc, "Title", "Title");
                _settings.Save();
                
                SetupHotkeyList();
                if (OnReset != null)
                    OnReset(this, EventArgs.Empty);

                this.Activate();
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public bool WaitingHotKey
        {
            get;
            set;
        }

        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void checkBoxHotKey_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHotKey.Checked)
            {
                if (OnHotKeyRequest != null)
                    OnHotKeyRequest(this, new HotKeyArgs(new HotKey()));
            }
        }
    }
}
