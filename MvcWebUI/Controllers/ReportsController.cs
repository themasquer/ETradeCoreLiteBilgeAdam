using _038_ETradeCoreLiteBilgeAdam.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _038_ETradeCoreLiteBilgeAdam.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            var reports = _reportService.GetList();
            var viewModel = new ReportsIndexViewModel()
            {
                Reports = reports,
                RecordsCount = reports.DistinctBy(r => r.ProductId).Count()
            };
            return View(viewModel);
        }
    }
}
