using Convey.CQRS.Events;
using Reservations.Application.Services;
using Reservations.Core.Entities;
using Reservations.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Events.External.Handlers
{
    public class PurchaseCancelledHandler : IEventHandler<PurchaseCancelled>
    {
        private readonly IReservationRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public PurchaseCancelledHandler(IReservationRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(PurchaseCancelled @event, CancellationToken cancellationToken = default)
        {
            var reservation = await _repository.GetAsync(@event.ReservationId);

            if (reservation == null || reservation.IsCancelled() || reservation.IsPurchased())
                return;

            reservation.CancelReservation();

            var ifSucceed = await _repository.UpdateAsync(reservation);
            if (ifSucceed)
            {
                await _messageBroker.PublishAsync(new ReservationCancelled
                {
                    ReservationId = reservation.Id,
                    TravelTo = new TransportEventDto { TransportId = reservation.TravelTo.ResourceId },
                    TravelBack = new TransportEventDto { TransportId = reservation.TravelBack.ResourceId },
                    HotelRoom = new HotelRoomEventDto
                    {
                        HotelId = reservation.HotelRoom.ResourceId,
                        Rooms = reservation.HotelRoom.Rooms,
                    },
                    NumberOfPeople = reservation.GetNumberOfPeople()
                });
            }
        }
    }
}
