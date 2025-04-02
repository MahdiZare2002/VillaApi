namespace OnlineShop.Pagination
{
    public class BasePagination
    {
        public int DataCount { get; set; }
        public int PageId { get; set; }
        public int PageCount { get; set; }
        public int Take { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public void Generate(IQueryable<object> query, int pageId, int take)
        {
            DataCount = query.Count();
            PageId = pageId;
            Take = take;
            PageCount = query.Count() / Take;
            if (DataCount % Take > 0) PageCount++;
            StartPage = (PageId - 2 <= 0) ? 1 : PageId - 2;
            EndPage = (PageId + 2 > PageCount) ? PageCount : PageId + 2;
        }
    }
}
