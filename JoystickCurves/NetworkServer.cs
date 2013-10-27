using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperWebSocket;
using System.Threading;
using System.Configuration;
using dot.Json;
using dot.Json.Linq;
using System.Diagnostics;

namespace JoystickCurves
{
    public class JoyStateFlag
    {
        public String name
        {
            get;set;
        }
        public int value
        {
            get;set;
        }
        public bool changed
        {
            get;set;
        }
    }
    public class NetworkServer
    {
        private const int SENDPERIOD = 16;
        private int _port;
        private WebSocketServer ws;
        private object lockSend = new object();
        private object lockConnect = new object();
        private Timer sendTimer;
        private List<JoyStateFlag> joystateflags;
        public NetworkServer( String listenPort )
        {
            int.TryParse(listenPort, out _port);
            Port = _port;
            Running = false;
            sendTimer = new Timer(sendTimerCallback, null, Timeout.Infinite, Timeout.Infinite);
            joystateflags = new List<JoyStateFlag>();

        }
        private void sendTimerCallback(object state)
        {
            lock (lockSend)
            {
                try
                {
                    if (joystateflags.Exists(f => f.changed == true))
                    {
                        var tempList = joystateflags.ToList();
                        foreach (var jstate in tempList.Where(f => f.changed == true))
                        {
                            var allSessions = ws.GetAllSessions();
                            var jsonContent = JsonConvert.SerializeObject(new JoystickState() { n = jstate.name, v = jstate.value });
                            foreach (WebSocketSession session in allSessions)
                            {
                                SendCurrentState(session, jsonContent);
                            }
                        }
                    }
                }
                catch { };
            }

        }
        public bool Running
        {
            get;
            set;
        }
        public int Port
        {
            get;
            set;
        }
        public void Start()
        {
            lock( lockConnect)
            {
                if (Running)
                    return;

                if( Port <= 0 || Port > 65535 )
                    return;

                try
                {
                    ws = new WebSocketServer();
                    ws.Setup(Port);                   
                    ws.NewSessionConnected += new SuperSocket.SocketBase.SessionHandler<WebSocketSession>(ws_NewSessionConnected);
                    ws.SessionClosed += new SuperSocket.SocketBase.SessionHandler<WebSocketSession, SuperSocket.SocketBase.CloseReason>(ws_SessionClosed);                   
                    ws.Start();
                    sendTimer.Change(0, SENDPERIOD);
                }
                catch { 
                    Running = false; 
                    return; 
                }
            
                Running = true;
            }

        }
        public void SendToAll( JoystickState state )
        {
            if (!Running )
                return;

            if (!joystateflags.Exists(f => f.name == state.n))
                joystateflags.Add(new JoyStateFlag() { name = state.n, value = state.v, changed = true });
            else
                joystateflags.ForEach(n => { if (n.name == state.n) { n.value = state.v; n.changed = true; } });
            


        }
        void SendCurrentState(WebSocketSession session, String content)
        {
            //session.TrySend(content);
            session.SendResponse(content);
        //    session.Send(content);
        }
        void ws_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            if (!Running)
                return; 
        }

        void ws_NewSessionConnected(WebSocketSession session)
        {
            if (!Running)
                return;
        }

        public void Stop()
        {
            if (!Running)
                return;
            try
            {
                ws.Stop();
                sendTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            catch { };

            Running = false;
        }


    }

    public class JoystickState
    {
        public String n
        {
            get;
            set;
        }

        public int v
        {
            get;
            set;
        }

    }
}
