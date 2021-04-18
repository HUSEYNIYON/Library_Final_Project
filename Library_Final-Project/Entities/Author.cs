using System.Collections.Generic;

namespace Library_Final_Project.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } 
    }
}
