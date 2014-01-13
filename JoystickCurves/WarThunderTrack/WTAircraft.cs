using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using dotUtilities;
using System.Globalization;

namespace JoystickCurves
{
    public class WTAircraft : CurrentAircraft
    {
        private const int POLL_LASTLOG = 30 * 1000;
        private const String debugSubFolder = "_debuginfo";
        private const String logExtension = ".clog";
        private const String trackString = "^.*?try to select aircraft (.*)$";
        private BGWorker bwReader;
        private WTFolders wtFolders;
        private object lockRead = new object();
        private object lockLastFile = new object();

        private readonly byte[] xorBytes = {
	        0x82,0x87,0x97,0x40,0x8D,0x8B,0x46,0x0B,0xBB,0x73,0x94,0x03,0xE5,0xB3,0x83,0x53, 
	        0x69,0x6B,0x83,0xDA,0x95,0xAF,0x4A,0x23,0x87,0xE5,0x97,0xAC,0x24,0x58,0xAF,0x36, 
	        0x4E,0xE1,0x5A,0xF9,0xF1,0x01,0x4B,0xB1,0xAD,0xB6,0x4C,0x4C,0xFA,0x74,0x28,0x69, 
	        0xC2,0x8B,0x11,0x17,0xD5,0xB6,0x47,0xCE,0xB3,0xB7,0xCD,0x55,0xFE,0xF9,0xC1,0x24, 
	        0xFF,0xAE,0x90,0x2E,0x49,0x6C,0x4E,0x09,0x92,0x81,0x4E,0x67,0xBC,0x6B,0x9C,0xDE, 
	        0xB1,0x0F,0x68,0xBA,0x8B,0x80,0x44,0x05,0x87,0x5E,0xF3,0x4E,0xFE,0x09,0x97,0x32, 
	        0xC0,0xAD,0x9F,0xE9,0xBB,0xFD,0x4D,0x06,0x91,0x50,0x89,0x6E,0xE0,0xE8,0xEE,0x99, 
	        0x53,0x00,0x3C,0xA6,0xB8,0x22,0x41,0x32,0xB1,0xBD,0xF5,0x28,0x50,0xE0,0x72,0xAE};

        private Timer timerLatestLog;

        public event EventHandler<EventArgsString> OnNewLine;

        public WTAircraft()
        {
            GameName = "War Thunder";
            wtFolders = new WTFolders();
            wtFolders.OnFolderChange += new EventHandler<EventArgs>(wtFolders_OnFolderChange);
            wtFolders.OnError += new EventHandler<EventArgsString>(wtFolders_OnError);

        }

        void wtFolders_OnError(object sender, EventArgsString e)
        {
            RaiseError(new EventArgsString() { Name = e.Name, Value = e.Value });
        }

        void wtFolders_OnFolderChange(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(wtFolders.BaseFolder))
            {
                Init(wtFolders.BaseFolder);
            }
        }
        public override void StartPoll()
        {
            wtFolders.StartPoll();
        }

        public override void StopPoll()
        {
            try
            {
                if( timerLatestLog != null )
                    timerLatestLog.Change(Timeout.Infinite, Timeout.Infinite);

                if (wtFolders != null)
                    wtFolders.StopPoll();
                if( bwReader != null )
                    bwReader.Stop();
            }
            catch {
                Debug.Print("WTAircraft::StopPoll exception");
            }


        }

        private void Init(String folder)
        {
            if (String.IsNullOrEmpty(folder))
                return;

            DebugFolder = String.Format(@"{0}\{1}", Path.GetFullPath(folder), debugSubFolder);

            if (!Directory.Exists(DebugFolder))
                return;
            
            if (timerLatestLog == null)
                timerLatestLog = new Timer(new TimerCallback(timerLatestLogCallback), null, 0, POLL_LASTLOG);
            else
                timerLatestLog.Change(0, POLL_LASTLOG);

        }
        private void timerLatestLogCallback( object o )
        {
            if( !String.IsNullOrEmpty( DebugFolder ))
            {
                lock (lockLastFile)
                {
                    var files = new DirectoryInfo(DebugFolder);
                    var newestLog = files.GetFiles().OrderByDescending(f => f.LastWriteTime).FirstOrDefault(
                        f => f.Extension.ToLower() == logExtension);

                    Debug.Print("WTAircraft: Current file {0}", newestLog);

                    if (String.IsNullOrEmpty(newestLog.FullName))
                        return;

                    if (newestLog.FullName != CurrentFile || 
                        (bwReader != null && bwReader.IsComplete)
                        )
                    {
                        CurrentFile = newestLog.FullName;
                        if (bwReader != null)
                        {
                            try
                            {
                                Debug.Print("WTAircraft: stopping ReadLines() background job");
                                bwReader.Stop();
                                AircraftName = String.Empty;
                            }
                            catch
                            {
                                Debug.Print("WTAircraft::timerLatestLogCallback exception");
                            }
                        }
                        Debug.Print("WTAircraft: ReadLines() started in background");
                        bwReader = new BGWorker(ReadLines, null);
                    }

                }
            }
        }
        private String DebugFolder
        {
            get;
            set;
        }
        private String CurrentFile
        {
            get;
            set;
        }
        private void ReadLines()
        {
            if (String.IsNullOrEmpty(CurrentFile))
                return;

            Debug.Print("WTAircraft entering ReadLines()", CurrentFile);
            lock (lockRead)
            {
                Debug.Print("WTAircraft: reset params, start to read new log file");
                int xorIndex = 0;
                String CurString = String.Empty;
                List<Byte> bytes = new List<byte>();

                try
                {
                    using (var fileStream = new FileStream(CurrentFile, FileMode.Open,
                                      FileAccess.Read, FileShare.ReadWrite))
                    {
                        Debug.Print("WTAircraft: file opened {0}", CurrentFile);
                        var airCraft = String.Empty;
                        while (true)
                        {
                            if (bwReader.CancelationPending)
                                break;

                            if (xorIndex >= xorBytes.Count()) xorIndex = 0;
                            var xoredByte = fileStream.ReadByte();
                            if (xoredByte >= 0)
                            {
                                var nextChar = (byte)(xoredByte ^ xorBytes[xorIndex++]);
                                if (nextChar == 0x0A || nextChar == 0x0D)
                                {
                                    if (bytes.Count > 0)
                                    {
                                        CurString = Encoding.UTF8.GetString(bytes.ToArray());
                                        if (OnNewLine != null)
                                            OnNewLine(this, new EventArgsString() { Value = CurString });

                                        var t = Re.GetSubString(CurString, trackString, 1);
                                        if (!string.IsNullOrEmpty(t))
                                            airCraft = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(t.Replace("_", " "));

                                        bytes.Clear();
                                        CurString = String.Empty;
                                    }
                                }
                                else
                                {
                                    bytes.Add(nextChar);
                                }
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(airCraft) && airCraft != AircraftName)
                                {
                                    AircraftName = airCraft;
                                }
                                Thread.Sleep(50);
                            }
                        }
                    }
                }
                catch( Exception e )
                {
                    Debug.Print("WTAircraft::ReadLines {0}", e.Message);
                }
            }
        }


    }
}
