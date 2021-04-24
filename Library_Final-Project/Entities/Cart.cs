﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<CartBook> CartBooks { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
