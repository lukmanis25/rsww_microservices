using Convey.CQRS.Commands;
using Reservations.Application.Events;
using Reservations.Application.Exceptions;
using Reservations.Application.Services;
using Reservations.Core.Entities;
using Reservations.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Commands.Handlers
{
    public class AddOfferReservationHandler : ICommandHandler<AddOfferReservation>
    {
        private readonly IReservationRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public AddOfferReservationHandler(IReservationRepository repository, IMessageBroker messageBroker )
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(AddOfferReservation command, CancellationToken cancellationToken = default)
        {
            var reservation = Reservation.Create(
                customerId: command.CustomerId,
                offerId: command.OfferId,
                numberOfAdults: command.NumberOfAdults,
                numberOfChildren: command.NumberOfChildren,
                hotelId: command.HotelId,
                rooms: command.Rooms,
                travelToId: command.TravelToId,
                travelBackId: command.TravelBackId
                );
            await _repository.AddAsync(reservation);
            await _messageBroker.PublishAsync(new OffertReservated(reservation.OffertId, reservation.CustomerId));
        }
    }
}
