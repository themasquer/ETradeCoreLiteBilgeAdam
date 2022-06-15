namespace AppCore.DataAccess.Models
{
    public class PageModel
    {
        public int PageNumber { get; set; }
        public int TotalRecordsCount { get; set; }
        public int RecordsPerPageCount { get; set; }

        private List<int> _pageNumberList;
        public List<int> PageNumberList
        {
            get
            {
                _pageNumberList = new List<int>();
                int totalPageNumber = (int)Math.Ceiling((double)TotalRecordsCount / RecordsPerPageCount);
                for (int pageNumber = 1; pageNumber <= totalPageNumber; pageNumber++)
                {
                    _pageNumberList.Add(pageNumber);
                }
                return _pageNumberList;
            }
        }

        public List<int> RecordsPerPageCountList => new List<int>() { 5, 10, 25, 50, 100 };
    }
}
