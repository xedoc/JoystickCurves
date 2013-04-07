using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using Microsoft.DirectX.DirectInput;
using Microsoft.Win32;

namespace JoystickCurves
{
    public static class Utils
    {
        private const string ANY = "Any";
        private const string NOTSET = "Not set";

        public delegate void SetPropCallback(Control ctrl, string propName, object value);
        public static void SetProperty<TControl, TValue>(this TControl ctrl, string propName, TValue value) where TControl : Control
        {

            if (ctrl.InvokeRequired)
            {
                var d = new SetPropCallback(SetProperty);
                ctrl.Invoke(d, new object[] { ctrl, propName, value });
            }
            else
            {
                Type t = ctrl.GetType();
                t.InvokeMember(propName, BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.Public, null, ctrl, new object[] { value });
            }

        }
        public delegate object GetPropCallback(Control ctrl, string propName);
        public static object GetProperty<TControl>(this TControl ctrl, string propName) where TControl : Control
        {

            if (ctrl.InvokeRequired)
            {
                var d = new GetPropCallback(GetProperty);
                ctrl.Invoke(d, new object[] { ctrl, propName });
                return null;
            }
            else
            {
                Type t = ctrl.GetType();
                return t.InvokeMember(propName, BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.Public, null, ctrl, null);
            }

        }

        public static float PTop(float bottomleft, float topright, float bottomright)
        {
            return bottomleft * topright / bottomright;
        }
        public static float PBottom(float topleft, float topright, float bottomright)
        {
            return topleft * bottomright / topright;
        }

        delegate void SetComboDataSourceCB(ComboBox cbox, BindingSource source, string displayMember, string valueMember);
        public static void SetComboDataSource(ComboBox cbox, BindingSource source = null, string displayMember = "", string valueMember = "")
        {

            if (cbox.Parent.InvokeRequired)
            {
                SetComboDataSourceCB dlgt = new SetComboDataSourceCB(SetComboDataSource);
                cbox.Parent.Invoke(dlgt, new object[] { cbox, source, displayMember, valueMember });
            }
            else
            {
                cbox.DataSource = null;
                if (source != null)
                {
                    cbox.DataSource = source.DataSource;

                    if (!String.IsNullOrEmpty(displayMember))
                        cbox.DisplayMember = displayMember;
                    
                    if (!String.IsNullOrEmpty(valueMember))
                        cbox.ValueMember = valueMember;
                }

            }
        }


        public static List<ToolStripMenuItem> SetAxisContextMenuItems(String itemName, JoystickOffset currentOffset, EventHandler<EventArgs>clickHandler, MouseEventHandler mouseDownHandler)
        {
            var items = DIUtils.AxisNames.Select(ax => new ToolStripMenuItem(ax) { CheckOnClick = true }).ToList();
            items.Insert(0, new ToolStripMenuItem(NOTSET) { CheckOnClick = true });

            foreach (var d in items )
            {
                d.Click += new EventHandler(clickHandler);
                d.MouseDown += new MouseEventHandler(mouseDownHandler);
                d.Name = itemName;
                if (currentOffset == DIUtils.ID(d.Text) && DIUtils.AxisNames.Contains(d.Text))
                    d.Checked = true;
                else
                    d.Checked = false;
            }
            return items;
        }


        public static List<ToolStripMenuItem> SetDeviceContextMenuItems(List<ToolStripMenuItem> items, String itemName, String currentDevice, EventHandler<EventArgs> clickHandler, MouseEventHandler mouseDownHandler)
        {
            if (string.IsNullOrEmpty(currentDevice))
            {
                currentDevice = items.Count > 0 ? items[0].Text : ANY;
            }

            items.Insert(0, new ToolStripMenuItem(NOTSET) { CheckOnClick = true });
            items.Insert(1, new ToolStripMenuItem(ANY) { CheckOnClick = true });

            foreach (var d in items)
            {
                d.Click += new EventHandler(clickHandler);
                d.MouseDown += new MouseEventHandler(mouseDownHandler);
                d.Name = itemName;
                if (currentDevice == d.Text)
                    d.Checked = true;
                else
                    d.Checked = false;
            }
            return items;
        }

        public static void AddToStartup()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            regKey.SetValue(Application.ProductName, Application.ExecutablePath.ToString());
            if (regKey.GetValue(Application.ProductName) == null)
            {
                MessageBox.Show("Registry write error!");
            }
        }
        public static void RemoveFromStartup()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            regKey.DeleteValue(Application.ProductName, false);
            if (regKey.GetValue(Application.ProductName) != null)
            {
                MessageBox.Show("Registry write error!");
            }
        }

    }
    public class XmlSerializableBase<T> where T : XmlSerializableBase<T>
    {
        static XmlSerializer serializer = new XmlSerializer(typeof(T));
        public static T Deserialize(XmlReader from) { return (T)serializer.Deserialize(from); }
        public void SerializeTo(Stream s) { serializer.Serialize(s, this); }
        public void SerializeTo(TextWriter w) { serializer.Serialize(w, this); }
        public void SerializeTo(XmlWriter xw) { serializer.Serialize(xw, this); }
        public void SerializeTo(StringWriter sw) { serializer.Serialize(sw, this); }
    }



}
