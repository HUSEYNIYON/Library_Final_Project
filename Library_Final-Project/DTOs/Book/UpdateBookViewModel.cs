﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.DTOs.Book
{
    public class UpdateBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Available { get; set; }
        public string PrevImagePath { get; set; }
        public string PrevPdfPath { get; set; }
        public IFormFile Image { get; set; }
        public int AvailableCount { get; set; }
        public int PublishYear { get; set; }
        public string Language { get; set; }
        public int PagesNumber { get; set; }
        public string Description { get; set; }
        public IFormFile PdfFile { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }
        public double Percent { get; set; }
        public Dictionary<int, string> PaymentTypes { get; set; }
        public List<int> PaymentTypesId { get; set; }
        public Dictionary<int, string> DeliveryTypes { get; set; }
        public List<int> DeliveryTypesId { get; set; }
        public Dictionary<int, string> Authors { get; set; }
        public List<int> AuthorsId { get; set; }
        public Dictionary<int, string> Categories { get; set; }
        public int CategoryId { get; set; }
    }
}