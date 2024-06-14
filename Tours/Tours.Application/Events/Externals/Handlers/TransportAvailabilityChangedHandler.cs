using Convey.CQRS.Events;
using Tours.Application.Services;
using Tours.Core.Entities;

namespace Tours.Application.Events;

public class TransportAvailabilityChangedHandler : IEventHandler<TransportAvailabilityChanged>
{
    private readonly IMessageBroker _messageBroker;
    private readonly ITourRepository _tourRepository;

    public TransportAvailabilityChangedHandler(IMessageBroker messageBroker, ITourRepository tourRepository)
    {
        _messageBroker = messageBroker;
        _tourRepository = tourRepository;
    }

    public async Task HandleAsync(TransportAvailabilityChanged @event, CancellationToken cancellationToken = default)
    {
        var tours = await _tourRepository.GetAllByTransportId(@event.TransportId);
        var toursToUpdate = new List<Tour>();
        foreach (var tour in tours)
        {
            if (tour.TransportToResource.TransportResourceId == @event.TransportId)
            {
                tour.TransportToResource.ReservedSeatNumber = @event.Amount;
            }
            else if (tour.TransportBackResource.TransportResourceId == @event.TransportId)
            {
                tour.TransportBackResource.ReservedSeatNumber = @event.Amount;
            }

            toursToUpdate.Add(tour);
        }

        await _tourRepository.UpdateManyAsync(toursToUpdate);
    }
}