using System.ComponentModel.DataAnnotations;

namespace Library_Final_Project.DTOs.Category
{
    public class CategoryViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string IconPath { get; set; }
    }
}
