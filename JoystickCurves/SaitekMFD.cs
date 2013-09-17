using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace JoystickCurves
{
    public unsafe class SaitekMFD
    {
        #region Saitek X52 Pro MFD APi
        [DllImport("kernel32.dll")]
        public unsafe static extern bool Beep(int freq, int duration);
        //v6
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

        //v7
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_RegisterDeviceCallback")]
        public static extern int DirectOutput_RegisterDeviceCallback(DeviceCallbackDelegate pfnCb, int pCtxt);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_RegisterPageCallback")]
        unsafe public static extern int DirectOutput_RegisterPageCallback(void* hDevice, PageCallbackDelegate pfnCb, int pCtxt);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_RegisterSoftButtonCallback")]
        unsafe public static extern int DirectOutput_RegisterSoftButtonCallback(void* hDevice, ButtonCallbackDelegate pfnCb, int pCtxt);        
        unsafe public delegate int EnumerateCallbackDelegate(void* hDevice, int lContext);
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_Enumerate")]
        public static extern int DirectOutput_Enumerate(EnumerateCallbackDelegate pfnCb, int pCtx);        
        [DllImport("DIRECTOUTPUT.DLL", EntryPoint = "DirectOutput_AddPage")]
        unsafe public static extern int DirectOutput_AddPage(void* hDevice, int dwPage, [MarshalAs(UnmanagedType.LPWStr)] string wszValue);
        #endregion

        PageCallbackDelegate pageCallback;
        ButtonCallbackDelegate buttonCallback;
        private const string PROGRAMNAME = "JoystickCurves";
        void* m_hDevice = null;
        private const string REGPATH = @"SOFTWARE\Saitek\DirectOutput";

        public event EventHandler<EventArgs> OnInit;
        public event EventHandler<EventArgs> OnError;
        public event EventHandler<SaitekEventArgs> OnPageChange;
        public event EventHandler<SaitekEventArgs> OnButton;

        public SaitekMFD()
        {
            Unacquiring = false;

            AddSaitekDllPath();
            

            if (OnInit != null)
                OnInit(this, EventArgs.Empty);
        }
        public int ApiVersion
        {
            get;
            set;
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
                Debug.Print("PageCallbackDelegate created");
                buttonCallback = new ButtonCallbackDelegate(ButtonCallback);
                Debug.Print("ButtonCallbackDelegate created");

                var result = 0;
                result = DirectOutput_Initialize(PROGRAMNAME);
                Debug.Print( "DirectOutput_Initialize result = {0}", result );
                if( ApiVersion <= 6 )
                {
                        result = DirectOutput_RegisterDeviceChangeCallback(new DeviceCallbackDelegate(DeviceCallback), 0);
                        Debug.Print("DirectOutput_RegisterDeviceChangeCallback result = {0}", result);
                        result = DirectOutput_Enumerate();
                        Debug.Print( "DirectOutput_Enumerate result = {0}", result );
                        result = DirectOutput_RegisterPageChangeCallback(m_hDevice, pageCallback, 0);
                        Debug.Print( "DirectOutput_RegisterPageChangeCallback result = {0}", result );
                        result = DirectOutput_RegisterSoftButtonChangeCallback(m_hDevice, buttonCallback, 0);
                        Debug.Print( "DirectOutput_RegisterSoftButtonChangeCallback result = {0}", result );
                }
                else
                {
                        result = DirectOutput_RegisterDeviceCallback(new DeviceCallbackDelegate(DeviceCallback), 0);
                        Debug.Print( "v7 DirectOutput_RegisterDeviceCallback result = {0}", result );
                        result = DirectOutput_Enumerate( new EnumerateCallbackDelegate( EnumerateCallback ),0);
                        Debug.Print( "v7 DirectOutput_Enumerate result = {0}", result );
                        result = DirectOutput_RegisterPageCallback(m_hDevice, pageCallback, 0);
                        Debug.Print( "v7 DirectOutput_RegisterPageCallback result = {0}", result );
                        result = DirectOutput_RegisterSoftButtonCallback(m_hDevice, buttonCallback, 0);
                        Debug.Print( "v7 DirectOutput_RegisterSoftButtonCallback result = {0}", result );
                }
                Acquired = true;
                Acquiring = false;
            }
            catch {
                Debug.Print("Saitek X52 pro acquire error!");
                if (OnError != null)
                    OnError(this, EventArgs.Empty);
                
                
                return false;
            }

            return true;
        }

        private bool AddSaitekDllPath()
        {
            Debug.Print("Checking path to the DirectOutput.dll");
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(REGPATH, false);
            if (regKey != null)
            {
                Debug.Print(@"Registry key: {0}\{1}", REGPATH, regKey);
                var pathToDLL = (string)regKey.GetValue("DirectOutput");
                Debug.Print("DirectOutput.dll full path: {0}", pathToDLL);
                if (pathToDLL != null)
                {
                    Debug.Print("Adding DLL path to environment variable");
                    Utils.AddEnvironmentPaths(new[] { Path.GetDirectoryName(pathToDLL) });

                    ApiVersion = Utils.FileVersion(pathToDLL).FileMajorPart;
                    Debug.Print("DirectOutput version major number: {0}",ApiVersion);

                    return true;
                }
            }
            else
            {
                Debug.Print("Registry key {0} not found", REGPATH);
            }
            return false;
        }

        public void UnAcquire()
        {
            if (!Unacquiring && Acquired)
            {
                Unacquiring = true;

                var result = DirectOutput_Deinitialize();
                Debug.Print("DirectOutput_Deinitialize result = {0}", result);
                Acquired = false;
            }
        }
        public void SetText(int pageNumber, int line, string text)
        {
            text = text.PadRight(16, ' ');
            var result = DirectOutput_SetString(m_hDevice, pageNumber, line, (text.Length > 16 ? 16 : text.Length), text);
            Debug.Print( "DirectOutput_SetString result = {0}", result );

        }
        public void AddPage(int number, string pageName)
        {
            if (ApiVersion == 6)
            {
                DirectOutput_AddPage(m_hDevice, number, pageName, false );
            }
            else
            {
                DirectOutput_AddPage(m_hDevice, number, pageName);
            }
        }

        public int DeviceCallback(void* hDevice, bool bAdded, int lContext)
        {            
            if (bAdded == true)
                m_hDevice = hDevice;
            else
                m_hDevice = null;
            return 0;
        }
        public int EnumerateCallback(void* hDevice, int lContext)
        {
            m_hDevice = hDevice;
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
