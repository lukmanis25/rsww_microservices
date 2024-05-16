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
    public class HotelAvailabilityChanged : IEvent
    {
        public Guid HotelId { get; set; }
        public int Capacity { get; set; }
        public RoomType Type { get; set; }
        public int Amount { get; set; }
    }
}
