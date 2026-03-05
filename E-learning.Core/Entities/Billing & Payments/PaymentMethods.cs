using E_learning.Core.Entities.Identity;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Billing___Payments
{
    public class PaymentMethods
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser AppUser { get; set; }

        public PaymentMethodsType Type { get; set; }
        public string? CardLastFour { get; set; }
        public string? CardHolderName { get; set; }
        public int? ExpiryMonth { get; set; }
        public int? ExpiryYear { get; set; }
        public string? PayPalEmail { get; set; }
        public bool IsDefault { get; set; }=false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PaymentTransactions> PaymentTransactions { get; set; }
    }
}
