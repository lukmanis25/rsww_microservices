using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.ValueObjects
{
    public enum RoomType
    {
        Small,
        Medium,
        Large,
        Apartment,
        Studio
    }
    public class Room : IEquatable<Room>
    {
        public int Occupancy { get; set; }
        public RoomType Type { get; set; }

        public bool Equals(Room reservation)
            => Occupancy.Equals(reservation.Occupancy) && Type.Equals(reservation.Type);

        public override bool Equals(object obj)
            => obj is Room reservation && Equals(reservation);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Occupancy.GetHashCode();
                hash = hash * 23 + Type.GetHashCode();
                return hash;
            }
        }
    }
}
