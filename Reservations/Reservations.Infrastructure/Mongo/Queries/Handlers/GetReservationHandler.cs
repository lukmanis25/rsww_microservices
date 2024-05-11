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
    internal sealed class GetReservationHandler : IQueryHandler<GetReservation, ReservationDto>
    {
        private readonly IMongoDatabase _database;

        public GetReservationHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<ReservationDto> HandleAsync(GetReservation query, CancellationToken cancellationToken = default)
        {
            var document = await _database.GetCollection<ReservationDocument>("reservations")
                .Find(r => r.Id == query.ReservationId)
                .SingleOrDefaultAsync();


            return document?.AsDto();
        }
    }
}
