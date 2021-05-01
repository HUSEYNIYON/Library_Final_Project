using Library_Final_Project.DTOs.Book;
using System.Collections.Generic;

namespace Library_Final_Project.DTOs
{
    public class CreateOrderViewModel
    {
        public string Address { get; set; }
        public int DeliveryTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public string Comment { get; set; }
        public List<OrderBookViewModel> Books { get; set; }
        public Dictionary<int,string> DeliveryTypes { get; set; }
        public Dictionary<int,string> PaymentTypes { get; set; }
    }
}
