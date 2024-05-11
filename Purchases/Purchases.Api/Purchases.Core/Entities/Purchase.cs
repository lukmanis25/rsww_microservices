using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Core.Entities
{
    public enum PaymentStatus
    {
        Pending,
        Failed,
        Accepted
    }
    public class Purchase : AggregateRoot
    {
        public Guid CustomerId { get; protected set; }
        public Guid ReservationId { get; protected set; }
        public PaymentStatus PaymentStatus { get; protected set; }
        public float Price { get; protected set; }
        public DateTime? PaymentDateTime { get; protected set; }


        public Purchase(
                Guid id,
                Guid customerId,
                Guid reservationId,
                PaymentStatus paymentStatus,
                float price,
                DateTime? paymentDateTime
            )
        {
            Id = id;
            CustomerId = customerId;
            ReservationId = reservationId;
            PaymentStatus = paymentStatus;
            Price = price;
            PaymentDateTime = paymentDateTime;
        }

        public static Purchase Create(
                Guid customerId,
                Guid reservationId,
                float price
            )
        {
            var purchase = new Purchase(
                id: Guid.NewGuid(),
                customerId: customerId,
                reservationId: reservationId,
                price: price,
                paymentStatus: PaymentStatus.Pending,
                paymentDateTime: null
                );
            return purchase;
        }
    }
}
