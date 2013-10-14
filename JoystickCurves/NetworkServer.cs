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

    public class NetworkServer
    {
        private int _port;
        private WebSocketServer ws;
        private object lockSend = new object();
        private object lockConnect = new object();
        public NetworkServer( String listenPort )
        {
            int.TryParse(listenPort, out _port);
            Port = _port;
            Running = false;

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

            lock (lockSend)
            {
                try
                {
                    foreach (WebSocketSession session in ws.GetAllSessions())
                    {
                        var jsonContent = JsonConvert.SerializeObject(state);
                        SendCurrentState(session, jsonContent);
                    }
                }
                catch { };
            }

        }
        void SendCurrentState(WebSocketSession session, String content)
        {           
            ThreadPool.QueueUserWorkItem(f => session.Send( content ));
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
