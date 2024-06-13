using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using Statistics.Core.Entities;
using Statistics.Core.Repositories;
using Statistics.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Statistics.Core.Events;
using Reservations.Infrastructure.Mongo.Documents;
using Statistics.Core.ValueObjects;

namespace Statistics.Infrastructure.Mongo.Repositories
{
    internal sealed class StatisticEventMongoRepository : IStatisticEventRepository
    {
        private readonly IMongoRepository<StatisticAmountChangeDocument, Guid> _repository;
        private readonly IMongoDatabase _database;

        public StatisticEventMongoRepository(IMongoRepository<StatisticAmountChangeDocument, Guid> repository, IMongoDatabase database)
        {
            _repository = repository;
            _database = database;
        }
            

        public Task AddEvent(StatisticAmountChange @event) =>
            _repository.AddAsync(@event.AsDocument());

    }
}
