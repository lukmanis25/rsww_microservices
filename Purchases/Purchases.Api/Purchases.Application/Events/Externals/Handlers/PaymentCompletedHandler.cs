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
    public class PaymentCompletedHandler : IEventHandler<PaymentCompleted>
    {
        private readonly IPurchaseRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public PaymentCompletedHandler(IPurchaseRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(PaymentCompleted @event, CancellationToken cancellationToken = default)
        {
            var purchase = await _repository.GetAsync(@event.PurchaseId);
            purchase.FinishPayment(PaymentStatus.Accepted);
            if (purchase.PaymentStatus == PaymentStatus.Cancelled)
            {
                return;
            }

            await _repository.UpdateAsync(purchase);

            await _messageBroker.PublishAsync(new PurchaseCompleted 
            { 
                PurchaseId = purchase.Id, 
                ReservationId = purchase.ReservationId 
            });
        }
    }
}
