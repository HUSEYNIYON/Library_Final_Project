namespace Library_Final_Project.Entities
{
    public class CartBook
    {
        public uint Id { get; set; }
        public int BookId { get; set; }
        public int CartId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
