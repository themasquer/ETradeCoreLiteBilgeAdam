using DataAccess.Entities;
using DataAccess.Services.CRUD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace _038_ETradeCoreLiteBilgeAdam.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly CategoryServiceBase _categoryService;

        public CategoriesViewComponent(CategoryServiceBase categoryService)
        {
            _categoryService = categoryService;
        }

        public ViewViewComponentResult Invoke()
        {
            Task<List<Category>> task = _categoryService.GetCategoriesAsync();
            List<Category> categories = task.Result;
            return View(categories);
        }
    }
}
