﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Threading;
using System.Diagnostics;
namespace JoystickCurves
{
    public class DirectInputJoystick : DirectInputDevice
    {

        public event EventHandler<EventArgs> OnAcquire;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnAxisChange;
        public event EventHandler<CustomEventArgs<DirectInputData>> OnButtonChange;
        private const int POLL_INTERVAL = 10;
        private const int AXIS_RANGE = 32767;
        private JoystickOffset[] JoystickAxis = new JoystickOffset[] { JoystickOffset.X, 
                                                     JoystickOffset.Y, 
                                                     JoystickOffset.Z, 
                                                     JoystickOffset.RX, 
                                                     JoystickOffset.RY, 
                                                     JoystickOffset.RZ, 
                                                     JoystickOffset.Slider0, 
                                                     JoystickOffset.Slider1 };

        private Timer _pollTimer;
        private Device _device;
        private Dictionary<JoystickOffset, List<Action<DirectInputData>>> _actionMap = new Dictionary<JoystickOffset,List<Action<DirectInputData>>>();
        private VirtualJoystick _virtualJoystick;
        private void emptyAction(DirectInputData joyData) {}

        public DirectInputJoystick(String name)
        {
            Name = name;
            Type = DeviceType.NotSet;
            Acquired = false;
        }
        public DirectInputJoystick( DeviceInstance dev, DeviceType type )
        {
            DeviceInstance = dev;
            Type = type;
            Acquired = false;
            
            _pollTimer = new Timer(new TimerCallback(poll_Tick), null, Timeout.Infinite, Timeout.Infinite);
            OnAcquire += new EventHandler<EventArgs>(Joystick_OnAcquire);
        }

        public VirtualJoystick VirtualJoystick
        {
            get { return _virtualJoystick; }
            set { _virtualJoystick = value; }
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
                    JoystickOffset dataType = (JoystickOffset)data.Offset;

                    DirectInputData joyData = new DirectInputData()
                    {
                        Value = data.Data,
                        JoystickOffset = dataType,
                        DeviceName = Name
                    };

                    _actionMap.TryGetValue(dataType, out action);

                    if (dataType <= JoystickOffset.PointOfView3)
                    {
                        joyData.Min = MinAxisValue;
                        joyData.Max = MaxAxisValue;

                        if (OnAxisChange != null)
                            OnAxisChange(this, new CustomEventArgs<DirectInputData>(joyData));
                    }
                    else
                    {
                        if (OnButtonChange != null)
                            OnButtonChange(this, new CustomEventArgs<DirectInputData>(joyData));
                    }
                    if (action != null)
                        action.ForEach(a => a(joyData));
                }
            }
            _pollTimer.Change(POLL_INTERVAL, POLL_INTERVAL);                
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
        public void Unacquire()
        {
            _pollTimer.Change(Timeout.Infinite, Timeout.Infinite);
            _actionMap.Clear();
            try
            {
                _device.Unacquire();
            }
            catch { }

            if (Type == DeviceType.Virtual)
            {
                try
                {
                    _virtualJoystick.Reset();
                    _virtualJoystick.Unacquire();
                }
                catch { }
            }
        }

        public void Acquire()
        {
            try
            {
                _device = new Device(Guid);
                _device.SetDataFormat(DeviceDataFormat.Joystick);
                _device.Properties.BufferSize = 16;
                _device.SetCooperativeLevel(Process.GetCurrentProcess().MainWindowHandle,
                    CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);


                foreach (DeviceObjectInstance d in _device.Objects)
                {
                    if ((d.ObjectId & (int)DeviceObjectTypeFlags.Axis) != 0)
                        _device.Properties.SetRange(ParameterHow.ById, d.ObjectId, new InputRange(MinAxisValue, MaxAxisValue));
                }
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
        public void DeleteAction(Action<DirectInputData> action)
        {
            if (action == null)
                return;

            foreach (var perKeyActions in _actionMap.Values)
            {
                perKeyActions.RemoveAll(a => a == action);
            }

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
                    _actionMap.Add(k, new List<Action<DirectInputData>>());

                _actionMap[k].Add(actions[k]);
            }
        }
       
        public void Set(JoystickOffset offset, int value)
        {
            if (Type != DeviceType.Virtual || _virtualJoystick == null)
                return;

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