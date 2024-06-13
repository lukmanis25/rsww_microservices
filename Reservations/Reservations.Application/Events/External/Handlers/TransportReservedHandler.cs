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
    public class TransportReservedHandler : IEventHandler<TransportReserved>
    {
        private readonly IReservationRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public TransportReservedHandler(IReservationRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(TransportReserved @event, CancellationToken cancellationToken = default)
        {
            var attemps = 3;
            while (attemps > 0)
            {
                var reservation = await _repository.GetAsync(@event.ReservationId);

                if (reservation == null || reservation.IsCancelled() || reservation.IsPurchased())
                    return;

                reservation.ReserveTransport(@event.TransportId);

                var ifSucceed = await _repository.UpdateAsync(reservation);
                if (ifSucceed)
                {
                    if(reservation.AreAllResourceReserved())
                    {
                        await _messageBroker.PublishAsync(new ReservationPurchasePending
                        {
                            ReservationId = reservation.Id,
                            CustomerId = reservation.CustomerId,
                            ReservedUntil = reservation.CreationDateTime.AddMinutes(1),
                            TotalPrice = reservation.TotalPrice,
                            Destination = reservation.Tour.TourDestination,
                            HotelId = reservation.HotelRoom.ResourceId,
                            Rooms = reservation.HotelRoom.Rooms,
                            TransportToId = reservation.TransportTo.ResourceId,
                            TransportBackId = reservation.TransportBack.ResourceId
                        });
                    }
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
                attemps--;
            }       
        }
    }
}
