namespace TerraMours_Gpt.Domains.LoginDomain.Contracts.Common
{
    public class PagedRes<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PagedRes(IEnumerable<T> items, int total, int page, int pageSize)
        {
            Items = items;
            Total = total;
            Page = page;
            PageSize = pageSize;
        }
    }

}
