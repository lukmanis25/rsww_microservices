using System.Threading;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using MongoDB.Driver;
using Tours.Application.DTO;
using Tours.Application.Queries;
using Tours.Infrastructure.Mongo.Documents;

namespace Tours.Infrastructure.Mongo.Queries.Handlers;

internal sealed class GetTourByIdHandler : IQueryHandler<GetTourById, TourDto>
{
    private readonly IMongoDatabase _database;

    public GetTourByIdHandler(IMongoDatabase database)
    {
        _database = database;
    }

    public Task<TourDto> HandleAsync(GetTourById query, CancellationToken cancellationToken = default)
    {
        var collection = _database.GetCollection<TourDocument>("tours");
        var document = collection.Find(d => d.Id == query.Id).SingleOrDefault(cancellationToken);
        return document is null ? Task.FromResult<TourDto>(null) : Task.FromResult(document.AsDto());
    }
}