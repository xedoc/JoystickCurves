using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
					
namespace JoystickCurves
{
    public partial class SettingsForm : Form, INotifyPropertyChanged
    {

        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int wMsg, int wParam, ref int lParam);
        private const int LB_SETTABSTOPS = 0x192;

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
            //SetupHotkeyList();
            //checkBoxHotKey.DataBindings.Add(new Binding("Checked", this, "WaitingHotKey", false, DataSourceUpdateMode.OnPropertyChanged, null));
            if( settingsTree1.Nodes.Count > 0) 
                settingsTree1.TreeView.SelectedNode = settingsTree1.Nodes[0];
        }
        private void SetListTabs()
        {
            int[] ListBoxTabs = new int[] { listHotKeys.Width / 3, listHotKeys.Width / 2 };
            int result;
            result = SendMessage(listHotKeys.Handle.ToInt32(), LB_SETTABSTOPS, ListBoxTabs.Length, ref ListBoxTabs[0]);
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


            SetListTabs();

            Utils.SetDataSource<ListBox>( listHotKeys, _hotkeyBindingSrc, "DisplayTitle", "Title" );


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
                if (_settings == null)
                    return;

                _settings.Reset();
                //_settings.hotKeys = new HotKeys();
                //_hotkeyBindingSrc.DataSource = _settings.hotKeys.Keys;
                //Utils.SetDataSource<ListBox>(listHotKeys, _hotkeyBindingSrc, "Title", "Title");
                _settings.Save();
                
                //SetupHotkeyList();
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


        public void UpdateHotKey(HotKey key)
        {

            if( _settings.hotKeys.Keys.ToList().Exists( hk => hk.Title == key.Title ))
            {

                var i = _settings.hotKeys.Keys.IndexOf((HotKey)listHotKeys.SelectedItem);
                if (i >= 0)
                {
                    _settings.hotKeys.Keys[i] = key;
                }
                else
                {
                    _settings.hotKeys.Keys.Add(key);
                }
                _hotkeyBindingSrc = null;
                SetupHotkeyList();
            }
            checkBoxHotKey.Checked = false;
            checkBoxHotKey.Enabled = true;
            listHotKeys.Enabled = true;
            checkBoxHold.Enabled = true;
        }

        private void listHotKeys_Resize(object sender, EventArgs e)
        {
            SetListTabs();
            listHotKeys.Refresh();
        }

        private void SettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void settingsTree1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
