using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Tours.Application.Events;

[Message("transport")]
public class TransportAvailabilityChanged : IEvent
{
    public Guid TransportId { get; set; }
    public int Amount { get; set; }
}