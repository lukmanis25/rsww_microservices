using Convey.CQRS.Commands;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Commands
{
    public class AddOfferReservation : ICommand
    {
        public Guid ReservationId { get; set; }
        public Guid CustomerId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public Guid HotelId { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public Guid? TravelToId { get; set; }
        public Guid? TravelBackId { get; set; }

        public AddOfferReservation(Guid reservationId, Guid customerId, int numberOfAdults, int numberOfChildren,
                        Guid hotelId, IEnumerable<Room> rooms, Guid? travelToId = null,
                        Guid? travelBackId = null)
        {
            ReservationId = reservationId;
            CustomerId = customerId;
            NumberOfAdults = numberOfAdults;
            NumberOfChildren = numberOfChildren;
            HotelId = hotelId;
            Rooms = rooms;
            TravelToId = travelToId;
            TravelBackId = travelBackId;
        }
    }
}
