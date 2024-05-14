using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Purchases.Application.Events.Externals;
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
    public class ReservationCancelledHandler : IEventHandler<ReservationCancelled>
    {
        private readonly IPurchaseRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public ReservationCancelledHandler(IPurchaseRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(ReservationCancelled @event, CancellationToken cancellationToken = default)
        {
            var purchase = await _repository.GetByReservationAsync(@event.ReservationId);
            if (purchase == null || purchase.PaymentStatus == PaymentStatus.Failed) 
                return;

            purchase.CancelPayment();
            await _repository.UpdateAsync(purchase);
            await _messageBroker.PublishAsync(new PurchaseCancelled
            {
                ReservationId = purchase.ReservationId,
                PurchaseId = purchase.Id
            });
        }
    }
}
