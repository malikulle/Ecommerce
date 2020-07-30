using ECommerce.Entity.Trackable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Entity.Models
{
    public class Order : BaseEntity , TEntity
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual User User { get; set; }
        public int? UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string OrderNote { get; set; }

        
        public string PaymentId { get; set; }

        public string PaymentToken { get; set; }

        public string ConcersationId { get; set; }


        public virtual EnumOrderState OrderState { get; set; }

        public virtual EnumPaymentType PaymentType { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

    }

    public enum EnumOrderState
    {
        Waiting = 1,
        Unpaid = 2,
        Complated = 3
    }

    public enum EnumPaymentType
    {
        CreditCard = 1,
        EFT = 2
    }
}
