using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Library_Final_Project.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<FavoriteBook> FavoriteBooks { get; set; }
        public virtual  ICollection<UserBook> UserBooks{ get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
