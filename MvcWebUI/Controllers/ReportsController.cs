using _038_ETradeCoreLiteBilgeAdam.Models;
using DataAccess.Models;
using DataAccess.Services;
using DataAccess.Services.CRUD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _038_ETradeCoreLiteBilgeAdam.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        private readonly CategoryServiceBase _categoryService;
        private readonly StoreServiceBase _storeService;

        public ReportsController(IReportService reportService, CategoryServiceBase categoryService, StoreServiceBase storeService)
        {
            _reportService = reportService;
            _categoryService = categoryService;
            _storeService = storeService;
        }

        public IActionResult Index(ReportFilterModel filter)
        {
            var reports = _reportService.GetList(filter);
            var viewModel = new ReportsIndexViewModel()
            {
                Reports = reports,
                RecordsCount = reports.DistinctBy(r => r.ProductId).Count(),
                Filter = filter,
                Categories = new SelectList(_categoryService.GetList(), "Id", "Name", filter.CategoryId),
                Stores = new MultiSelectList(_storeService.GetList(), "Id", "Name", filter.StoreIds)
            };
            return View(viewModel);
        }
    }
}
