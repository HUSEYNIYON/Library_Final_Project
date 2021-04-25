using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Services.PaymentType
{
    public class PaymentTypeService
    {
        private readonly LibraryDbContext _context;

        public PaymentTypeService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<int,string>> GetAllInDictionaryAsync()
        {
            return await _context.PaymentTypes.ToDictionaryAsync(x => x.Id, x => x.Name);
        } 
    }
}
