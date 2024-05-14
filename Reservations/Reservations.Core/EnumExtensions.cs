using Reservations.Core.Entities;
using Reservations.Core.Exceptions;
using Reservations.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core
{
    public static class EnumExtensions
    {
        public static string ReservationStatusAsString(this ReservationStatus status)
        {
            return Enum.GetName(typeof(ReservationStatus), status);
        }

        public static ReservationStatus StringAsReservationStatus(this string statusString)
        {
            if (Enum.TryParse(typeof(ReservationStatus), statusString, out object result))
            {
                return (ReservationStatus)result;
            }
            else
            {
                throw new InvalidReservationStatusException();
            }
        }

        public static string MealTypeAsString(this MealType mealType)
        {
            return Enum.GetName(typeof(MealType), mealType);
        }

        public static MealType StringAsMealType(this string mealType)
        {
            if (Enum.TryParse(typeof(MealType), mealType, out object result))
            {
                return (MealType)result;
            }
            else
            {
                throw new InvalidMealTypeException();
            }
        }

        public static string RoomTypeAsString(this RoomType roomType)
        {
            return Enum.GetName(typeof(RoomType), roomType);
        }

        public static RoomType StringAsRoomType(this string roomType)
        {
            if (Enum.TryParse(typeof(RoomType), roomType, out object result))
            {
                return (RoomType)result;
            }
            else
            {
                throw new InvalidRoomTypeException();
            }
        }
    }
}
