using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Services.DeliveryType
{
    public class DeliveryTypeService
    {
        private readonly LibraryDbContext _context;

        public DeliveryTypeService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<int,string>> GetAllInDictionaryAsync()
        {
            return await _context.DeliveryTypes.ToDictionaryAsync(x => x.Id, x => x.Name);
        }
    }
}
