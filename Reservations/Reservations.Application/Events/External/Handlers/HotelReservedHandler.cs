using Convey.CQRS.Events;
using Microsoft.AspNetCore.SignalR;
using Reservations.Application.Services;
using Reservations.Application.SignalRHub;
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
    public class HotelReservedHandler : IEventHandler<HotelReserved>
    {
        private readonly IReservationRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IHubContext<ReservationHub, INotificationsClient> _hubContext;

        public HotelReservedHandler(IReservationRepository repository, IMessageBroker messageBroker, IHubContext<ReservationHub, INotificationsClient> context)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _hubContext = context;
        }
        public async Task HandleAsync(HotelReserved @event, CancellationToken cancellationToken = default)
        {
            var attemps = 3;
            while (attemps > 0)
            {
                var reservation = await _repository.GetAsync(@event.ReservationId);

                if (reservation == null || reservation.IsCancelled() || reservation.IsPurchased())
                    return;

                reservation.ReserveHotel(@event.HotelId);

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
                        await _hubContext.Clients.All.ReceiveNotification($"TourReserved {reservation.Tour.TourId}");
                    }
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
                attemps--;
            }       
        }
    }
}
