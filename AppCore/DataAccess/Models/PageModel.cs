namespace AppCore.DataAccess.Models
{
    public class PageModel
    {
        public int PageNumber { get; set; } = 1;
        public int TotalRecordsCount { get; set; }
        public int RecordsPerPageCount { get; set; } = 10;

        private List<int> _pageNumbers;
        public List<int> PageNumbers
        {
            get
            {
                _pageNumbers = new List<int>();
                int totalPageNumber = (int)Math.Ceiling((double)TotalRecordsCount / RecordsPerPageCount);
                for (int pageNumber = 1; pageNumber <= totalPageNumber; pageNumber++)
                {
                    _pageNumbers.Add(pageNumber);
                }
                return _pageNumbers;
            }
        }
    }
}
