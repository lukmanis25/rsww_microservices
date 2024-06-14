using System;

namespace Transports.Core.Events
{
    public class TransportAmountChange
    {
        public Guid TransportId { get; set; }
        public int Amount { get; set; }
    }
}
