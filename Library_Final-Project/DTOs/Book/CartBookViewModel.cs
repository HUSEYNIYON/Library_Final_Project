using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.DTOs.Book
{
    public class CartBookViewModel
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
