using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Tours.Application.Events;

public enum RoomType
{
    Small,
    Medium,
    Large,
    Apartment,
    Studio
}

[Message("hotel")]
public class HotelAvailabilityChanged : IEvent
{
    public Guid HotelId { get; set; }
    public RoomType Type { get; set; }
    public int Capacity { get; set; }
    public int Amount { get; set; }
}