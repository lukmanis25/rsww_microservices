
using Statistics.Core.Exceptions;
using Statistics.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Core
{
    public static class EnumExtensions
    {
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

        public static string StatisticTypeAsString(this StatisticType roomType)
        {
            return Enum.GetName(typeof(StatisticType), roomType);
        }

        public static StatisticType StringAsStatisticType(this string roomType)
        {
            if (Enum.TryParse(typeof(StatisticType), roomType, out object result))
            {
                return (StatisticType)result;
            }
            else
            {
                throw new InvalidRoomTypeException();
            }
        }
    }
}
