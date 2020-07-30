using ECommerce.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Models
{
    /// <summary>
    /// Order Model For Frontend
    /// </summary>
    public class OrderModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public int? ExpireMonth { get; set; }

        public int? ExpireYear { get; set; }

        public string Cvc { get; set; }

        public virtual EnumPaymentType? PaymentType { get; set; }

        public virtual List<OrderItemModel> OrderItems { get; set; }


    }

    public class OrderItemModel
    {
        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }
    }
}
