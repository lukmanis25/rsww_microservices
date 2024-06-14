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
    public class ReservationPendingCreatedHandler : IEventHandler<ReservationPendingCreated>
    {
        private readonly ITransportEventRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public ReservationPendingCreatedHandler(ITransportEventRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(ReservationPendingCreated @event, CancellationToken cancellationToken = default)
        {
            var transportToResource = await _repository.GetTransportResource(@event.TransportTo.TransportId);
            var transportBackResource = await _repository.GetTransportResource(@event.TransportBack.TransportId);
            //transport to
            await _repository.AddEvent(new Core.Events.TransportAmountChange
            {
                TransportId = @event.TransportTo.TransportId,
                Amount = @event.NumberOfPeople * (-1)
            });

            var transportToResourceAmount = transportToResource != null ? transportToResource.Amount : 0;
            await _messageBroker.PublishAsync(new TransportAvailabilityChanged
            {
                TransportId = @event.TransportTo.TransportId,
                Amount = transportToResourceAmount - @event.NumberOfPeople,
            });

            //transport back
            await _repository.AddEvent(new Core.Events.TransportAmountChange
            {
                TransportId = @event.TransportBack.TransportId,
                Amount = @event.NumberOfPeople * (-1)
            });

            var transportBackResourceAmount = transportBackResource != null ? transportBackResource.Amount : 0;
            await _messageBroker.PublishAsync(new TransportAvailabilityChanged
            {
                TransportId = @event.TransportTo.TransportId,
                Amount = transportBackResourceAmount - @event.NumberOfPeople,
            });


            if (transportToResource == null || transportBackResource == null ||
                !transportToResource.IfTransportCanBeReserved(@event.NumberOfPeople) || 
                !transportBackResource.IfTransportCanBeReserved(@event.NumberOfPeople))
            {
                //tylko jeden zeby nie poszło 2 razy ReservedCanceled od reservations-ms
                await _messageBroker.PublishAsync(new TransportReservationRejected
                {
                    TransportId = @event.TransportTo.TransportId,
                    ReservationId = @event.ReservationId
                });
                return;
            }
            else
            {
                await _messageBroker.PublishAsync(new TransportReserved
                {
                    TransportId = @event.TransportTo.TransportId,
                    ReservationId = @event.ReservationId
                });
                await _messageBroker.PublishAsync(new TransportReserved
                {
                    TransportId = @event.TransportBack.TransportId,
                    ReservationId = @event.ReservationId
                });
            }
            
        }
    }
}
