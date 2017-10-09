using System;
using System.Collections.Generic;

namespace EventTransmit
{
    public class EventDispatcher
    {
        private static EventController EventController_ = new EventController();
        public static Dictionary<string, Delegate> EventRouter
        {
            get
            {
                return EventDispatcher.EventController_.EventRouter;
            }
        }

        public static void MarkAsPermanent(string eventType)
        {
            EventDispatcher.EventController_.MarkAsPermanent(eventType);
        }

        public static void Cleanup()
        {
            EventDispatcher.EventController_.Cleanup();
        }

        public static void AddEventListener(string eventType, Action handler)
        {
            EventDispatcher.EventController_.AddEventListener(eventType, handler);
        }

        public static void AddEventListener<T>(string eventType, Action<T> handler)
        {
            EventDispatcher.EventController_.AddEventListener<T>(eventType, handler);
        }

        public static void AddEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            EventDispatcher.EventController_.AddEventListener<T, U>(eventType, handler);
        }

        public static void AddEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            EventDispatcher.EventController_.AddEventListener<T, U, V>(eventType, handler);
        }

        public static void AddEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            EventDispatcher.EventController_.AddEventListener<T, U, V, W>(eventType, handler);
        }

        public static void RemoveEventListener(string eventType, Action handler)
        {
            EventDispatcher.EventController_.RemoveEventListener(eventType, handler);
        }

        public static void RemoveEventListener<T>(string eventType, Action<T> handler)
        {
            EventDispatcher.EventController_.RemoveEventListener<T>(eventType, handler);
        }

        public static void RemoveEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            EventDispatcher.EventController_.RemoveEventListener<T, U>(eventType, handler);
        }

        public static void RemoveEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            EventDispatcher.EventController_.RemoveEventListener<T, U, V>(eventType, handler);
        }

        public static void RemoveEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            EventDispatcher.EventController_.RemoveEventListener<T, U, V, W>(eventType, handler);
        }

        public static void TriggerEvent(string eventType)
        {
            EventDispatcher.EventController_.TriggerEvent(eventType);
        }

        public static void TriggerEvent<T>(string eventType, T arg1)
        {
            EventDispatcher.EventController_.TriggerEvent<T>(eventType, arg1);
        }

        public static void TriggerEvent<T, U>(string eventType, T arg1, U arg2)
        {
            EventDispatcher.EventController_.TriggerEvent<T, U>(eventType, arg1, arg2);
        }

        public static void TriggerEvent<T, U, V>(string eventType, T arg1, U arg2, V arg3)
        {
            EventDispatcher.EventController_.TriggerEvent<T, U, V>(eventType, arg1, arg2, arg3);
        }

        public static void TriggerEvent<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
        {
            EventDispatcher.EventController_.TriggerEvent<T, U, V, W>(eventType, arg1, arg2, arg3, arg4);
        }


    }
}
