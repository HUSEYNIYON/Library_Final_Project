using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Entities
{
    public class CartBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CartId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
