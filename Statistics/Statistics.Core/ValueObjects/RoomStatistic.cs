using Statistics.Core.Entities;
using System;

namespace Statistics.Core.ValueObjects
{
    public class RoomStatistic
    {
        public RoomType RoomType { get; set; }
        public StatisticType StatisticType { get; set; }
        public int Amount { get; set; }


        public RoomStatistic(
                RoomType roomType,
                StatisticType statisticType,
                int amount
            )
        {
            RoomType = roomType;
            StatisticType = statisticType;
            Amount = amount;
        }
    }
}
