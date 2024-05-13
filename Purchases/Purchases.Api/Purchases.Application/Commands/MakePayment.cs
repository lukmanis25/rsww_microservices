using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Application.Commands
{
    public class MakePayment : ICommand
    {
        public Guid ReservationId { get; set; }
    }
}
