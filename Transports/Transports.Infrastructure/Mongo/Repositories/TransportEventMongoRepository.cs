using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using Transports.Core.Entities;
using Transports.Core.Repositories;
using Transports.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transports.Core.Events;
using Reservations.Infrastructure.Mongo.Documents;

namespace Transports.Infrastructure.Mongo.Repositories
{
    internal sealed class TransportEventMongoRepository : ITransportEventRepository
    {
        private readonly IMongoRepository<TransportAmountChangeDocument, Guid> _repository;
        private readonly IMongoDatabase _database;

        public TransportEventMongoRepository(IMongoRepository<TransportAmountChangeDocument, Guid> repository, IMongoDatabase database)
        {
            _repository = repository;
            _database = database;
        }
            

        public Task AddEvent(TransportAmountChange @event) =>
            _repository.AddAsync(@event.AsDocument());
    

        public async Task<TransportResource> GetTransportResource(AggregateId transportId)
        {
            var x = await _database.GetCollection<TransportAmountChangeDocument>("transport_events").Find(r => true)
                .ToListAsync(); 
            

            var events = await _database.GetCollection<TransportAmountChangeDocument>("transport_events")
                .Find(r => r.TransportId == transportId)
                .ToListAsync();

            var totalAmount = 0;

            foreach (var @event in events)
            {
                totalAmount += @event.Amount;
            }
            return new TransportResource(transportId, totalAmount);
        }
    }
}
