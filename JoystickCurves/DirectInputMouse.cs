using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Threading;
using System.Diagnostics;

namespace JoystickCurves
{
    public class DirectInputMouse : DirectInputDevice
    {
        public event EventHandler<EventArgs> OnAcquire;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnButtonDown;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnButtonUp;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnButtonPress;
        private const int POLL_INTERVAL = 10;

        private Timer _pollTimer;
        private Device _device;
        private Dictionary<MouseOffset, List<Action<DirectInputData>>> _actionMap = new Dictionary<MouseOffset,List<Action<DirectInputData>>>();
        private void emptyAction(DirectInputData joyData) {}

        public DirectInputMouse(DeviceInstance dev)
        {
            Acquired = false;
            DeviceInstance = dev;

            _pollTimer = new Timer(new TimerCallback(poll_Tick), null, Timeout.Infinite, Timeout.Infinite);
            OnAcquire += new EventHandler<EventArgs>(Mouse_OnAcquire);
        }
        private void poll_Tick(object o)
        {
            List<Action<DirectInputData>> action;
            BufferedDataCollection queue = null;

            _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);

            try
            {
                _device.Poll();
                queue = _device.GetBufferedData();
            }
            catch {
                _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);
                return;
            }

            if (queue != null)
            {
                foreach (BufferedData data in queue)
                {
                    MouseOffset dataType = (MouseOffset)data.Offset;

                    DirectInputData mouseData = new DirectInputData()
                    {
                        Value = data.Data,
                        MouseOffset = dataType
                    };

                    CustomEventArgs<DirectInputData> eventArg = new CustomEventArgs<DirectInputData>(mouseData);

                    _actionMap.TryGetValue(dataType, out action);

                    if (action != null)
                        action.ForEach(a => a(mouseData));

                    if (dataType >= MouseOffset.Button0)
                    {
                        if ((KeyState)data.Data == KeyState.Down && OnButtonDown != null)
                            OnButtonDown(this, eventArg);

                        if ((KeyState)data.Data == KeyState.Up)
                        {
                            if (OnButtonUp != null)
                                OnButtonUp(this, eventArg);
                            if (OnButtonPress != null)
                                OnButtonPress(this, eventArg);
                        }

                    }
                    else
                    {
                        //Mouse axis
                    }


                }
            }
            _pollTimer.Change(POLL_INTERVAL, POLL_INTERVAL);                
        }
        private void Mouse_OnAcquire(object sender, EventArgs e)
        {
            Acquired = true;
        }

        public void Unacquire()
        {
            _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);
            _actionMap.Clear();
            try
            {
                _device.Unacquire();
            }
            catch { }

        }

        public void Acquire()
        {
            try
            {

                _device = new Device(Guid);
                _device.SetDataFormat(DeviceDataFormat.Mouse);
                _device.Properties.BufferSize = 16;
                _device.SetCooperativeLevel(Process.GetCurrentProcess().MainWindowHandle,
                    CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);

                _device.Acquire();
            }
            catch
            {
                return;
            }
            _pollTimer.Change(0, POLL_INTERVAL);
            
            if (OnAcquire != null)
                OnAcquire(this, EventArgs.Empty);
        }
        public void SetActions(Dictionary<MouseOffset, Action<DirectInputData>> actions)
        {
            if (actions == null || actions.Count <= 0)
                return;

            foreach (var inAction in actions.Values.Distinct())
            {
                foreach (var perKeyActions in _actionMap.Values)
                {
                    perKeyActions.RemoveAll( a => a == inAction);
                }
            }
            foreach (var k in actions.Keys)
            {
                if (!_actionMap.Keys.Contains(k))
                    _actionMap.Add(k, new List<Action<DirectInputData>>());

                _actionMap[k].Add(actions[k]);
            }
        }       
    }
}
