using Hotels.Core.Events;
using Hotels.Infrastructure.Mongo.Documents;

namespace Reservations.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static HotelRoomAmountChangeDocument AsDocument(this HotelRoomAmountChange @event)
        {
            return new HotelRoomAmountChangeDocument
            {
                Id = @event.HotelId,
                Room = @event.Room,
            };
        }
    }
}
