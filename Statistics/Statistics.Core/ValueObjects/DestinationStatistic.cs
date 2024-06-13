using System;
using Statistics.Core.Entities;

namespace Statistics.Core.ValueObjects
{
    public class DestinationStatistic
    {
        public string Destination { get; set; }
        public StatisticType StatisticType { get; set; }
        public int Amount { get; set; }


        public DestinationStatistic(
                string destination,
                StatisticType statisticType,
                int amount
            )
        {
            Destination = destination;
            StatisticType = statisticType;
            Amount = amount;
        }
    }
}
