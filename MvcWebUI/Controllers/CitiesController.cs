using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace _038_ETradeCoreLiteBilgeAdam.Controllers
{
    public class CitiesController : Controller
    {
        private readonly CityServiceBase _cityService;

        public CitiesController(CityServiceBase cityService)
        {
            _cityService = cityService;
        }

        public IActionResult GetJson(int? countryId)
        {
            return Json(_cityService.GetCities(countryId ?? 0));
        }
    }
}
