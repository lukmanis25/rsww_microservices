using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using Tours.Core.Entities;
using Tours.Infrastructure.Mongo.Documents;

namespace Tours.Infrastructure.Mongo.Repositories;

internal sealed class TourMongoRepository : ITourRepository
{
    private readonly IMongoRepository<TourDocument, Guid> _repository;

    public TourMongoRepository(IMongoRepository<TourDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<Tour> GetByIdAsync(Guid id)
    {
        var doc = await _repository.GetAsync(id);
        return doc?.AsEntity();
    }

    public async Task<IEnumerable<Tour>> GetAllAsync()
    {
        var doc = await _repository.FindAsync(_ => true);
        return doc?.Select(d => d.AsEntity());
    }

    public Task AddAsync(Tour tour)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Tour tour)
    {
        var doc = tour.AsDocument();
        await _repository.UpdateAsync(doc);
    }

    public async Task UpdatePartialAsync(Guid id, object updateDefinition)
    {
        var updateDef = updateDefinition as UpdateDefinition<TourDocument>;

        if (updateDef is null)
        {
            throw new InvalidOperationException("Invalid update definition.");
        }

        await _repository.Collection.UpdateOneAsync(t => t.Id == id, updateDef);
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Tour>> GetAllByHotelId(Guid hotelId)
    {
        var doc = await _repository.FindAsync(t => t.HotelResource.HotelResourceId == hotelId);
        return doc?.Select(d => d.AsEntity());
    }

    public async Task<IEnumerable<Tour>> GetAllByTransportId(Guid transportId)
    {
        var doc = await _repository.FindAsync(t => t.TransportToResource.TransportResourceId == transportId
                                                         || t.TransportBackResource.TransportResourceId == transportId);
        return doc?.Select(d => d.AsEntity());
    }

    public async Task UpdateManyAsync(IEnumerable<Tour> tours)
    {
        foreach (var tour in tours)
        {
            await UpdateAsync(tour);
        }
    }
}