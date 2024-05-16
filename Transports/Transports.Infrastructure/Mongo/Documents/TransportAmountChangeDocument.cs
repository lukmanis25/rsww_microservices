using Convey.Types;
using System;

namespace Transports.Infrastructure.Mongo.Documents
{
    internal sealed class TransportAmountChangeDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid TransportId { get; set; }
        public int Amount { get; set; }
    }
}
