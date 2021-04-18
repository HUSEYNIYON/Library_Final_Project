using System.Collections.Generic;

namespace Library_Final_Project.Entities
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BookPaymentType> BookPaymentTypes { get; set; }
    }
}
