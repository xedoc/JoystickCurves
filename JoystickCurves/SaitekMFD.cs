using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;

namespace JoystickCurves
{
    public unsafe class SaitekMFD
    {
        #region Saitek X52 Pro MFD APi
        [DllImport("kernel32.dll")]
        public unsafe static extern bool Beep(int freq, int duration);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_Initialize")]
        public static extern int DirectOutput_Initialize([MarshalAs(UnmanagedType.LPWStr)] string wszAppName);
        unsafe public delegate int DeviceCallbackDelegate(void* hDevice, bool bAdded, int lContext);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_RegisterDeviceChangeCallback")]
        public static extern int DirectOutput_RegisterDeviceChangeCallback(DeviceCallbackDelegate pfnCb, int pCtxt);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_Enumerate")]
        public static extern int DirectOutput_Enumerate();
        unsafe public delegate int PageCallbackDelegate(void* hDevice, int lPage, bool bActivated, int lContext);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_RegisterPageChangeCallback")]
        unsafe public static extern int DirectOutput_RegisterPageChangeCallback(void* hDevice, PageCallbackDelegate pfnCb, int pCtxt);
        unsafe public delegate int ButtonCallbackDelegate(void* hDevice, int lButtons, int lContext);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_RegisterSoftButtonChangeCallback")]
        unsafe public static extern int DirectOutput_RegisterSoftButtonChangeCallback(void* hDevice, ButtonCallbackDelegate pfnCb, int pCtxt);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_AddPage")]
        unsafe public static extern int DirectOutput_AddPage(void* hDevice, int dwPage, [MarshalAs(UnmanagedType.LPWStr)] string wszValue, bool bSetAsActive);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_RemovePage")]
        unsafe public static extern int DirectOutput_RemovePage(void* hDevice, int dwPage);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_SetString")]
        unsafe public static extern int DirectOutput_SetString(void* hDevice, int dwPage, int dwIndex, int cchValue, [MarshalAs(UnmanagedType.LPWStr)] string wszValue);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_SetLed")]
        unsafe public static extern int DirectOutput_SetLed(void* hDevice, int dwPage, int dwIndex, int dwValue);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_Deinitialize")]
        public static extern int DirectOutput_Deinitialize();
        #endregion

        PageCallbackDelegate pageCallback;
        ButtonCallbackDelegate buttonCallback;
        private const string PROGRAMNAME = "JoystickCurves";
        void* m_hDevice = null;
        private const string REGPATH64 = @"SOFTWARE\Saitek\DirectOutput";
        private const string REGPATH32 = @"SOFTWARE\Wow6432Node\Saitek\DirectOutput";

        public event EventHandler<EventArgs> OnInit;
        public event EventHandler<EventArgs> OnError;
        public event EventHandler<SaitekEventArgs> OnPageChange;
        public event EventHandler<SaitekEventArgs> OnButton;

        public SaitekMFD()
        {
            Unacquiring = false;

            if (!AddSaitekDllPath())
                return;

            if (OnInit != null)
                OnInit(this, EventArgs.Empty);
        }
        public bool Unacquiring
        {
            get;
            set;
        }
        public bool Acquiring
        {
            get;
            set;
        }

        public bool Acquired
        {
            get;
            set;
        }

        public bool Acquire()
        {
            Acquiring = true;

            Acquired = false;
            Unacquiring = false;
            try
            {
                pageCallback = new PageCallbackDelegate(PageCallback);
                buttonCallback = new ButtonCallbackDelegate(ButtonCallback);

                DirectOutput_Initialize(PROGRAMNAME);
                DirectOutput_RegisterDeviceChangeCallback(new DeviceCallbackDelegate(DeviceCallback), 0);
                DirectOutput_Enumerate();
                DirectOutput_RegisterPageChangeCallback(m_hDevice, pageCallback, 0);
                DirectOutput_RegisterSoftButtonChangeCallback(m_hDevice, buttonCallback, 0);
                Acquired = true;
                Acquiring = false;
            }
            catch {
                if (OnError != null)
                    OnError(this, EventArgs.Empty);
                
                
                return false;
            }

            return true;
        }

        private bool AddSaitekDllPath()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(REGPATH32, false);
            var pathToDLL = (string)regKey.GetValue("DirectOutput");
            if (pathToDLL != null)
            {
                Utils.AddEnvironmentPaths(new[] { Path.GetDirectoryName(pathToDLL) });
                return true;
            }
            return false;
        }

        public void UnAcquire()
        {
            if (!Unacquiring)
            {
                Unacquiring = true;
                DirectOutput_Deinitialize();
                Acquired = false;
            }
        }
        public void SetText(int pageNumber, int line, string text)
        {
            DirectOutput_SetString(m_hDevice, pageNumber, line, (text.Length > 16 ? 16 : text.Length), text);
        }
        public void AddPage(int number, string pageName)
        {
            DirectOutput_AddPage(m_hDevice, number, pageName, false);
        }

        public int DeviceCallback(void* hDevice, bool bAdded, int lContext)
        {            
            if (bAdded == true)
                m_hDevice = hDevice;
            else
                m_hDevice = null;
            return 0;
        }
        public int PageCallback(void* hDevice, int lPage, bool bActivated, int lContext)
        {
            if (OnPageChange != null)
                OnPageChange(this, new SaitekEventArgs() { Page = lPage, PageActivated = bActivated, Context = lContext });

            return 0;
        }
        public int ButtonCallback(void* hDevice, int lButtons, int lContext)
        {
            if( OnButton != null )
                OnButton(this, new SaitekEventArgs() { Buttons = lButtons, Context = lContext });

            return 0;
        }
    }
    public class SaitekEventArgs : EventArgs
    {
        public int Buttons
        {
            get;
            set;
        }
        public int Page
        {
            get;
            set;
        }
        public bool PageActivated
        {
            get;
            set;
        }
        public int Context
        {
            get;
            set;
        }
    }
}
