using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Application.Exceptions
{
    public class ReservationPurchaseDoesntExistException : AppException
    {
        public override string Code { get; } = "reservation_purchase_doesnt_exists";
        public Guid Id { get; }

        public ReservationPurchaseDoesntExistException(Guid id) : base($"Purchase for reservation id: {id} doesnt exist.")
            => Id = id;
    }
}
