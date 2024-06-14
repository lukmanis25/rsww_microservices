using Convey.CQRS.Events;
using Tours.Application.Services;
using Tours.Core.Entities;

namespace Tours.Application.Events;

public class HotelChangedHandler : IEventHandler<HotelChanged>
{
    private readonly IMessageBroker _messageBroker;
    private readonly ITourRepository _tourRepository;

    public HotelChangedHandler(IMessageBroker messageBroker, ITourRepository tourRepository)
    {
        _messageBroker = messageBroker;
        _tourRepository = tourRepository;
    }

    public async Task HandleAsync(HotelChanged @event, CancellationToken cancellationToken = default)
    {
        var tour = await _tourRepository.GetAllByHotelId(@event.HotelId);
        var toursToUpdate = new List<Tour>();
        foreach (Tour t in tour)
        {

            foreach (Room r in t.HotelResource.Rooms)
            {
                if (r.Type == @event.Type.RoomTypeAsString() && r.Capacity == @event.Capacity)
                {
                    r.Price = @event.Price;
                    r.NumberOf = @event.NumberOf;
                    toursToUpdate.Add(t);
                }
            }
        }

        await _tourRepository.UpdateManyAsync(toursToUpdate);
    }
}