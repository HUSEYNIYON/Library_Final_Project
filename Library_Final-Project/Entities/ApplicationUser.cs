using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<FavoriteBook> FavoriteBooks { get; set; }
        public virtual  ICollection<UserBook> UserBooks{ get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
