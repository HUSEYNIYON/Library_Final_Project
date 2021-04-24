using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.DTOs.Category
{
    public class EditCategoryViewModel:CreateCategoryViewModel
    {
        public int Id { get; set; }
        public string IconPath { get; set; }
    }
}
