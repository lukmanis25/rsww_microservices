using Convey.CQRS.Events;
using Reservations.Core.Entities;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events
{
    [Contract]
    public class ReservationPurchased : IEvent
    {
        public Guid ReservationId { get; set; }
        public string Destination { get; set; }
        public Guid HotelId { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public Guid TransportToId { get; set; }
        public Guid TransportBackId { get; set; }
    }
}
