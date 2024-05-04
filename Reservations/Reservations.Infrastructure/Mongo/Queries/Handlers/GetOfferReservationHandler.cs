using Convey.CQRS.Queries;
using MongoDB.Driver;
using Reservations.Application.DTO;
using Reservations.Application.Queries;
using Reservations.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Infrastructure.Mongo.Queries.Handlers
{
    internal sealed class GetOfferReservationHandler : IQueryHandler<GetOfferReservation, OfferReservationDto>
    {
        private readonly IMongoDatabase _database;

        public GetOfferReservationHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<OfferReservationDto> HandleAsync(GetOfferReservation query, CancellationToken cancellationToken = default)
        {
            var document = await _database.GetCollection<OfferReservationDocument>("reservations")
                .Find(r => r.OffertId == query.OffertId)
                .SingleOrDefaultAsync();


            return document?.AsDto();
        }
    }
}
