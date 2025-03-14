using System.Collections.Generic;

public class EventService : ILocatable
{
    public delegate void EventReceiver(params object[] parameters);

    private readonly Dictionary<GameEvent, EventReceiver> _events = null;

    public EventService()
    {
        _events = new Dictionary<GameEvent, EventReceiver>();
    }

    public void SubscribeTo(GameEvent type, EventReceiver listener)
    {
        if (!_events.ContainsKey(type))
        {
            _events.Add(type, null);
        }

        _events[type] += listener;
    }

    public void UnsubscribeFrom(GameEvent type, EventReceiver listener)
    {
        if (_events.ContainsKey(type))
        {
            _events[type] -= listener;
        }
    }

    public void Trigger(GameEvent type)
    {
        Trigger(type, null);
    }

    public void Trigger(GameEvent type, params object[] parameters)
    {
        if (_events.ContainsKey(type))
        {
            _events[type]?.Invoke(parameters);
        }
    }
}