using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Transports.Application.Services;
using Transports.Core.Entities;
using Transports.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transports.Application.Events
{
    public class ReservationCancelledHandler : IEventHandler<ReservationCancelled>
    {
        private readonly ITransportEventRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public ReservationCancelledHandler(ITransportEventRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(ReservationCancelled @event, CancellationToken cancellationToken = default)
        {
            //transport to
            await _repository.AddEvent(new Core.Events.TransportAmountChange
            {
                TransportId = @event.TransportTo.TransportId,
                Amount = @event.NumberOfPeople
            });

            await _messageBroker.PublishAsync(new TransportAvailabilityChanged
            {
                TransportId = @event.TransportTo.TransportId,
                Amount = @event.NumberOfPeople
            });

            //transport back
            await _repository.AddEvent(new Core.Events.TransportAmountChange
            {
                TransportId = @event.TransportBack.TransportId,
                Amount = @event.NumberOfPeople
            });

            await _messageBroker.PublishAsync(new TransportAvailabilityChanged
            {
                TransportId = @event.TransportBack.TransportId,
                Amount = @event.NumberOfPeople
            });
        }
    }
}
