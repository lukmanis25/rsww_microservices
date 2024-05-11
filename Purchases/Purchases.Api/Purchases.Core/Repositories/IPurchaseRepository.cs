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
        Task<Purchase> GetAsync(AggregateId id);
        Task AddAsync(Purchase reservation);
        Task UpdateAsync(Purchase reservation);
        Task DeleteAsync(AggregateId id);
    }
}
