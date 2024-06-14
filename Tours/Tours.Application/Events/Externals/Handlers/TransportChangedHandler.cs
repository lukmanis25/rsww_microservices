using Convey.CQRS.Events;
using Tours.Application.Services;
using Tours.Core.Entities;

namespace Tours.Application.Events;

public class TransportChangedHandler : IEventHandler<TransportChanged>
{
    private readonly IMessageBroker _messageBroker;
    private readonly ITourRepository _tourRepository;

    public TransportChangedHandler(IMessageBroker messageBroker, ITourRepository tourRepository)
    {
        _messageBroker = messageBroker;
        _tourRepository = tourRepository;
    }

    public async Task HandleAsync(TransportChanged @event, CancellationToken cancellationToken = default)
    {
        var tour = await _tourRepository.GetAllByTransportId(@event.TransportId);
        var toursToUpdate = new List<Tour>();
        foreach (Tour t in tour)
        {
            // check transport to
            if (t.TransportToResource.TransportResourceId == @event.TransportId)
            {
                t.TransportToResource.Price = @event.Price;
                t.TransportToResource.SeatNumber = @event.NumberOf;
            }
            // check transport from
            if (t.TransportBackResource.TransportResourceId == @event.TransportId)
            {
                t.TransportBackResource.Price = @event.Price;
                t.TransportBackResource.SeatNumber = @event.NumberOf;
            }

            toursToUpdate.Add(t);
        }

        await _tourRepository.UpdateManyAsync(toursToUpdate);
    }
  }