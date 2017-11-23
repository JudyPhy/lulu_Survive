using System;
using System.Collections.Generic;

namespace EventTransmit
{
    public class EventController
    {
        private Dictionary<string, Delegate> EventRouter_ = new Dictionary<string, Delegate>();
        private List<string> PermanentEvents_ = new List<string>();

        public Dictionary<string, Delegate> EventRouter
        {
            get
            {
                return this.EventRouter_;
            }
        }

        public void MarkAsPermanent(string eventType)
        {
            this.PermanentEvents_.Add(eventType);
        }

        public bool ContainsEvent(string eventType)
        {
            return this.EventRouter_.ContainsKey(eventType);
        }

        public void Cleanup()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, Delegate> current in this.EventRouter_)
            {
                bool flag = false;
                for (int i = 0; i < this.PermanentEvents_.Count; i++)
                {
                    string current2 = this.PermanentEvents_[i];
                    if (current.Key == current2)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    list.Add(current.Key);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                string current2 = list[i];
                this.EventRouter_.Remove(current2);
            }
        }

        private void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
        {
            if (!this.EventRouter_.ContainsKey(eventType))
            {
                this.EventRouter_.Add(eventType, null);
            }

            Delegate delgate = this.EventRouter_[eventType];
            if (delgate != null && delgate.GetType() != listenerBeingAdded.GetType())
            {
                throw new EventException(string.Format("Try to add not correct event {0}. Current type is {1}, adding type is {2}.", eventType, delgate.GetType().Name, listenerBeingAdded.GetType().Name));
            }
        }

        private bool OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
        {
            bool result;
            if (!this.EventRouter_.ContainsKey(eventType))
            {
                result = false;
            }
            else
            {
                Delegate delgate = this.EventRouter_[eventType];
                if (delgate != null && delgate.GetType() != listenerBeingRemoved.GetType())
                {
                    throw new EventException(string.Format("Remove listener {0}\" failed, Current type is {1}, adding type is {2}.", eventType, delgate.GetType(), listenerBeingRemoved.GetType()));
                }
                result = true;
            }

            return result;
        }

        private void OnListenerRemoved(string eventType)
        {
            if (this.EventRouter_.ContainsKey(eventType) && this.EventRouter_[eventType] == null)
            {
                this.EventRouter_.Remove(eventType);
            }
        }

        public void AddEventListener(string eventType, Action handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.EventRouter_[eventType] = (Action)Delegate.Combine((Action)this.EventRouter_[eventType], handler);
        }

        public void AddEventListener<T>(string eventType, Action<T> handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.EventRouter_[eventType] = (Action<T>)Delegate.Combine((Action<T>)this.EventRouter_[eventType], handler);
        }

        public void AddEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.EventRouter_[eventType] = (Action<T, U>)Delegate.Combine((Action<T, U>)this.EventRouter_[eventType], handler);
        }

        public void AddEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.EventRouter_[eventType] = (Action<T, U, V>)Delegate.Combine((Action<T, U, V>)this.EventRouter_[eventType], handler);
        }

        public void AddEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.EventRouter_[eventType] = (Action<T, U, V, W>)Delegate.Combine((Action<T, U, V, W>)this.EventRouter_[eventType], handler);
        }

        public void RemoveEventListener(string eventType, Action handler)
        {
            if (this.OnListenerRemoving(eventType, handler))
            {
                this.EventRouter_[eventType] = (Action)Delegate.Remove((Action)this.EventRouter_[eventType], handler);
                this.OnListenerRemoved(eventType);
            }
        }

        public void RemoveEventListener<T>(string eventType, Action<T> handler)
        {
            if (this.OnListenerRemoving(eventType, handler))
            {
                this.EventRouter_[eventType] = (Action<T>)Delegate.Remove((Action<T>)this.EventRouter_[eventType], handler);
                this.OnListenerRemoved(eventType);
            }
        }

        public void RemoveEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            if (this.OnListenerRemoving(eventType, handler))
            {
                this.EventRouter_[eventType] = (Action<T, U>)Delegate.Remove((Action<T, U>)this.EventRouter_[eventType], handler);
                this.OnListenerRemoved(eventType);
            }
        }

        public void RemoveEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            if (this.OnListenerRemoving(eventType, handler))
            {
                this.EventRouter_[eventType] = (Action<T, U, V>)Delegate.Remove((Action<T, U, V>)this.EventRouter_[eventType], handler);
                this.OnListenerRemoved(eventType);
            }
        }

        public void RemoveEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            if (this.OnListenerRemoving(eventType, handler))
            {
                this.EventRouter_[eventType] = (Action<T, U, V, W>)Delegate.Remove((Action<T, U, V, W>)this.EventRouter_[eventType], handler);
                this.OnListenerRemoved(eventType);
            }
        }

        public void TriggerEvent(string eventType)
        {
            Delegate delgate;
            if (this.EventRouter_.TryGetValue(eventType, out delgate))
            {
                Delegate[] invocationList = delgate.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action action = invocationList[i] as Action;
                    if (null == action)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action();
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void TriggerEvent<T>(string eventType, T arg1)
        {
            Delegate delgate;
            if (this.EventRouter_.TryGetValue(eventType, out delgate))
            {
                Delegate[] invocationList = delgate.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action<T> action = invocationList[i] as Action<T>;
                    if (null == action)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action(arg1);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void TriggerEvent<T, U>(string eventType, T arg1, U arg2)
        {
            Delegate delgate;
            if (this.EventRouter_.TryGetValue(eventType, out delgate))
            {
                Delegate[] invocationList = delgate.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action<T, U> action = invocationList[i] as Action<T, U>;
                    if (null == action)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action(arg1, arg2);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void TriggerEvent<T, U, V>(string eventType, T arg1, U arg2, V arg3)
        {
            Delegate delgate;
            if (this.EventRouter_.TryGetValue(eventType, out delgate))
            {
                Delegate[] invocationList = delgate.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action<T, U, V> action = invocationList[i] as Action<T, U, V>;
                    if (null == action)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action(arg1, arg2, arg3);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void TriggerEvent<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
        {
            Delegate delgate;
            if (this.EventRouter_.TryGetValue(eventType, out delgate))
            {
                Delegate[] invocationList = delgate.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action<T, U, V, W> action = invocationList[i] as Action<T, U, V, W>;
                    if (null == action)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action(arg1, arg2, arg3, arg4);
                    }
                    catch
                    {

                    }
                }
            }
        }
    }


}