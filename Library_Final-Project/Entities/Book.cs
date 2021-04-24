using System.Collections.Generic;

namespace Library_Final_Project.Entities
{
    public class Book
    {
        public int Id { get; set; }         
        public string Title { get; set; } 
        public string ImagePath { get; set; }  
        public bool Available { get; set; }    
        public int AvailableCount { get; set; } 
        public int PublishYear { get; set; }
        public string Language { get; set; }
        public int PagesNumber { get; set; }
        public string Description { get; set; }
        public bool HasPdf { get; set; }
        public string PdfPath { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }
        public double Percent { get; set; }
        public virtual ICollection<OrderBook> OrderBooks { get; set; } 
        public virtual ICollection<BookDeliveryType> BookDeliveryTypes { get; set; }
        public virtual ICollection<BookPaymentType> BookPaymentTypes { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<FavoriteBook> FavoriteBooks { get; set; }
    }
}
