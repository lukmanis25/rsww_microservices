using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Purchases.Application.Services;
using Purchases.Core.Entities;
using Purchases.Core.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Purchases.Application.Events
{
    public class ReservationPurchasePendingHandler : IEventHandler<ReservationPurchasePending>
    {
        private readonly IPurchaseRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public ReservationPurchasePendingHandler(IPurchaseRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(ReservationPurchasePending @event, CancellationToken cancellationToken = default)
        {
            var ifExist = await _repository.ExistsByReservationAsync(@event.ReservationId);
            if (ifExist || @event.ReservedUntil < DateTime.UtcNow) 
                return;

            var purchase = Purchase.Create(
                    customerId: @event.CustomerId,
                    reservationId: @event.ReservationId,
                    price: @event.TotalPrice
                );
            await _repository.AddAsync( purchase );
        }
    }
}
