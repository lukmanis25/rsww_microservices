using Convey.CQRS.Events;
using Reservations.Application.Services;
using Reservations.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Events.External.Handlers
{
    public class PurchaseCompletedHandler : IEventHandler<PurchaseCompleted>
    {
        private readonly IReservationRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public PurchaseCompletedHandler(IReservationRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(PurchaseCompleted @event, CancellationToken cancellationToken = default)
        {
            var reservation = await _repository.GetAsync(@event.ReservationId);

            if (reservation == null || reservation.IsCancelled())
                return;

            reservation.PurchaseReservation();

            await _repository.UpdateAsync(reservation);
        }
    }
}
