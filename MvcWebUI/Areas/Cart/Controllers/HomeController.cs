using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Services.CRUD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace _038_ETradeCoreLiteBilgeAdam.Areas.Cart.Controllers
{
    [Authorize]
    [Area("Cart")]
    public class HomeController : Controller
    {
        const string _cartKey = "cart";

        private readonly ProductServiceBase _productService;

        public HomeController(ProductServiceBase productService)
        {
            _productService = productService;
        }

        public IActionResult Add(int productId)
        {
            int userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            Product product = _productService.GetItem(productId);
            List<CartItemModel> cart = GetSession();
            CartItemModel item = new CartItemModel()
            {
                ProductId = product.Id,
                UserId = userId,
                ProductName = product.Name,
                UnitPrice = product.UnitPrice ?? 0
            };
            cart.Add(item);
            SetSession(cart);
            TempData["CartMessage"] = "Product added to cart.";
            return RedirectToAction("Index", "Products", new { area = "" });
        }

        public IActionResult List()
        {
            int userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            List<CartItemModel> cart = GetSession();
            List<CartItemModel> userCart = cart.Where(c => c.UserId == userId).ToList();
            List<CartItemGroupByModel> userCartGroupBy = (from c in userCart
                                                          group c by new { c.ProductId, c.UserId, c.ProductName }
                                                          into cGroupBy
                                                          select new CartItemGroupByModel()
                                                          {
                                                              ProductId = cGroupBy.Key.ProductId,
                                                              UserId = cGroupBy.Key.UserId,
                                                              ProductName = cGroupBy.Key.ProductName,
                                                              TotalUnitPrice = cGroupBy.Sum(cgb => cgb.UnitPrice),
                                                              TotalUnitPriceDisplay = cGroupBy.Sum(cgb => cgb.UnitPrice).ToString("C2"),
                                                              ProductCount = cGroupBy.Count()
                                                          }).ToList();
            return View(userCartGroupBy);
        }

        public IActionResult Clear()
        {
            int userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            List<CartItemModel> cart = GetSession();
            List<CartItemModel> userCart = cart.Where(c => c.UserId == userId).ToList();
            foreach (CartItemModel userCartItem in userCart)
            {
                cart.Remove(userCartItem);
            }
            SetSession(cart);
            TempData["CartMessage"] = "Products removed from cart.";
            return RedirectToAction(nameof(List));
        }

        public IActionResult Remove(int productId, int userId)
        {
            List<CartItemModel> cart = GetSession();
            CartItemModel item = cart.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
            cart.Remove(item);
            SetSession(cart);
            TempData["CartMessage"] = "Product removed from cart.";
            return RedirectToAction(nameof(List));
        }

        private List<CartItemModel> GetSession()
        {
            List<CartItemModel> cart = new List<CartItemModel>();
            string cartJson = HttpContext.Session.GetString(_cartKey);
            if (cartJson != null)
                cart = JsonConvert.DeserializeObject<List<CartItemModel>>(cartJson);
            return cart;
        }

        private void SetSession(List<CartItemModel> cart)
        {
            string cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(_cartKey, cartJson);
        }
    }
}
