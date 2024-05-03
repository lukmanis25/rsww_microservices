using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.Entities
{
    public enum ReservationStatus
    {
        New,
        PendingReservationApproval,
        Reserved,
        Canceled,
        Purchased
    }
}
