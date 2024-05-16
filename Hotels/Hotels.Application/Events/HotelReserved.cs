using Convey.CQRS.Events;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Application.Events
{
    [Contract]
    public class HotelReserved : IEvent
    {
        public Guid HotelId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
