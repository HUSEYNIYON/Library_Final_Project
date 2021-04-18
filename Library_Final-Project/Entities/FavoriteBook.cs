using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Entities
{
    public class FavoriteBook
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public virtual Book Book { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
