using Convey.CQRS.Queries;
using MongoDB.Driver;
using Purchases.Application.DTO;
using Purchases.Application.Queries;
using Purchases.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Purchases.Infrastructure.Mongo.Queries.Handlers
{
    internal sealed class GetPurchaseByReservationHandler : IQueryHandler<GetPurchaseByReservation, PurchaseDto>
    {
        private readonly IMongoDatabase _database;

        public GetPurchaseByReservationHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<PurchaseDto> HandleAsync(GetPurchaseByReservation query, CancellationToken cancellationToken = default)
        {
            var document = await _database.GetCollection<PurchaseDocument>("purchases")
                .Find(r => r.ReservationId == query.ReservationId)
                .SingleOrDefaultAsync();


            return document?.AsDto();
        }
    }
}
