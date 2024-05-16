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
    public class HotelReservationRejectedHandler : IEventHandler<HotelReservationRejected>
    {
        private readonly IReservationRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public HotelReservationRejectedHandler(IReservationRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(HotelReservationRejected @event, CancellationToken cancellationToken = default)
        {
            var attemps = 3;
            while (attemps > 0)
            {
                var reservation = await _repository.GetAsync(@event.ReservationId);

                if (reservation == null || reservation.IsCancelled())
                    return;

                reservation.CancelReservation();

                var ifSucceed = await _repository.UpdateAsync(reservation);
                if (ifSucceed)
                {
                    await _messageBroker.PublishAsync(new ReservationCancelled
                    {
                        ReservationId = reservation.Id,
                        TransportTo = new TransportEventDto { TransportId = reservation.TransportTo.ResourceId },
                        TransportBack = new TransportEventDto { TransportId = reservation.TransportBack.ResourceId },
                        HotelRoom = new HotelRoomEventDto
                        {
                            HotelId = reservation.HotelRoom.ResourceId,
                            Rooms = reservation.HotelRoom.Rooms,
                        },
                        NumberOfPeople = reservation.GetNumberOfPeople()
                    });
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
                attemps--;
            }       
        }
    }
}
