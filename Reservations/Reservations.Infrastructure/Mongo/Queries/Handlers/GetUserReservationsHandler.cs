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
    internal sealed class GetUserReservationsHandler : IQueryHandler<GetUserReservations, IEnumerable<ReservationDto>>
    {
        private readonly IMongoDatabase _database;

        public GetUserReservationsHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<ReservationDto>> HandleAsync(GetUserReservations query, CancellationToken cancellationToken)
        {
            var documents = await _database.GetCollection<ReservationDocument>("reservations")
                .Find(r => r.CustomerId == query.CustomerId)
                .ToListAsync();

            return documents.Select(d => d?.AsDto());
        }
    }
}
