using Convey.CQRS.Events;
using Payments.Application.Events.Externals;
using Payments.Application.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payments.Application.Events
{
    public class PurchasePendingCreatedHandler : IEventHandler<PurchasePendingCreated>
    {
        private readonly IMessageBroker _messageBroker;

        public PurchasePendingCreatedHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(PurchasePendingCreated @event, CancellationToken cancellationToken = default)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 2);
            if (randomNumber == 0)
            {
                await _messageBroker.PublishAsync(new PaymentFailed
                {
                    PurchaseId = @event.PurchaseId,
                });
            }
            else
            {
                await _messageBroker.PublishAsync(new PaymentCompleted
                {
                    PurchaseId = @event.PurchaseId,
                });
            }
        }
    }
}
