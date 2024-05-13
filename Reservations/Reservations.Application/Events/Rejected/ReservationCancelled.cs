using Convey.CQRS.Events;
using Reservations.Core.Entities;
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
        public HotelRoomEventDto HotelRoom { get; set; }
        public TransportEventDto TravelTo { get; set; }
        public TransportEventDto TravelBack { get; set; }
        public int NumberOfPeople { get; set; }
        public string Reason { get;  }
        public string Code { get;  }
    }
}
