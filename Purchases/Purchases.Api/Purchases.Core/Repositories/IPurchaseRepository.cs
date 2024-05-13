using Purchases.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Core.Purchases
{
    public interface IPurchaseRepository
    {
        Task<Purchase> GetByReservationAsync(Guid reservationId);
        Task<Purchase> GetAsync(AggregateId id);
        Task<bool> ExistsByReservationAsync(Guid reservationId);
        Task<bool> ExistsAsync(AggregateId id);
        Task AddAsync(Purchase purchase);
        Task<bool> UpdateAsync(Purchase purchase);
        Task DeleteAsync(AggregateId id);
    }
}
