using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _038_ETradeCoreLiteBilgeAdam.Controllers
{
    public class DbController : Controller
    {
        private readonly Db _db;

        public DbController(Db db)
        {
            _db = db;
        }

        public IActionResult Seed()
        {
            var products = _db.Products.ToList();
            _db.Products.RemoveRange(products);

            var categories = _db.Categories.ToList();
            _db.Categories.RemoveRange(categories);

            _db.Categories.Add(new Category()
            {
                Name = "Computer",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Name = "Laptop",
                        UnitPrice = 3000.5,
                        ExpirationDate = new DateTime(2032, 1, 27)
                    },
                    new Product()
                    {
                        Name = "Mouse",
                        UnitPrice = 20.5,
                        ExpirationDate = new DateTime(2027, 5, 19)
                    },
                    new Product()
                    {
                        Name = "Keyboard",
                        UnitPrice = 40,
                    },
                    new Product()
                    {
                        Name = "Monitor",
                        UnitPrice = 2500,
                    }
                }
            });

            _db.Categories.Add(new Category()
            {
                Name = "Home Theater System",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Name = "Speaker",
                        UnitPrice = 2500
                    },
                    new Product()
                    {
                        Name = "Receiver",
                        UnitPrice = 5000,
                    },
                    new Product()
                    {
                        Name = "Equalizer",
                        UnitPrice = 1000,
                    }
                }
            });

            _db.SaveChanges();

            return Content("<label style=\"color:red;\"><b>Database seed successful.</b></label>", "text/html");
        }
    }
}
