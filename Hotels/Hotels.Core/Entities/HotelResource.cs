using Hotels.Core.Events;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Core.Entities
{
    public enum PaymentStatus
    {
        Pending,
        Failed,
        Accepted,
        Cancelled
    }
    public class HotelResource : AggregateRoot
    {
        public IEnumerable<Room> Rooms { get; set; }


        public HotelResource(
                Guid id,
                IEnumerable<Room> rooms,
                int version = 0
            )
        {
            Id = id;
            Rooms = rooms;
            Version = version;
        }

        public bool IfRoomsCanBeReserved(IEnumerable<Room> rooms)
        {
            foreach ( var room in rooms )
            {
                var existingRoom = Rooms.FirstOrDefault(r => r == room);
                if ( existingRoom == null || existingRoom.Amount - room.Amount < 0 )
                {
                    return false;
                }
            }
            return true;
        }

        public int GetRoomAmout(Room room)
        {
            var hotelResourceRoom = Rooms.FirstOrDefault(r => r == room);
            return hotelResourceRoom != null ? hotelResourceRoom.Amount : 0;
        }
    }
}
