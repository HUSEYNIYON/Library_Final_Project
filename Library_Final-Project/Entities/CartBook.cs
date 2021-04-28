namespace Library_Final_Project.Entities
{
    public class CartBook
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }
    }
}
