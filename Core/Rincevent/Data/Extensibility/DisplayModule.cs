using System;
using System.Collections.Generic;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Extensibility;

namespace Meow.FR.Rincevent.Core.Extensibility
{
    public abstract class DisplayModule : BaseModule
    {
        /* INFORMATION */
        abstract public DisplayModuleSettings Settings { get; }

        /* BASIC SETTINGS */
        abstract public int Timer { get; }

        /* ACTIONS */
        abstract public void Start(ICore core);
        abstract public void Show(Content content);
        abstract public void ReceiveAllData(List<Content> content);
        abstract public void Stop();

        /* ANTI-BOSS */
        abstract public void BossHide();
        abstract public void BossShow();

        /* EVENTS */
        private void SafeEventSubscribe(ref EventHandler ev, EventHandler dg)
        {
            if (ev != null)
                foreach (Delegate current in ev.GetInvocationList())
                    if (current.Target == dg.Target)
                        return;
            ev += dg;
        }

        private void SafeEventUnsubscribe(ref EventHandler ev, EventHandler dg)
        {
            if (ev != null)
                foreach (Delegate current in ev.GetInvocationList())
                    if (current.Target == dg.Target)
                    {
                        ev -= dg;
                        return;
                    }
        }

        /// <summary>
        /// Called once the module has been started.
        /// </summary
        public event EventHandler StartedEvent
        {
            add { SafeEventSubscribe(ref _StartedEvent, value); }
            remove { SafeEventUnsubscribe(ref _StartedEvent, value); }
        }
        public event EventHandler _StartedEvent;
        public void InvokeStartedEvent(EventArgs e) { _StartedEvent.Invoke(this, null); }

        /// <summary>
        /// Called once the information has been displayed.
        /// </summary>
        public event EventHandler ShowedEvent
        {
            add { SafeEventSubscribe(ref _ShowedEvent, value); }
            remove { SafeEventUnsubscribe(ref _ShowedEvent, value); }
        }
        private event EventHandler _ShowedEvent;
        public void InvokeShowedEvent(EventArgs e) { _ShowedEvent.Invoke(this, null); }


        /// <summary>
        /// Called when the module NEEDS to be stopped.
        /// </summary>
        public event EventHandler StoppedEvent
        {
            add { SafeEventSubscribe(ref _StoppedEvent, value); }
            remove { SafeEventUnsubscribe(ref _StoppedEvent, value); }
        }
        public event EventHandler _StoppedEvent;
        public void InvokeStoppedEvent(EventArgs e) { _StoppedEvent.Invoke(this, null); }

        /// <summary>
        /// Called when the module needs all the data.
        /// </summary>
        public event EventHandler WaitForAllDataEvent
        {
            add {
                SafeEventSubscribe(ref _WaitForAllDataEvent, value); 
            }
            remove { SafeEventUnsubscribe(ref _WaitForAllDataEvent, value); }
        }
        private event EventHandler _WaitForAllDataEvent;
        public void InvokeWaitForAllDataEvent(EventArgs e) { _WaitForAllDataEvent.Invoke(this, null); }
    }
}
