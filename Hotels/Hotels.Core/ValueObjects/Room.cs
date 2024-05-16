using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Core.ValueObjects
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
        public int Capacity { get; set; }
        public RoomType Type { get; set; }
        public int Amount { get; set; }

        public bool Equals(Room other)
        {
            if (other == null) return false;
            return (this.Type == other.Type && this.Capacity == other.Capacity);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Room roomObj = obj as Room;
            if (roomObj == null) return false;
            else return Equals(roomObj);
        }

        public override int GetHashCode()
        {
            return (this.Type.GetHashCode() * 397) ^ this.Capacity;
        }

        public static bool operator ==(Room room1, Room room2)
        {
            if (ReferenceEquals(room1, room2)) return true;
            if (ReferenceEquals(room1, null) || ReferenceEquals(room2, null)) return false;
            return room1.Type == room2.Type && room1.Capacity == room2.Capacity;
        }

        public static bool operator !=(Room room1, Room room2)
        {
            return !(room1 == room2);
        }
    }
}
