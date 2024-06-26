﻿using Convey.CQRS.Events;
using Hotels.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Application.Events
{
    [Contract]
    public class HotelReservationRejected : IEvent
    {
        public Guid HotelId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
