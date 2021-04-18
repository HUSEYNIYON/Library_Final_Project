using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
