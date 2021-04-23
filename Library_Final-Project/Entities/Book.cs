using System.Collections.Generic;

namespace Library_Final_Project.Entities
{
    public class Book
    {
        public int Id { get; set; }         
        public string Title { get; set; }   //create
        public int AuthorId { get; set; }   //create
        public string ImagePath { get; set; }   //formfile File
        public bool Available { get; set; }     //create
        public int AvailableCount { get; set; } //create
        public int PublishYear { get; set; }//create
        public string Language { get; set; }//create
        public int PagesNumber { get; set; }//create
        public string Description { get; set; }//create
        public bool HasPdf { get; set; }//create
        public string PdfPath { get; set; }//create
        public string ISBN { get; set; }//create
        public int DiscountId { get; set; }//create
        public virtual Discount Discount { get; set; }
        public virtual ICollection<OrderBook> OrderBooks { get; set; } 
        public virtual ICollection<BookDeliveryType> BookDeliveryTypes { get; set; }
        public virtual ICollection<BookPaymentType> BookPaymentTypes { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<FavoriteBook> FavoriteBooks { get; set; }
    }
}
