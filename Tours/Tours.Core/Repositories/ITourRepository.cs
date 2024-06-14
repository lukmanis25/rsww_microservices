using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Tours.Core.Entities;

public interface ITourRepository
{
    Task<Tour> GetByIdAsync(Guid id);
    Task<IEnumerable<Tour>> GetAllAsync();
    Task<IEnumerable<Tour>> GetAllByHotelId(Guid hotelId);
    Task<IEnumerable<Tour>> GetAllByTransportId(Guid transportId);
    Task AddAsync(Tour tour);
    Task UpdateAsync(Tour tour);
    Task UpdateManyAsync(IEnumerable<Tour> tours);
    Task UpdatePartialAsync(Guid id, object updateDefinition);
    Task DeleteAsync(Guid id);
}