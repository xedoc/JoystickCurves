using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JoystickCurves
{
    public partial class SettingsForm : Form
    {
        public event EventHandler<EventArgs> OnReset;

        private BindingSource _hotkeyBindingSrc;
        private Properties.Settings _settings;
        private String oldLogin, oldPassword, oldToken;
        public SettingsForm()
        {
            InitializeComponent();
            _settings = Properties.Settings.Default;
            oldLogin = textBox1.Text;
            oldPassword = textBox2.Text;
            oldToken = _settings.steamToken;
            SetupHotkeyList();

        }

        private void SetupHotkeyList()
        {
            buttonHotKey.Enabled = false;
            if (_settings.hotKeys == null)
                return;

            if( _settings.hotKeys.Keys.Count <= 0 )
                return;

            if (_hotkeyBindingSrc == null)
            {
                _hotkeyBindingSrc = new BindingSource();
                _hotkeyBindingSrc.DataSource = _settings.hotKeys.Keys.ToList();
            }            
            Utils.SetDataSource<ListBox>( listHotKeys, _hotkeyBindingSrc, "Title", "Title" );
            buttonHotKey.Enabled = true;
            var first = _settings.hotKeys.Keys.FirstOrDefault();
            var text = "Hot Key";
            var devicetext = "";
            if( first != null)
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
            buttonHotKey.Text = devicetext + text;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will reset all settings including curves, hot-keys etc. Continue ?", "Reset settings", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();
                if (OnReset != null)
                    OnReset(this, EventArgs.Empty);
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
