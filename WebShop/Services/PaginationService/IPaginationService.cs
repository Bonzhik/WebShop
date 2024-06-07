using WebShop.Dtos.Read;

namespace WebShop.Services.PaginationService
{
    public interface IPaginationService<T>
    {
        PaginationResponse<T> Paginate(IQueryable<T> data, int page, int pageSize);
    }
}
