#nullable disable
using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.Models;
using DataAccess.Services;
using DataAccess.Services.CRUD;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace _038_ETradeCoreLiteBilgeAdam.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class HomeController : Controller
    {
        // Add service injections here
        private readonly IAccountService _accountService;
        private readonly CountryServiceBase _countryService;
        private readonly CityServiceBase _cityService;

        public HomeController(IAccountService accountService, CountryServiceBase countryService, CityServiceBase cityService)
        {
            _accountService = accountService;
            _countryService = countryService;
            _cityService = cityService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _accountService.Login(model);
                if (user != null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.RoleNameDisplay),
                        new Claim(ClaimTypes.Sid, user.Id.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult AccessDenied()
        {
            return View("_Error", "You don't have access to this operation!");
        }

        public IActionResult Register()
        {
            ViewBag.Countries = new SelectList(_countryService.GetList(), "Id", "Name");
            ViewBag.Cities = new SelectList(new List<City>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _accountService.Register(model);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Login));
                ModelState.AddModelError("", result.Message);
            }
            ViewBag.Countries = new SelectList(_countryService.GetList(), "Id", "Name", model.CountryId);
            ViewBag.Cities = new SelectList(_cityService.GetList(c => c.CountryId == model.CountryId), "Id", "Name", model.CityId);
            return View(model);
        }
    }
}
