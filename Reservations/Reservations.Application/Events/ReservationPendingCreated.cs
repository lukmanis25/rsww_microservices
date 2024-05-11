using Convey.CQRS.Events;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events
{
    [Contract]
    public class ReservationPendingCreated : IEvent
    {
        public Guid ReservationId { get; set; }
        public HotelRoomReservation HotelRoom { get; set; }
        public ResourceReservation TravelTo { get; set; }
        public ResourceReservation TravelBack { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
