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
        public event EventHandler<EventArgs> OnError;
        public event EventHandler<EventArgs> OnUnacquire;


        private const int POLL_INTERVAL = 10;
        private object pollLock = new object();
        private Timer _pollTimer;
        private Device _device;
        private Dictionary<Key, HashSet<Action<DirectInputData>>> _actionMap = new Dictionary<Key,HashSet<Action<DirectInputData>>>();
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
            lock (pollLock)
            {
                HashSet<Action<DirectInputData>> action;
                BufferedDataCollection queue = null;

                try
                {
                    _device.Poll();
                    queue = _device.GetBufferedData();
                }
                catch
                {
                    Debug.Print("DirectInputKeyboard::poll_tick exception");
                    _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    if (OnError != null)
                        OnError(this, EventArgs.Empty);

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
                            Type = DIDataType.Keyboard,
                            DeviceName = Name

                        };
                        CustomEventArgs<DirectInputData> eventArg = new CustomEventArgs<DirectInputData>(keyData);

                        _actionMap.TryGetValue(dataType, out action);

                        if (action != null)
                            action.ToList().ForEach(a => a(keyData));

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
            }
              
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
            catch {
                Debug.Print("DirectInputKeyboard::Unacquire exception");
            }
            if (OnUnacquire != null)
                OnUnacquire(this, EventArgs.Empty);
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
                Debug.Print("DirectInputKeyboard::Acquire exception");
                return;
            }
            _pollTimer.Change(0, POLL_INTERVAL);
            
            if (OnAcquire != null)
                OnAcquire(this, EventArgs.Empty);
        }
        public void DeleteAction(Action<DirectInputData> action)
        {
            if (action == null)
                return;

            foreach (var perKeyActions in _actionMap.Values)
            {
                perKeyActions.RemoveWhere(a => a == action);
            }

        }

        public void DeleteActions(Key key)
        {
            _actionMap.Remove(key);
        }
        public void AddAction(Key key, Action<DirectInputData> action)
        {
            if (action == null)
                return;

            if (!_actionMap.Keys.Contains(key))
                _actionMap.Add(key, new HashSet<Action<DirectInputData>>());

            _actionMap[key].Add(action);
        }

        public void SetActions(Dictionary<Key, Action<DirectInputData>> actions)
        {
            if (actions == null || actions.Count <= 0)
                return;

            foreach (var inAction in actions.Values.Distinct())
            {
                foreach (var perKeyActions in _actionMap.Values)
                {
                    perKeyActions.RemoveWhere( a => a == inAction);
                }
            }
            foreach (var k in actions.Keys)
            {
                if (!_actionMap.Keys.Contains(k))
                    _actionMap.Add(k, new HashSet<Action<DirectInputData>>());

                _actionMap[k].Add(actions[k]);
            }
        }
       

    }
}
