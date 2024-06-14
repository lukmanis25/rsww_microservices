using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transports.Application.Events
{
    [Message("reservation")]
    public class ReservationCancelled : IEvent
    {
        public Guid ReservationId { get; set; }
        public TransportEventDto TransportTo { get; set; }
        public TransportEventDto TransportBack { get; set; }
        public int NumberOfPeople { get; set; }

    }
}
