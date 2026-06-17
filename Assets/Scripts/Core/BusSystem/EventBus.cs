using System;
using System.Collections.Generic;

namespace Atypiki.Core.Core.BusSystem
{
    /*
     * This class handle subscription between classes
     */
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> _subscribers
            = new();

        public static void Subscribe<T>(Action<T> callback)
        {
            if (_subscribers.TryGetValue(typeof(T), out var existing))
            {
                _subscribers[typeof(T)] =
                    Delegate.Combine(existing, callback);
            }
            else
            {
                _subscribers[typeof(T)] = callback;
            }
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            if (_subscribers.TryGetValue(typeof(T), out var existing))
            {
                _subscribers[typeof(T)] =
                    Delegate.Remove(existing, callback);
            }
        }

        public static void Publish<T>(T eventData)
        {
            if (_subscribers.TryGetValue(typeof(T), out var callback))
            {
                ((Action<T>)callback)?.Invoke(eventData);
            }
        }
    }
}