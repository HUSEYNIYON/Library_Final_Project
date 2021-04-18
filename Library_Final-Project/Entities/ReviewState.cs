using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Entities
{
    public class ReviewState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Review> Reviews{ get; set; }

    }
}
