using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.DTOs.Author
{
    public class CreateAuthorViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
