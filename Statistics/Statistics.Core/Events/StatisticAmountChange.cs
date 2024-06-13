using Statistics.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace Statistics.Core.Events
{
    public class StatisticAmountChange
    {
        public StatisticType StatisticType { get; set; }
        public string Destination { get; set; }
        public Guid HotelId { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public Guid TransportToId { get; set; }
        public Guid TransportBackId { get; set; }
        public int Amount { get; set; }
    }
}
