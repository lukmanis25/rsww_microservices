using Transports.Core.Events;
using Transports.Infrastructure.Mongo.Documents;
using System;

namespace Reservations.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static TransportAmountChangeDocument AsDocument(this TransportAmountChange @event)
        {
            return new TransportAmountChangeDocument
            {
                Id = Guid.NewGuid(),
                TransportId = @event.TransportId,
                Amount = @event.Amount,
            };
        }
    }
}
