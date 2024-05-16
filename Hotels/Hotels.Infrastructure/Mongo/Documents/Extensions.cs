using Hotels.Core.Events;
using Hotels.Infrastructure.Mongo.Documents;
using System;

namespace Hotels.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static HotelRoomAmountChangeDocument AsDocument(this HotelRoomAmountChange @event)
        {
            return new HotelRoomAmountChangeDocument
            {
                Id = Guid.NewGuid(),
                HotelId = @event.HotelId,
                Room = @event.Room,
            };
        }
    }
}
