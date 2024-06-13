using Statistics.Core.Events;
using Statistics.Infrastructure.Mongo.Documents;
using System;

namespace Reservations.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static StatisticAmountChangeDocument AsDocument(this StatisticAmountChange @event)
        {
            return new StatisticAmountChangeDocument
            {
                Id = Guid.NewGuid(),
                Destination = @event.Destination,
                HotelId = @event.HotelId,
                StatisticType = @event.StatisticType,
                TransportBackId = @event.TransportBackId,
                TransportToId = @event.TransportToId,
                Rooms = @event.Rooms,
                Amount = @event.Amount,
            };
        }
    }
}
