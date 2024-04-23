using System.Linq.Dynamic.Core;

namespace WebShop.Services.SortingService
{
    public class SortingService<T> : ISortingService<T>
    {
        public List<T> Sort(List<T> data, string sortField, string sortOrder)
        {
            var orderBy = $"{sortField} {sortOrder}";
            var result = data.AsQueryable().OrderBy(orderBy).ToList();
            return result;
        }
    }
}
