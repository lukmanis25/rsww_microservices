using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.Rejected
{
    [Contract]
    public class ReservateOffertRejected : IRejectedEvent
    {
        public Guid OfferId { get; set; }
        public Guid CustomerId { get; set; }
        public string Reason { get;  }

        public string Code { get;  }
    }
}
