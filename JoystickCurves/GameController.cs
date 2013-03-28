using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Threading;
using System.Diagnostics;
namespace JoystickCurves
{
    public enum GameControllerType
    {
        Virtual,
        Physical
    }
    public class GameController
    {

        public event EventHandler<EventArgs> OnAcquire;
        public event EventHandler<CustomEventArgs<int>> OnXChange;
        public event EventHandler<CustomEventArgs<int>> OnYChange;
        public event EventHandler<CustomEventArgs<int>> OnZChange;

        public event EventHandler<CustomEventArgs<int>> OnButtonDown;
        public event EventHandler<CustomEventArgs<int>> OnButtonUp;
        private const int POLL_INTERVAL = 30;
        private Timer _pollTimer;
        private byte[] _buttonState;
        private Device _device;

        public GameController( string name, string productname, Guid productguid, Guid instanceguid, GameControllerType type )
        {
            Name = name;
            ProductName = productname;
            ProductGuid = productguid;
            InstanceGuid = instanceguid;
            Type = type;
            Acquired = false;
            
            _pollTimer = new Timer(new TimerCallback(poll_Tick), null, Timeout.Infinite, Timeout.Infinite);
            OnAcquire += new EventHandler<EventArgs>(GameController_OnAcquire);
            OnButtonUp += new EventHandler<CustomEventArgs<int>>(GameController_OnButtonUp);
            OnButtonDown += new EventHandler<CustomEventArgs<int>>(GameController_OnButtonDown);
            OnXChange += new EventHandler<CustomEventArgs<int>>(GameController_OnXChange);
            OnYChange += new EventHandler<CustomEventArgs<int>>(GameController_OnYChange);
            OnZChange += new EventHandler<CustomEventArgs<int>>(GameController_OnZChange);
        }

        void GameController_OnZChange(object sender, CustomEventArgs<int> e)
        {
           
        }

        void GameController_OnYChange(object sender, CustomEventArgs<int> e)
        {
            
        }

        void GameController_OnXChange(object sender, CustomEventArgs<int> e)
        {   
      
        }

        void GameController_OnButtonDown(object sender, CustomEventArgs<int> e)
        {
            Debug.Print(String.Format("down {0}", e.Data));
        }

        void GameController_OnButtonUp(object sender, CustomEventArgs<int> e)
        {
            Debug.Print(String.Format("up {0}", e.Data));
            
        }
        public bool Acquired
        {
            get;
            set;
        }
        private void poll_Tick(object o)
        {
            _device.Poll();
            //Microsoft.DirectX.DirectInput.JoystickOffset
            
            ReadButtonsState();      
    
        }
        private void ReadAxisState()
        {
            
        }
        private void ReadButtonsState()
        {
            var prevButtonState = _buttonState;

            _buttonState = _device.CurrentJoystickState.GetButtons();
            if (prevButtonState == null)
            {
                prevButtonState = _buttonState;
                return;
            }
            for (var i = 0; i < _buttonState.Length; i++)
            {
                if (_buttonState[i] != prevButtonState[i])
                {
                    if (_buttonState[i] != 0)
                    {
                        if (OnButtonDown != null)
                            OnButtonDown(this, new CustomEventArgs<int>( i));
                    }
                    else
                    {
                        if (OnButtonUp != null)
                            OnButtonUp(this, new CustomEventArgs<int>( i ));
                    }
                }
            }
        }
        void GameController_OnAcquire(object sender, EventArgs e)
        {
            Acquired = true;
        }
        public string Name
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }
        public Guid ProductGuid
        {
            get;
            set;
        }
        public Guid InstanceGuid
        {
            get;
            set;
        }
  
        public GameControllerType Type
        {
            get;
            set;
        }

        public void Acquire()
        {
            _device = new Device(InstanceGuid);
            _device.SetDataFormat(DeviceDataFormat.Joystick);
            _device.Properties.BufferSize = 16;
            _device.Acquire();

           
            _pollTimer.Change(POLL_INTERVAL, POLL_INTERVAL);         
            if (OnAcquire != null)
                OnAcquire(this, EventArgs.Empty);
        }

    }
}
