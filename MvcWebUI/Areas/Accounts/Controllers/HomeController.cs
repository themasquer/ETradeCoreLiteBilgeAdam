#nullable disable
using Microsoft.AspNetCore.Mvc;

namespace _038_ETradeCoreLiteBilgeAdam.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class HomeController : Controller
    {
        // Add service injections here
        //private readonly AccountServiceBase _accountService;

        //public HomeController(AccountServiceBase accountService)
        //{
        //    _accountService = accountService;
        //}

        public IActionResult Login()
        {
            return View();
        }
	}
}
