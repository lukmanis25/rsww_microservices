using Convey.CQRS.Events;
using System.ComponentModel.DataAnnotations;
using Tours.Application.Services;
using Tours.Core.Entities;

namespace Tours.Application.Events;

public class HotelAvailabilityChangedHandler : IEventHandler<HotelAvailabilityChanged>
{
    private readonly IMessageBroker _messageBroker;
    private readonly ITourRepository _tourRepository;

    public HotelAvailabilityChangedHandler(IMessageBroker messageBroker, ITourRepository tourRepository)
    {
        _messageBroker = messageBroker;
        _tourRepository = tourRepository;
    }

    public async Task HandleAsync(HotelAvailabilityChanged @event, CancellationToken cancellationToken = default)
    {
        var tour = await _tourRepository.GetAllByHotelId(@event.HotelId);
        var toursToUpdate = new List<Tour>();
        foreach (Tour t in tour)
        {

            foreach (Room r in t.HotelResource.Rooms)
            {
                if (r.Type == @event.Type.RoomTypeAsString() && r.Capacity == @event.Capacity)
                {
                    r.ReservedNumber = @event.Amount;
                }
            }
            toursToUpdate.Add(t);

        }

        await _tourRepository.UpdateManyAsync(toursToUpdate);
    }
}