﻿using Convey.CQRS.Events;
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
    public class ReservationPendingCreated : IEvent
    {
        public Guid ReservationId { get; set; }
        public HotelRoomEventDto HotelRoom { get; set; }
        public TransportEventDto TransportTo { get; set; }
        public TransportEventDto TransportBack { get; set; }
        public int NumberOfPeople { get; set; }
    }
    public class TransportEventDto
    {
        public Guid TransportId { get; set; }
    }

    public class HotelRoomEventDto
    {
        public Guid HotelId { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}
