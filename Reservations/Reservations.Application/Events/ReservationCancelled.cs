﻿using Convey.CQRS.Events;
using Reservations.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events
{
    [Contract]
    public class ReservationCancelled : IEvent
    {
        public Guid ReservationId { get; set; }
        public HotelRoomEventDto HotelRoom { get; set; }
        public TransportEventDto TransportTo { get; set; }
        public TransportEventDto TransportBack { get; set; }
        public int NumberOfPeople { get; set; }

    }
}
