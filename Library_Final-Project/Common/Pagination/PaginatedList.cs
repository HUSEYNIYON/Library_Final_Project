using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Common.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        private readonly int _totalPages;
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            _totalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        public bool PreviousPage => (PageIndex > 1);
        public bool NextPage => (PageIndex < _totalPages);
    }
}
