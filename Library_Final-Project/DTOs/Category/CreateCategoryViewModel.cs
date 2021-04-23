using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Library_Final_Project.DTOs.Category
{
    public class CreateCategoryViewModel
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public int? ParentCategoryId { get; set; }
        public Dictionary<int,string> Categories { get; set; }
    }
}
