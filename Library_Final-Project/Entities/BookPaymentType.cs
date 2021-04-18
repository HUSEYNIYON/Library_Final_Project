namespace Library_Final_Project.Entities
{
    public class BookPaymentType
    {
        public int BookId { get; set; }
        public int PaymentTypeId { get; set; }
        public virtual Book Book { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}
