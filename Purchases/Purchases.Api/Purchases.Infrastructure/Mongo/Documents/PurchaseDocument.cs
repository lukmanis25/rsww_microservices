using Convey.Types;
using Purchases.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Infrastructure.Mongo.Documents
{
    internal sealed class PurchaseDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ReservationId { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public float Price { get; set; }
        public DateTime? PaymentDateTime { get; set; }
        public int Version { get; set; }
    }
}
