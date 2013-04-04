using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Diagnostics;
using System.Threading;

namespace JoystickCurves
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(MainForm.ThreadException);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                Debug.Print("Error: " + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Debug.Print("Error: " + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
        }
    }
}
