namespace WebShop.Services.SortingService
{
    public interface ISortingService<T>
    {
        List<T> Sort(List<T> data, string sortField, string sortOrder);
    }
}
