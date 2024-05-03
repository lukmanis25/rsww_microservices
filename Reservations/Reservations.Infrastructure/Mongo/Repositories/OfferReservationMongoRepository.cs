using Convey.Persistence.MongoDB;
using Reservations.Core.Entities;
using Reservations.Core.Repositories;
using Reservations.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Infrastructure.Mongo.Repositories
{
    internal sealed class OfferReservationMongoRepository : IOfferReservationRepository
    {
        private readonly IMongoRepository<OfferReservationDocument, Guid> _repository;

        public OfferReservationMongoRepository(IMongoRepository<OfferReservationDocument, Guid> repository)
            => _repository = repository;
        public Task AddAsync(OfferReservation offerReservation) => 
            _repository.AddAsync(offerReservation.AsDocument());

        public Task DeleteAsync(AggregateId id)
         => _repository.DeleteAsync(id);

        public Task<bool> ExistsAsync(AggregateId id)
            => _repository.ExistsAsync(r => r.Id == id);

        public async Task<OfferReservation> GetAsync(AggregateId id)
        {
            var document = await _repository.GetAsync(r => r.Id == id);
            return document?.AsEntity();
        }

        public Task UpdateAsync(OfferReservation offerReservation)
        {
            throw new NotImplementedException();
        }
    }
}
