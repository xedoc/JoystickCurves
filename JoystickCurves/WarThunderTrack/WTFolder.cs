using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace JoystickCurves
{
    public class WTFolders
    {
        private Timer pollWTProcess;
        private const String debugFolder = "_debuginfo";
        private const String processName = "aces";
        private const int POLL_PERIOD = 30 * 1000;
        public event EventHandler<EventArgs> OnFolderChange;
        public event EventHandler<EventArgsString> OnError;

        public WTFolders()
        {
        }

        public void StartPoll()
        {
            if (pollWTProcess == null)
                pollWTProcess = new Timer(new TimerCallback(poolWTProcessCallback), null, 0, POLL_PERIOD);
            else
                pollWTProcess.Change(0, POLL_PERIOD);
        }
        public void StopPoll()
        {
            if( pollWTProcess != null )
                pollWTProcess.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void poolWTProcessCallback(object o)
        {
            try
            {
                var processes = Process.GetProcessesByName(processName);
                foreach (var proc in processes)
                {
                    var folder = Path.GetDirectoryName( ExecutablePath.GetExecutablePath(proc));
                    if (isWTFolder(folder))
                    {
                        if (BaseFolder != folder)
                        {
                            BaseFolder = folder;
                            if (OnFolderChange != null)
                                OnFolderChange(this, EventArgs.Empty);
                        }
                        break;
                    }
                }
            }
            catch (Exception e) {
                Debug.Print("WTFolder::poolWTProcessCallback exception");
                if (OnError != null)
                    OnError(this, new EventArgsString() { Name = "Get process folder", Value = e.Message });
            }
        }

        public String BaseFolder
        {
            get;
            set;
        }
        private bool isWTFolder( String folder )
        {
            if (Directory.Exists(String.Format(@"{0}\{1}",folder,debugFolder)))
                return true;
            else
                return false;
        }

    }
}
