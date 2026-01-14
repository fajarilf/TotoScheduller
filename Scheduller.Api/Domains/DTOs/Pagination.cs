namespace Scheduller.Api.Domains.DTOs
{
    public static class Pagination
    {
        public static PagedResult<T> Paginante<T>(IEnumerable<T> items, int page, int pageSize, int totalCount)
        {
            return new PagedResult<T>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }

    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int Page {  get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount/PageSize);
    }
}
