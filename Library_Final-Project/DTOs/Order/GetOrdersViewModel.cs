using System.Collections.Generic;

namespace Library_Final_Project.DTOs.Order
{
    public class GetOrdersViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
        public Dictionary<int,string> OrderStates { get; set; }
    }
}
