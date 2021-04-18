using System;

namespace Library_Final_Project.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreateAt { get; set; }
        public int ReviewStateId { get; set; }
        public virtual ReviewState ReviewState { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }
    }
}
