using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using Purchases.Core.Entities;
using Purchases.Core.Purchases;
using Purchases.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Infrastructure.Mongo.Repositories
{
    internal sealed class PurchaseMongoRepository : IPurchaseRepository
    {
        private readonly IMongoRepository<PurchaseDocument, Guid> _repository;

        public PurchaseMongoRepository(IMongoRepository<PurchaseDocument, Guid> repository)
            => _repository = repository;

        public Task AddAsync(Purchase purchase) =>
            _repository.AddAsync(purchase.AsDocument());

        public Task DeleteAsync(AggregateId id) => _repository.DeleteAsync(id);

        public async Task<Purchase> GetByReservationAsync(Guid reservationId)
        {
            var document = await _repository.GetAsync(r => r.ReservationId == reservationId);
            return document?.AsEntity();
        }

        public async Task<Purchase> GetAsync(AggregateId id)
        {
            var document = await _repository.GetAsync(r => r.Id == id);
            return document?.AsEntity();
        }

        public async Task<bool> UpdateAsync(Purchase purchase)
        {
            var result = await _repository.Collection.ReplaceOneAsync(
                r => r.Id == purchase.Id && r.Version < purchase.Version,
                purchase.AsDocument());

            return result.ModifiedCount > 0;
        }

        public Task<bool> ExistsByReservationAsync(Guid reservationId)
        => _repository.ExistsAsync(r => r.ReservationId == reservationId);

        public Task<bool> ExistsAsync(AggregateId id)
        => _repository.ExistsAsync(r => r.Id == id);

        //=> _repository.Collection.ReplaceOneAsync(r => r.Id == purchase.Id && r.Version < purchase.Version,
        //        purchase.AsDocument());

        //{
        //    var filter = Builders<PurchaseDocument>.Filter.And(
        //        Builders<PurchaseDocument>.Filter.Eq(r => r.Id, purchase.Id.Value),
        //        Builders<PurchaseDocument>.Filter.Lt(r => r.Version, purchase.Version)
        //    );

        //    return _repository.Collection.ReplaceOneAsync(filter, purchase.AsDocument());
        //}
    }
}
