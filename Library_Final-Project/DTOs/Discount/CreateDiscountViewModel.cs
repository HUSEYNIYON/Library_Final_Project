using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.DTOs.Discount
{
    public class CreateDiscountViewModel
    {
        [Required]
        public double Price { get; set; }
        [Required]
        public double Percent { get; set; }
    }
}
