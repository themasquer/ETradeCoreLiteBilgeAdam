using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _038_ETradeCoreLiteBilgeAdam.Models
{
    public class ReportsIndexViewModel
    {
        public List<ReportModel> Reports { get; set; }
        public int RecordsCount { get; set; }
        public ReportFilterModel Filter { get; set; }
        public SelectList Categories { get; set; }
        public MultiSelectList Stores { get; set; }
    }
}
