using Purchases.Application.DTO;
using Purchases.Core.Entities;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static Purchase AsEntity(this PurchaseDocument document)
            => new Purchase(
                id: document.Id,
                customerId: document.CustomerId,
                reservationId: document.ReservationId,
                paymentStatus: document.PaymentStatus,
                price: document.Price,
                paymentDateTime: document.PaymentDateTime,
                version: document.Version
            );

        public static PurchaseDocument AsDocument(this Purchase entity)
        {
            return new PurchaseDocument
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                ReservationId = entity.ReservationId,
                PaymentStatus = entity.PaymentStatus,
                Price = entity.Price,
                PaymentDateTime = entity.PaymentDateTime,
                Version = entity.Version
            };
        }

        public static PurchaseDto AsDto(this PurchaseDocument document)
        {
            return new PurchaseDto
            {
                Id = document.Id,
                CustomerId = document.CustomerId,
                ReservationId = document.ReservationId,
                PaymentStatus = document.PaymentStatus,
                Price = document.Price,
                PaymentDateTime = document.PaymentDateTime,
            };
        }
    }
}
