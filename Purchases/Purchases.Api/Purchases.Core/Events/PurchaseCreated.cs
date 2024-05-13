using Purchases.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Core.Events
{
    public class PurchaseCreated : IDomainEvent
    {
        public Purchase Purchase { get; set; }
    }
}
