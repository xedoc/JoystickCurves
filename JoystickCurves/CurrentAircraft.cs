using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace JoystickCurves
{

    public class EventArgsString: EventArgs
    {

        public String Value
        {
            get;
            set;
        }
        public String Name
        {
            get;
            set;
        }
    }

    public abstract class CurrentAircraft
    {
        public event EventHandler<EventArgsString> AircraftChange;
        public event EventHandler<EventArgsString> OnError;

        public String currentName;
                                    
        protected virtual void OnAircraftChange(EventArgsString e)
        {
            if (AircraftChange != null)
                AircraftChange(this, e);
        }
        protected void RaiseError(EventArgsString e)
        {
            if (OnError != null)
                OnError(this, e);
        }

        public virtual void StartPoll()
        {
        }
        public virtual void StopPoll()
        {
        }

        public String AircraftName
        {
            get 
            {
                return currentName; 
            }
            set
            {
                if( value != currentName && !String.IsNullOrEmpty(value) ) 
                {
                    currentName = value;
                    OnAircraftChange(new EventArgsString() { Name = GameName, Value = currentName });
                }
            }
        }
        public String GameName
        {
            get;
            set;
        }
    }
}
