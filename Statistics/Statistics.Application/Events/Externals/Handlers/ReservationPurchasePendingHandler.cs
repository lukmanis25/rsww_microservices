using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Statistics.Application.Services;
using Statistics.Core.Entities;
using Statistics.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Statistics.Application.Events
{
    public class ReservationPurchasePendingHandler : IEventHandler<ReservationPurchasePending>
    {
        private readonly IStatisticEventRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public ReservationPurchasePendingHandler(IStatisticEventRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(ReservationPurchasePending @event, CancellationToken cancellationToken = default)
        {
            await _repository.AddEvent(new Core.Events.StatisticAmountChange
            {
                Destination = @event.Destination,
                StatisticType = Core.ValueObjects.StatisticType.Reservations,
                HotelId = @event.HotelId,
                Rooms = @event.Rooms,
                TransportToId = @event.TransportToId,
                TransportBackId = @event.TransportBackId,
                Amount = 1
            });      
        }
    }
}
