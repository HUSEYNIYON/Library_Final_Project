using System.Collections.Generic;

namespace Library_Final_Project.Entities
{
    public class DeliveryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual ICollection<BookDeliveryType> BookDeliveryTypes { get; set; }
    }
}
