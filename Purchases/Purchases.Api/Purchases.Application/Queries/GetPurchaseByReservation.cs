using Convey.CQRS.Queries;
using Purchases.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Application.Queries
{
    public class GetPurchaseByReservation : IQuery<PurchaseDto>
    {
        public Guid ReservationId { get; set; }
    }
}
