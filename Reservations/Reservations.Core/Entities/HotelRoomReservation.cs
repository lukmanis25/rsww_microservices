using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservations.Core.ValueObjects;

namespace Reservations.Core.Entities
{
    public enum MealType
    {
        Regular,
        AllInclusive
    }
    public class HotelRoomReservation : ResourceReservation
    {
        public IEnumerable<Room> Rooms { get; set; }
        public MealType MealType { get; set; }
    }
}
