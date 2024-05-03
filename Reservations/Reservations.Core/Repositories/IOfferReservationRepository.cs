using Reservations.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Repositories
{
    public interface IOfferReservationRepository
    {
        Task<OfferReservation> GetAsync(AggregateId id);
        Task<bool> ExistsAsync(AggregateId id);
        Task AddAsync(OfferReservation offerReservation);
        Task UpdateAsync(OfferReservation offerReservation);
        Task DeleteAsync(AggregateId id);
    }
}
