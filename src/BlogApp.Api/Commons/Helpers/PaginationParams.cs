namespace BlogApp.Api.Commons.Helpers
{
    public class PaginationParams
    {
        private int _pageIndex;
        private int _pageSize;

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value is < 0 or > 100 ? 10 : value;
        }
    }
}
