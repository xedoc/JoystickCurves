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
using System.Diagnostics;

namespace JoystickCurves
{
    public static class Utils
    {
        private const string ANY = "Any";
        private const string NOTSET = "Not set";

        public delegate void CallMethodCallback(Control ctrl, string methodName, object param);
        public static void CallMethod<TControl>(this TControl ctrl, string methodName, object value) where TControl : Control
        {
            try
            {
                if (ctrl.InvokeRequired)
                {
                    var d = new CallMethodCallback(CallMethod);
                    ctrl.Invoke(d, new object[] { ctrl, methodName, value });
                }
                else
                {
                    Type t = ctrl.GetType();
                    t.InvokeMember(methodName, BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public, null, ctrl, new object[] { value });
                }
            }
            catch { }

        }


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


        delegate void SetDataSourceCB<T>(T list, BindingSource source, string displayMember, string valueMember);
        public static void SetDataSource<T>(T list, BindingSource source = null, string displayMember = "", string valueMember = "") where T: ListControl
        {

            if (list.Parent.InvokeRequired)
            {
                SetDataSourceCB<T> dlgt = new SetDataSourceCB<T>(SetDataSource);
                list.Parent.Invoke(dlgt, new object[] { list, source, displayMember, valueMember });
            }
            else
            {
                list.DataSource = null;
                if (source != null)
                {
                    list.DataSource = source.DataSource;

                    if (!String.IsNullOrEmpty(displayMember))
                        list.DisplayMember = displayMember;

                    if (!String.IsNullOrEmpty(valueMember))
                        list.ValueMember = valueMember;
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
                currentDevice = items.Count > 0 ? items[0].Text : NOTSET;
            }
            if (currentDevice != NOTSET && !items.Exists(d => d.Text == currentDevice))
                 currentDevice = items.Where( d => d.Text != NOTSET ).Select( d => d.Text ).FirstOrDefault();

            if (currentDevice == null)
                currentDevice = NOTSET;

            items.Insert(0, new ToolStripMenuItem(NOTSET) { CheckOnClick = true });
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

        public static List<DeviceInstance> DevList(DeviceList devList)
        {
            var result = new List<DeviceInstance>();
            foreach (DeviceInstance dev in devList)
            {
                result.Add(dev);
            }
            return result;
        }

        public static void AddEnvironmentPaths(string[] paths)
        {
            string path = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
            path += ";" + string.Join(";", paths);

            Environment.SetEnvironmentVariable("PATH", path);
        }

        public static FileVersionInfo FileVersion(string path)
        {
            return FileVersionInfo.GetVersionInfo(path);
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

    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _lambdaComparer;
        private readonly Func<T, int> _lambdaHash;

        public LambdaComparer(Func<T, T, bool> lambdaComparer) :
            this(lambdaComparer, o => 0)
        {
        }

        public LambdaComparer(Func<T, T, bool> lambdaComparer, Func<T, int> lambdaHash)
        {
            if (lambdaComparer == null)
                throw new ArgumentNullException("lambdaComparer");
            if (lambdaHash == null)
                throw new ArgumentNullException("lambdaHash");

            _lambdaComparer = lambdaComparer;
            _lambdaHash = lambdaHash;
        }

        public bool Equals(T x, T y)
        {
            return _lambdaComparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _lambdaHash(obj);
        }
    }
}
