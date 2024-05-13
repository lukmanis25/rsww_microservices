using Convey.CQRS.Commands;
using Purchases.Application.Events;
using Purchases.Application.Exceptions;
using Purchases.Application.Services;
using Purchases.Core.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Purchases.Application.Commands.Handlers
{
    public class MakePaymentHandler : ICommandHandler<MakePayment>
    {
        private readonly IPurchaseRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public MakePaymentHandler(IPurchaseRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(MakePayment command, CancellationToken cancellationToken = default)
        {
            var ifExist = await _repository.ExistsByReservationAsync(command.ReservationId);
            if (!ifExist)
            {
                throw new ReservationPurchaseDoesntExistException(command.ReservationId);
            }
            var purchase = await _repository.GetByReservationAsync(command.ReservationId);
            if (purchase.PaymentStatus != null)
            {
                throw new PaymentForReservationAlreadyStartedException(command.ReservationId);
            }

            purchase.StartPayment();

            var ifSucceed = await _repository.UpdateAsync(purchase);
            if (ifSucceed)
            {
                await _messageBroker.PublishAsync(new PurchasePendingCreated
                {
                    PurchaseId = purchase.Id,
                    Price = purchase.Price,
                });
            }
        }
    }
}
