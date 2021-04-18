﻿namespace Library_Final_Project.Entities
{
    public class UserBook
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public virtual Book Book { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
