using System;
using Statistics.Core.Entities;

namespace Statistics.Core.ValueObjects
{
    public class HotelStatistic
    {
        public string HotelName { get; set; }
        public StatisticType StatisticType { get; set; }
        public int Amount { get; set; }


        public HotelStatistic(
                string hotelName,
                StatisticType statisticType,
                int amount
            )
        {
            HotelName = hotelName;
            StatisticType = statisticType;
            Amount = amount;
        }
    }
}
