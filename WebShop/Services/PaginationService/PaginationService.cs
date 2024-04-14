using System.Collections;
using WebShop.Dtos.Read;

namespace WebShop.Services.PaginationService
{
    public class PaginationService<T> : IPaginationService<T>
    {
        public PaginationResponse<T> Paginate(List<T> data, int page, int pageSize)
        {
            int totalCount = data.Count;
            bool hasPrevPage = page > 1;
            bool hasNextPage = page * pageSize < totalCount;
            var paginationData = data.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToList();

            var pagination = new PaginationResponse<T>
            {
                Data = paginationData,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                HasPrevPage = hasPrevPage,
                HasNextPage = hasNextPage
            };

            return pagination;
        }
    }
}
