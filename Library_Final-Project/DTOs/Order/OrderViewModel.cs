using System;

namespace Library_Final_Project.DTOs.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
    }
}
