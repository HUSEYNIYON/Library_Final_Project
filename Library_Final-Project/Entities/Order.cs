using System;
using System.Collections.Generic;

namespace Library_Final_Project.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreateAt { get; set; }
        public int DeliveryTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public int OrderStateId { get; set; }
        public string Address { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual OrderState OrderState { get; set; }
        public virtual ICollection<OrderBook> OrderBooks { get; set; } 
    }
}
