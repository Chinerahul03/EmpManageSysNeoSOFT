using EmpManageSys2.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpManageSys2.ViewModel
{
    public class PaginatedList
    {
        public IEnumerable<EmployeeMaster> employeeMasters { get; set; }
        public string SearchTerm { get; set; }
        public string SortOrder { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        //    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        //    {
        //        PageIndex = pageIndex;
        //        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        //        this.AddRange(items);
        //    }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        //    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        //    {
        //        var count = await source.CountAsync();
        //        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        //        return new PaginatedList<T>(items, count, pageIndex, pageSize);
        //    }
    }
}
