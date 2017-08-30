using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public static class EventManager
{

    private static Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

    public static void StartListening(string pEventName, UnityAction pListener)
    {
        UnityEvent newEvent = null;
        if (eventDictionary.TryGetValue(pEventName, out newEvent))
        {
            newEvent.AddListener(pListener);
        }
        else
        {
            newEvent = new UnityEvent();
            newEvent.AddListener(pListener);
            eventDictionary.Add(pEventName, newEvent);
        }
    }

    public static void StopListening(string pEventName, UnityAction pListener)
    {
        UnityEvent newEvent = null;
        if (eventDictionary.TryGetValue(pEventName, out newEvent))
        {
            newEvent.RemoveListener(pListener);
        }
    }

    public static void TriggerEvent(string pEventName)
    {
        UnityEvent newEvent = null;
        if (eventDictionary.TryGetValue(pEventName, out newEvent))
        {
            newEvent.Invoke();
        }
    }
}
