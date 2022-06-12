using AppCore.DataAccess.Models;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _038_ETradeCoreLiteBilgeAdam.Areas.Reports.Models
{
    public class ReportsIndexViewModel
    {
        public List<ReportModel> Reports { get; set; }
        public ReportFilterModel Filter { get; set; }
        public SelectList Categories { get; set; }
        public MultiSelectList Stores { get; set; }
        public PageModel Page { get; set; }
        public SelectList Pages { get; set; }
    }
}
