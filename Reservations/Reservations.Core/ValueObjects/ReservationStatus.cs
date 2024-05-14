using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Core.ValueObjects
{
    public enum ReservationStatus
    {
        PendingReservationApproval,
        Reserved,
        Cancelled,
        Purchased
    }
}
