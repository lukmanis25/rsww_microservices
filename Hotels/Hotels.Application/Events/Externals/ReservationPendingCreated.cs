using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Application.Events
{
    [Message("reservation")]
    public class ReservationPendingCreated : IEvent
    {
        public Guid ReservationId { get; set; }
        public HotelRoomEventDto HotelRoom { get; set; }
        public int NumberOfPeople { get; set; }
    }
    public class HotelRoomEventDto
    {
        public Guid HotelId { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}
