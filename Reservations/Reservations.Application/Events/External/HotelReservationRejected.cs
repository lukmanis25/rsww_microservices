using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.External
{
    [Message("hotel")]
    public class HotelReservationRejected : IEvent
    {
        public Guid HotelId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
