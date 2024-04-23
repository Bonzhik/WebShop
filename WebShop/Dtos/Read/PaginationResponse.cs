namespace WebShop.Dtos.Read
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPrevPage { get; set; }
    }
}
