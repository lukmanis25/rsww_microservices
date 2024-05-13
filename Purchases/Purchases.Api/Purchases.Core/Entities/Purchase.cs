using Purchases.Core.Events;
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
        public PaymentStatus? PaymentStatus { get; protected set; }
        public float Price { get; protected set; }
        public DateTime? PaymentDateTime { get; protected set; }


        public Purchase(
                Guid id,
                Guid customerId,
                Guid reservationId,
                PaymentStatus? paymentStatus,
                float price,
                DateTime? paymentDateTime,
                int version = 0
            )
        {
            Id = id;
            CustomerId = customerId;
            ReservationId = reservationId;
            PaymentStatus = paymentStatus;
            Price = price;
            PaymentDateTime = paymentDateTime;
            Version = version;
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
                paymentStatus: null,
                paymentDateTime: null
                );
            purchase.AddEvent(new PurchaseCreated{ Purchase = purchase});
            return purchase;
        }

        public void StartPayment()
        {
            PaymentStatus = Entities.PaymentStatus.Pending;
            AddEvent(new PaymentStarted { Purchase = this });
        }

        public void FinishPayment(PaymentStatus paymentStatus)
        {
            PaymentStatus = paymentStatus;
            PaymentDateTime = DateTime.Now;
            AddEvent(new PaymentFinished { Purchase = this });
        }
    }
}
