using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tours.Application.Events;

[Message("transport")]
public class TransportChanged : IEvent
{
    public Guid TransportId { get; set; }
    public float Price { get; set; }
    public int NumberOf { get; set; }
}
