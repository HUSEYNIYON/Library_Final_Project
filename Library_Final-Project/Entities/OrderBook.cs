using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Entities
{
    public class OrderBook
    {
        public int BookId { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }
    }
}
