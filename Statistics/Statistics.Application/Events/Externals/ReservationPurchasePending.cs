using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Statistics.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Events
{
    [Message("reservation")]
    public class ReservationPurchased : IEvent
    {
        public string Destination { get; set; }
        public Guid HotelId { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public Guid TransportToId { get; set; }
        public Guid TransportBackId { get; set; }
    }
}
