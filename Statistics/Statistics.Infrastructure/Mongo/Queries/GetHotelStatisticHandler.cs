using Convey.CQRS.Queries;
using MongoDB.Driver;
using Statistics.Application.DTO;
using Statistics.Application.Queries;
using Statistics.Core;
using Statistics.Core.ValueObjects;
using Statistics.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Statistics.Infrastructure.Mongo.Queries
{
    internal sealed class GetHotelStatisticHandler : IQueryHandler<GetHotelStatistic, IEnumerable<HotelStatisticDto>>
    {
        private readonly IMongoDatabase _database;

        public GetHotelStatisticHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<HotelStatisticDto>> HandleAsync(GetHotelStatistic query, CancellationToken cancellationToken = default)
        {
            var events = await _database.GetCollection<StatisticAmountChangeDocument>("statistic_events")
                .Find(r => r.StatisticType == query.StatisticType)
                .ToListAsync();

            var groupedEvents = events
                .GroupBy(e => e.HotelId) //TODO dodać join 
                .Select(g => new HotelStatisticDto
                {
                    HotelName = g.Key.ToString(),
                    StatisticType = query.StatisticType.StatisticTypeAsString(),
                    Amount = g.Sum(e => e.Amount)
                })
                .ToList();

            return groupedEvents;
        }
    } 
}
