using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
