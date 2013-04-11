using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Threading;
using System.Diagnostics;

namespace JoystickCurves
{
    public class DirectInputKeyboard : DirectInputDevice
    {
        public event EventHandler<EventArgs> OnAcquire;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnKeyDown;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnKeyUp;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnKeyPress;

        private const int POLL_INTERVAL = 10;

        private Timer _pollTimer;
        private Device _device;
        private Dictionary<Key, List<Action<DirectInputData>>> _actionMap = new Dictionary<Key,List<Action<DirectInputData>>>();
        private void emptyAction(DirectInputData joyData) {}

        public DirectInputKeyboard(DeviceInstance dev)
        {
            Acquired = false;
            DeviceInstance = dev;

            _pollTimer = new Timer(new TimerCallback(poll_Tick), null, Timeout.Infinite, Timeout.Infinite);
            OnAcquire += new EventHandler<EventArgs>(Keyboard_OnAcquire);
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
                    Key dataType = (Key)data.Offset;
                    KeyState state = (KeyState)data.Data;

                    DirectInputData keyData = new DirectInputData()
                    {
                        Value = data.Data,
                        KeyboardKey = dataType,
                    };
                    CustomEventArgs<DirectInputData> eventArg = new CustomEventArgs<DirectInputData>(keyData);

                    _actionMap.TryGetValue(dataType, out action);

                    if (action != null)
                        action.ForEach(a => a(keyData));

                    if (state == KeyState.Down && OnKeyDown != null)
                        OnKeyDown(this, eventArg);

                    if (state == KeyState.Up)
                    {
                        if (OnKeyUp != null)
                            OnKeyUp(this, eventArg);
                        if (OnKeyPress != null)
                            OnKeyPress(this, eventArg);
                    }

                }
            }
            _pollTimer.Change(POLL_INTERVAL, POLL_INTERVAL);                
        }
        private void Keyboard_OnAcquire(object sender, EventArgs e)
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
                _device.SetDataFormat(DeviceDataFormat.Keyboard);
                _device.Properties.BufferSize = 16;
                _device.SetCooperativeLevel(Process.GetCurrentProcess().MainWindowHandle,
                    CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);

                _device.Acquire();
            }
            catch {
                return;
            }
            _pollTimer.Change(0, POLL_INTERVAL);
            
            if (OnAcquire != null)
                OnAcquire(this, EventArgs.Empty);
        }
        public void SetActions(Dictionary<Key, Action<DirectInputData>> actions)
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
