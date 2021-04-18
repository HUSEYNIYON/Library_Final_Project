namespace Library_Final_Project.Entities
{
    public class BookDeliveryType
    {
        public int BookId { get; set; }
        public int DeliveryTypeId { get; set; }
        public virtual Book Book { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
    }
}
