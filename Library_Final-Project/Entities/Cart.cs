using System.Collections.Generic;

namespace Library_Final_Project.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<CartBook> CartBooks { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
