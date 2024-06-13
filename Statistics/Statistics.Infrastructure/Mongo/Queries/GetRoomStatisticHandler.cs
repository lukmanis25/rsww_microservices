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
    internal sealed class GetRoomStatisticHandler : IQueryHandler<GetRoomStatistic, IEnumerable<RoomStatisticDto>>
    {
        private readonly IMongoDatabase _database;

        public GetRoomStatisticHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<RoomStatisticDto>> HandleAsync(GetRoomStatistic query, CancellationToken cancellationToken = default)
        {
            var events = await _database.GetCollection<StatisticAmountChangeDocument>("statistic_events")
                .Find(r => r.StatisticType == query.StatisticType)
                .ToListAsync();

            var groupedEvents = events
                .SelectMany(e => e.Rooms.Select(r => new
                {
                    RoomType = r.Type,
                    TotalAmount = r.Amount * e.Amount
                }))
                .GroupBy(r => r.RoomType)
                .Select(g => new RoomStatisticDto
                {
                    RoomType = g.Key.RoomTypeAsString(),
                    StatisticType = query.StatisticType.StatisticTypeAsString(),
                    Amount = g.Sum(x => x.TotalAmount)
                })
                .ToList();

            return groupedEvents;
        }
    } 
}
