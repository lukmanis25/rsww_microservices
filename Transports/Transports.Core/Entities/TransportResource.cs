using System;

namespace Transports.Core.Entities
{
    public enum PaymentStatus
    {
        Pending,
        Failed,
        Accepted,
        Cancelled
    }
    public class TransportResource : AggregateRoot
    {
        public int Amount { get; set; }


        public TransportResource(
                Guid id,
                int amount,
                int version = 0
            )
        {
            Id = id;
            Amount = amount;
            Version = version;
        }

        public bool IfTransportCanBeReserved(int amount)
        {
            if (Amount - amount < 0)
            {
                return false;
            }
            return true;
        }
    }
}
