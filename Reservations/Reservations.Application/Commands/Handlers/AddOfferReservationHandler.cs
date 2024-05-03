using Convey.CQRS.Commands;
using Reservations.Application.Exceptions;
using Reservations.Core.Entities;
using Reservations.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Commands.Handlers
{
    public class AddOfferReservationHandler : ICommandHandler<AddOfferReservation>
    {
        //private readonly IOfferReservationRepository _repository;

        //public AddOfferReservationHandler(IOfferReservationRepository repository)
        //{
        //    _repository = repository;
        //}
        public async Task HandleAsync(AddOfferReservation command, CancellationToken cancellationToken = default)
        {
            var reservation = OfferReservation.Create(
                customerId: command.CustomerId,
                offerId: command.OfferId,
                numberOfAdults: command.NumberOfAdults,
                numberOfChildren: command.NumberOfChildren,
                hotelId: command.HotelId,
                rooms: command.Rooms,
                travelToId: command.TravelToId,
                travelBackId: command.TravelBackId
                );
            var x = 0;
            //await _repository.AddAsync(reservation);
        }
    }
}
