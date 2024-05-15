using Convey.CQRS.Commands;
using Reservations.Application.Events;
using Reservations.Application.Exceptions;
using Reservations.Application.Services;
using Reservations.Core.Entities;
using Reservations.Core.Repositories;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Commands.Handlers
{
    public class CancelReservationHandler : ICommandHandler<CancelReservation>
    {
        private readonly IReservationRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public CancelReservationHandler(IReservationRepository repository, IMessageBroker messageBroker )
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(CancelReservation command, CancellationToken cancellationToken = default)
        {
            var reservation = await _repository.GetAsync(command.ReservationId);
            if (reservation.IsPurchased())
            {
                throw new CancelPurchasedReservationException(reservation.Id);
            }
            if (reservation.IsCancelled())
            {
                throw new ReservationAlreadyCancelledException(reservation.Id);
            }

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

        }
    }
}
