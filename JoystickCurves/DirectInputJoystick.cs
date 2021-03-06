﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System.Threading;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
namespace JoystickCurves
{
    public class DirectInputJoystick : DirectInputDevice
    {

        public event EventHandler<EventArgs> OnUnacquire;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnAxisChange;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnButtonPress;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnButtonDown;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnButtonUp;
        public event EventHandler<EventArgs> OnError;
        public event EventHandler<EventArgs> OnAcquire;

        private object pollLock = new object();
        private const int POLL_INTERVAL = 10;
        private const int AXIS_RANGE = 32767;
        private JoystickOffset[] JoystickAxis = new JoystickOffset[] { JoystickOffset.X, 
                                                     JoystickOffset.Y, 
                                                     JoystickOffset.Z, 
                                                     JoystickOffset.RX, 
                                                     JoystickOffset.RY, 
                                                     JoystickOffset.RZ, 
                                                     JoystickOffset.Slider0, 
                                                     JoystickOffset.Slider1};

        private bool _exclusive;
        private Timer _pollTimer;
        private Device _device;
        private Dictionary<JoystickOffset, AxisFilterMedian> _axisFilters = new Dictionary<JoystickOffset, AxisFilterMedian>();
        private Dictionary<JoystickOffset, HashSet<Action<DirectInputData>>> _actionMap = new Dictionary<JoystickOffset, HashSet<Action<DirectInputData>>>();
        private VirtualJoystick _virtualJoystick;
        private object lockSet = new object();
        private void emptyAction(DirectInputData joyData) {}

        public DirectInputJoystick(String name)
        {
            Name = name;
            Type = DeviceType.NotSet;
            Acquired = false;
            ExclusiveMode = false;
        }
        public DirectInputJoystick( DeviceInstance dev, DeviceType type )
        {
            DeviceInstance = dev;
            Type = type;
            Acquired = false;
            ExclusiveMode = false;
            
            _pollTimer = new Timer(new TimerCallback(poll_Tick), null, Timeout.Infinite, Timeout.Infinite);
            OnAcquire += new EventHandler<EventArgs>(Joystick_OnAcquire);
        }

        public VirtualJoystick VirtualJoystick
        {
            get { return _virtualJoystick; }
            set { _virtualJoystick = value; }
        }
        [HandleProcessCorruptedStateExceptions]
        private void poll_Tick(object o)
        {
            if (!Acquired)
                return;
            
            lock (pollLock)
            {
                HashSet<Action<DirectInputData>> action;
                AxisFilterMedian filter;
                BufferedDataCollection queue = null;

                try
                {
                    _device.Poll();
                    queue = _device.GetBufferedData();                    

                    if (queue != null)
                    {
                        foreach (BufferedData data in queue)
                        {
                            JoystickOffset dataType = (JoystickOffset)data.Offset;

                            DirectInputData joyData = new DirectInputData()
                            {
                                Value = data.Data,
                                JoystickOffset = dataType,
                                Type = DIDataType.Joystick,
                                DeviceName = Name
                            };

                            var eventArgs = new CustomEventArgs<DirectInputData>(joyData);

                            _actionMap.TryGetValue(dataType, out action);

                            //Axis
                            if (dataType <= JoystickOffset.PointOfView3)
                            {
                                _axisFilters.TryGetValue(dataType, out filter);

/*                                if (filter != null && filter.Noise != null && eventArgs.Data.Value != MinAxisValue && eventArgs.Data.Value != MaxAxisValue)
                                {
                                    eventArgs.Data.Value = (int)filter.Correct((double)eventArgs.Data.Value);
                                    Debug.Print(eventArgs.Data.Value.ToString());
                                }
*/
                                if (filter != null && filter.Length > 0)
                                {
                                    if (filter.IsFilled && eventArgs.Data.Value != MinAxisValue && eventArgs.Data.Value != MaxAxisValue)
                                    {
                                        eventArgs.Data.Value = filter.Add((short)eventArgs.Data.Value);
                                    }
                                    else
                                    {
                                        filter.Add((short)eventArgs.Data.Value);
                                    }
                                }
                                eventArgs.Data.Min = MinAxisValue;
                                eventArgs.Data.Max = MaxAxisValue;

                                if (OnAxisChange != null)
                                    OnAxisChange(this, eventArgs);
                            }
                            else //Button
                            {
                                if (joyData.Value == (int)KeyState.Up)
                                {
                                    if (OnButtonUp != null)
                                        OnButtonUp(this, eventArgs);
                                    if (OnButtonPress != null)
                                        OnButtonPress(this, eventArgs);
                                }
                                else if (joyData.Value == (int)KeyState.Down)
                                {
                                    if (OnButtonDown != null)
                                        OnButtonDown(this, eventArgs);
                                }
                            }
                            if (action != null)
                                action.ToList().ForEach(a => a(eventArgs.Data));
                        }
                    }
                }
                catch
                {
                    Acquired = false;
                    Debug.Print("Joystick poll tick exception");
                    _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    if (OnError != null)
                        OnError(this, EventArgs.Empty);
                    return;
                }

            }
        }
        private void Joystick_OnAcquire(object sender, EventArgs e)
        {
            Acquired = true;
        }

        public int MinAxisValue
        {
            get { return -AXIS_RANGE; }
        }
        public int MaxAxisValue
        {
            get { return AXIS_RANGE; }
        }
        public int NumberAxes
        {
            get { return _device.Caps.NumberAxes; }
        }
        public int NumberButtons
        {
            get { return _device.Caps.NumberButtons; }
        }
        public int NumberPOVs
        {
            get { return _device.Caps.NumberPointOfViews;}
        }
        public bool ExclusiveMode
        {
            get { return _exclusive; }
            set {
                if (value != _exclusive)
                {
                    _exclusive = value;
                    if (Acquired )
                    {
                        Unacquire();
                        Acquire();
                    }
                }
            }

        }
        public void Unacquire()
        {
            lock (pollLock)
            {
                    Acquired = false;
                    _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);

                _actionMap.Clear();
                try
                {
                    _device.Unacquire();
                }
                catch {
                    Debug.Print("Joystick unacquire exception");
                }

                if (Type == DeviceType.Virtual)
                {
                    try
                    {
                        _virtualJoystick.Unacquire();
                    }
                    catch {
                        Debug.Print("Virtual joystick unacquire exception");
                    }
                }
                if (OnUnacquire != null)
                    OnUnacquire(this, EventArgs.Empty);
            }
        }
        
        public void Acquire()
        {
            try
            {
                _device = new Device(Guid);
                _device.SetDataFormat(DeviceDataFormat.Joystick);
                _device.Properties.BufferSize = 16;
                Debug.Print("Exclusive: {0} {1}", Name, _exclusive);
                _device.SetCooperativeLevel(Process.GetCurrentProcess().MainWindowHandle,
                   CooperativeLevelFlags.NonExclusive|CooperativeLevelFlags.Background);


                foreach (DeviceObjectInstance d in _device.Objects)
                {
                    if ((d.ObjectId & (int)DeviceObjectTypeFlags.Axis) != 0)
                        _device.Properties.SetRange(ParameterHow.ById, d.ObjectId, new InputRange(MinAxisValue, MaxAxisValue));
                }
                _device.Properties.AxisModeAbsolute = true;
                _device.Acquire();

                _pollTimer.Change(0, POLL_INTERVAL);
                Acquired = true;
                if (OnAcquire != null)
                    OnAcquire(this, EventArgs.Empty);
            }
            catch (OtherApplicationHasPriorityException)
            {
                Acquired = false;
                LastErrorMessage = Name + " couldn't be used in exclusive mode!";
                Debug.Print(LastErrorMessage);
                if (OnError != null)
                    OnError(this, EventArgs.Empty);

                return;
            }
            catch
            {
                Debug.Print("DirectInputJoystick::Acquire exception");
                LastErrorMessage = "DirectInput acquire error!";
                if (OnError != null)
                    OnError(this, EventArgs.Empty);
            }
        }
        public void SetAxisFilter(JoystickOffset offset, int arrayLength)
        {
            AxisFilterMedian currentFilter;
            try
            {
                if (!_axisFilters.TryGetValue(offset, out currentFilter))
                {
                    _axisFilters.Add(offset, new AxisFilterMedian( arrayLength * 4 ));
                }
                else
                {
                    _axisFilters[offset] = new AxisFilterMedian( arrayLength * 4 );
                }
            }
            catch
            {
                Debug.Print("SetAxisFilter exception");
            }

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
        public void DeleteActions(JoystickOffset key)
        {
            _actionMap.Remove(key);
        }
        public void AddAction(JoystickOffset key, Action<DirectInputData> action)
        {
            if (action == null)
                return;

            if (!_actionMap.Keys.Contains(key))
                _actionMap.Add(key, new HashSet<Action<DirectInputData>>());

            _actionMap[key].Add(action);
        }
        public void SetActions(Dictionary<JoystickOffset, Action<DirectInputData>> actions)
        {
            if (actions == null || actions.Count <= 0)
                return;

            foreach (var inAction in actions.Values.Distinct())
            {
                DeleteAction(inAction);
            }
            foreach (var k in actions.Keys)
            {
                if (!_actionMap.Keys.Contains(k))
                    _actionMap.Add(k, new HashSet<Action<DirectInputData>>());

                _actionMap[k].Add(actions[k]);
            }
        }

        public int Get(JoystickOffset offset)
        {
            try
            {
                var state = _device.CurrentJoystickState;
                switch (offset)
                {
                    case JoystickOffset.X:
                        return state.X;
                    case JoystickOffset.Y:
                        return state.Y;
                    case JoystickOffset.Z:
                        return state.Z;
                    case JoystickOffset.RX:
                        return state.Rx;
                    case JoystickOffset.RY:
                        return state.Ry;
                    case JoystickOffset.RZ:
                        return state.Rz;
                    case JoystickOffset.Slider0:
                        return state.GetSlider()[0];
                    case JoystickOffset.Slider1:
                        return state.GetSlider()[1];
                }
            }
            catch {
                Debug.Print("Get joystick value exception");
                return 0; 
            }
            
            
            return 0;
        }
        public void Set(JoystickOffset offset, int value)
        {
            
            if (Type != DeviceType.Virtual || _virtualJoystick == null)
                return;

            lock (lockSet)
            {
                if (offset >= JoystickOffset.Button0 && offset <= JoystickOffset.Button128)
                {
                    _virtualJoystick.SetButton((uint)(offset - JoystickOffset.Button0), value == 128 ? true : false);
                    return;
                }

                switch (offset)
                {
                    case JoystickOffset.X:
                        _virtualJoystick.X = value;
                        break;
                    case JoystickOffset.Y:
                        _virtualJoystick.Y = value;
                        break;
                    case JoystickOffset.Z:
                        _virtualJoystick.Z = value;
                        break;
                    case JoystickOffset.RX:
                        _virtualJoystick.RX = value;
                        break;
                    case JoystickOffset.RY:
                        _virtualJoystick.RY = value;
                        break;
                    case JoystickOffset.RZ:
                        _virtualJoystick.RZ = value;
                        break;
                    case JoystickOffset.Slider0:
                        _virtualJoystick.SL0 = value;
                        break;
                    case JoystickOffset.Slider1:
                        _virtualJoystick.SL1 = value;
                        break;
                }
            }


        }

    }
}
