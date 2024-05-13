using Purchases.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Application.DTO
{
    public class PurchaseDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ReservationId { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public float Price { get; set; }
        public DateTime? PaymentDateTime { get; set; }
    }
}
