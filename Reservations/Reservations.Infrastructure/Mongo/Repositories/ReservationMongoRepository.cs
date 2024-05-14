using Convey.Persistence.MongoDB;
using MongoDB.Driver;
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
    internal sealed class ReservationMongoRepository : IReservationRepository
    {
        private readonly IMongoRepository<ReservationDocument, Guid> _repository;

        public ReservationMongoRepository(IMongoRepository<ReservationDocument, Guid> repository)
            => _repository = repository;
        public Task AddAsync(Reservation reservation) => 
            _repository.AddAsync(reservation.AsDocument());

        public Task DeleteAsync(AggregateId id)
         => _repository.DeleteAsync(id);

        public async Task<Reservation> GetAsync(AggregateId id)
        {
            var document = await _repository.GetAsync(r => r.Id == id);
            return document?.AsEntity();
        }
        public async Task<bool> UpdateAsync(Reservation reservation)
        {
            var result = await _repository.Collection.ReplaceOneAsync(
                r => r.Id == reservation.Id && r.Version < reservation.Version,
                reservation.AsDocument());

            return result.ModifiedCount > 0;
        }
        //public  Task UpdateAsync(Reservation reservation)
        //{
        //    var filter = Builders<ReservationDocument>.Filter.And(
        //        Builders<ReservationDocument>.Filter.Eq(r => r.Id, reservation.Id.Value),
        //        Builders<ReservationDocument>.Filter.Lt(r => r.Version, reservation.Version)
        //    );

        //    return _repository.Collection.ReplaceOneAsync(filter, reservation.AsDocument());
        //}

    }
}
