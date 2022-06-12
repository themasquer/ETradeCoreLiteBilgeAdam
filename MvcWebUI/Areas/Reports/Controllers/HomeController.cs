using _038_ETradeCoreLiteBilgeAdam.Areas.Reports.Models;
using _038_ETradeCoreLiteBilgeAdam.Settings;
using AppCore.DataAccess.Models;
using DataAccess.Models;
using DataAccess.Services;
using DataAccess.Services.CRUD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _038_ETradeCoreLiteBilgeAdam.Areas.Reports.Controllers
{
    [Area("Reports")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;
        private readonly CategoryServiceBase _categoryService;
        private readonly StoreServiceBase _storeService;

        public HomeController(IReportService reportService, CategoryServiceBase categoryService, StoreServiceBase storeService)
        {
            _reportService = reportService;
            _categoryService = categoryService;
            _storeService = storeService;
        }

        public IActionResult Index(ReportsIndexViewModel viewModel)
        {
            viewModel.Filter = viewModel.Filter ?? new ReportFilterModel();
            viewModel.Page = viewModel.Page ?? new PageModel();
            viewModel.Page.RecordsPerPageCount = AppSettings.RecordsPerPageCount;
            viewModel.Reports = _reportService.GetList(viewModel.Filter, viewModel.Page);
            viewModel.Categories = new SelectList(_categoryService.GetList(), "Id", "Name", viewModel.Filter.CategoryId);
            viewModel.Stores = new MultiSelectList(_storeService.GetList(), "Id", "Name", viewModel.Filter.StoreIds);
            viewModel.Pages = new SelectList(viewModel.Page.PageNumbers, viewModel.Page.PageNumber);
            return View(viewModel);
        }
    }
}
