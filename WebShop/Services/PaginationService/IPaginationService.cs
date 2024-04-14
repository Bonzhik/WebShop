using System.Collections;
using WebShop.Dtos.Read;

namespace WebShop.Services.PaginationService
{
    public interface IPaginationService<T>
    {
        PaginationResponse<T> Paginate(List<T> data, int page, int pageSize);
    }
}
