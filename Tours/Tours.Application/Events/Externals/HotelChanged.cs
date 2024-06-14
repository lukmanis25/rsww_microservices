using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Application.Queries;

namespace Tours.Application.Events;

[Message("hotel")]
public class HotelChanged : IEvent
{
    public Guid HotelId { get; set; } // part of key
    public RoomType Type { get; set; } // part of key
    public int Capacity { get; set; } // parth of key
    public int NumberOf { get; set; } // +/- amount
    public float Price { get; set; } // New price
}