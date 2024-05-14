using Purchases.Core.Entities;
using Reservations.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Core
{
    public static class EnumExtensions
    {
        public static string PaymentStatusAsString(this PaymentStatus status)
        {
            return Enum.GetName(typeof(PaymentStatus), status);
        }

        public static PaymentStatus StringAsPaymentStatus(this string statusString)
        {
            if (Enum.TryParse(typeof(PaymentStatus), statusString, out object result))
            {
                return (PaymentStatus)result;
            }
            else
            {
                throw new InvalidPaymentStatusException();
            }
        }
    }
}
