﻿using Convey.CQRS.Events;

namespace Tours.Application.Services;

public interface IMessageBroker
{
    Task PublishAsync(params IEvent[] events);
    Task PublishAsync(IEnumerable<IEvent> events);
}