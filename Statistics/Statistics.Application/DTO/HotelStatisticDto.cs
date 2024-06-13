using Statistics.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.DTO
{
    public class HotelStatisticDto
    {
        public string HotelName { get; set; }
        public string StatisticType { get; set; }
        public int Amount { get; set; }
    }
}
