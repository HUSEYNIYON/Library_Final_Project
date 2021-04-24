using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.DTOs.Book
{
    public class BookViewModel
    {
        public string Title { get; set; }  
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Percent { get; set; }
    }
}
