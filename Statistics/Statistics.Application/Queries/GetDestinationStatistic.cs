using Convey.CQRS.Queries;
using Statistics.Application.DTO;
using Statistics.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Queries
{
    public class GetDestinationStatistic : IQuery<IEnumerable<DestinationStatisticDto>>
    {
        public StatisticType StatisticType { get; set; }
    }
}
