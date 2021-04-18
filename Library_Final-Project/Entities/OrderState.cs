using System.Collections.Generic;

namespace Library_Final_Project.Entities
{
    public class OrderState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
