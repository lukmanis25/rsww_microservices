using Convey.CQRS.Events;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.Rejected
{
    [Contract]
    public class ReservationCancelled : IRejectedEvent
    {
        public Guid ReservationId { get; set; }
        public HotelRoomReservation HotelRoom { get; set; }
        public ResourceReservation TravelTo { get; set; }
        public ResourceReservation TravelBack { get; set; }
        public int NumberOfPeople { get; set; }
        public string Reason { get;  }
        public string Code { get;  }
    }
}
