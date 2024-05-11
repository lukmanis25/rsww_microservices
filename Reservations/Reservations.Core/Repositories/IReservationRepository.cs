using Reservations.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> GetAsync(AggregateId id);
        Task<bool> ExistsAsync(AggregateId id);
        Task AddAsync(Reservation offertReservation);
        Task UpdateAsync(Reservation offertReservation);
        Task DeleteAsync(AggregateId id);
    }
}
