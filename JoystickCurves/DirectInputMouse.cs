using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.DirectInput;
using System.Threading;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace JoystickCurves
{
    public class DirectInputMouse : DirectInputDevice
    {
        public event EventHandler<EventArgs> OnAcquire;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnButtonDown;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnButtonUp;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnButtonPress;
        public event EventHandler<EventArgs> OnError;
        public event EventHandler<EventArgs> OnUnacquire;

        private const int POLL_INTERVAL = 10;
        private const int DEFAULT_SENS = 150;
        private object pollLock = new object();
        private Timer _pollTimer;
        private Mouse _device;
        private Dictionary<MouseOffset, HashSet<Action<DirectInputData>>> _actionMap = new Dictionary<MouseOffset,HashSet<Action<DirectInputData>>>();
        private void emptyAction(DirectInputData joyData) {}

        public DirectInputMouse(DeviceInstance dev)
        {
            Acquired = false;
            DeviceInstance = dev;
            CurrentValue = new Dictionary<MouseOffset, int>();
            Sensitivity = new Dictionary<MouseOffset, int>();
            
            foreach (var moff in DIUtils.AllNamesMouse)
            {
                CurrentValue.Add(moff.Key, 0);
                if (moff.Key < MouseOffset.Buttons0)
                    Sensitivity.Add(moff.Key, DEFAULT_SENS);
            }            
            _pollTimer = new Timer(new TimerCallback(poll_Tick), null, Timeout.Infinite, Timeout.Infinite);
            OnAcquire += new EventHandler<EventArgs>(Mouse_OnAcquire);
        }
        [HandleProcessCorruptedStateExceptions]
        private void poll_Tick(object o)
        {
            lock (pollLock)
            {
                HashSet<Action<DirectInputData>> action;
                MouseUpdate[] queue = null;

                try
                {
                    _device.Poll();
                    queue = _device.GetBufferedData();
                }
                catch
                {
                    Debug.Print("DirectInputMouse::poll_tick exception");
                    _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    if (OnError != null)
                        OnError(this, EventArgs.Empty);

                    return;
                }

                if (queue != null)
                {
                    foreach (MouseUpdate data in queue)
                    {
                        MouseOffset dataType = (MouseOffset)data.Offset;

                        var curValue = data.Value;

                        if (dataType < MouseOffset.Buttons0)
                        {
                            curValue = CurrentValue.FirstOrDefault(m => m.Key == dataType).Value;
                            var delta = (int)((float)data.Value * (float)Sensitivity[dataType] / 10.0f);
                            if (curValue + delta > 32767)
                                curValue = 32767;
                            else if (curValue + delta < -32767)
                                curValue = -32767;
                            else
                                curValue += delta;

                            CurrentValue[dataType] = curValue;
                        }
                        DirectInputData mouseData = new DirectInputData()
                        {
                            Value = curValue,
                            MouseOffset = dataType,
                            Type = DIDataType.Mouse,
                            DeviceName = Name
                        };

                        CustomEventArgs<DirectInputData> eventArg = new CustomEventArgs<DirectInputData>(mouseData);

                        _actionMap.TryGetValue(dataType, out action);

                        if (action != null)
                            action.ToList().ForEach(a => { if (a != null) a(mouseData); });

                        if (dataType >= MouseOffset.Buttons0)
                        {
                            if ((KeyState)data.Value == KeyState.Down && OnButtonDown != null)
                                OnButtonDown(this, eventArg);

                            if ((KeyState)data.Value == KeyState.Up)
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
            }
        }
        private void Mouse_OnAcquire(object sender, EventArgs e)
        {
            Acquired = true;
        }
        public Dictionary<MouseOffset, int> Sensitivity
        {
            get;
            set;
        }
        private Dictionary<MouseOffset, int> CurrentValue
        {
            get;
            set;
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
                Debug.Print("DirectInputMouse::Unacquire exception");
            }
            if (OnUnacquire != null)
                OnUnacquire(this, EventArgs.Empty);
        }

        public void Acquire()
        {
            try
            {

                var directInput = new DirectInput();
                _device = new Mouse(directInput);
                // _device.SetDataFormat(DeviceDataFormat.Mouse);
                _device.Properties.BufferSize = 16;
                _device.SetCooperativeLevel(Process.GetCurrentProcess().MainWindowHandle,
                    CooperativeLevel.NonExclusive | CooperativeLevel.Background);

                _device.Acquire();
            }
            catch
            {
                Debug.Print("DirectInputMouse::Acquire exception");
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

        public void DeleteActions(MouseOffset key)
        {
            _actionMap.Remove(key);
        }
        public void AddAction(MouseOffset key, Action<DirectInputData> action)
        {
            if (action == null)
                return;

            if (!_actionMap.Keys.Contains(key))
                _actionMap.Add(key, new HashSet<Action<DirectInputData>>());

            _actionMap[key].Add(action);
        }
        public void SetActions(Dictionary<MouseOffset, Action<DirectInputData>> actions)
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
