using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.DTO
{
    public class OfferReservationDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OffertId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public HotelRoomsReservation HotelRooms { get; set; }
        public ResourceReservation TravelTo { get; set; }
        public ResourceReservation TravelBack { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
