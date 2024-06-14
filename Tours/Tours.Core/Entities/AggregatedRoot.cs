using System;
using System.Collections.Generic;
using System.Linq;
using Tours.Core.Events;

namespace Tours.Core.Entities;

public class AggregateRoot
{
    private readonly List<IDomainEvent> _events = new();
    public IEnumerable<IDomainEvent> Events => _events;
    public Guid Id { get; protected set; }
    public int Version { get; protected set; }

    protected void AddEvent(IDomainEvent @event)
    {
        if (!_events.Any()) Version++;

        _events.Add(@event);
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}