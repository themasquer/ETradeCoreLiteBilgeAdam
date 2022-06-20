#nullable disable
using _038_ETradeCoreLiteBilgeAdam.Settings;
using AppCore.Utils;
using DataAccess.Entities;
using DataAccess.Services.CRUD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _038_ETradeCoreLiteBilgeAdam.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        // Add service injections here
        private readonly ProductServiceBase _productService;
        private readonly CategoryServiceBase _categoryService;
        private readonly StoreServiceBase _storeService;

        public ProductsController(ProductServiceBase productService, CategoryServiceBase categoryService, StoreServiceBase storeService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _storeService = storeService;
        }

        // GET: Products
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<Product> productList = _productService.GetList();
            return View(productList);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            Product product = _productService.GetItem(id); 
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CategoryId"] = new SelectList(_categoryService.GetList(), "Id", "Name");
            ViewBag.Stores = new MultiSelectList(_storeService.GetList(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (UpdateImage(product, image) == false) // imaj uzantı ve boyut validasyonlarını geçememiş demektir
                {
                    ModelState.AddModelError("", $"Image could not be uploaded: Accepted image extensions are {AppSettings.AcceptedImageExtensions} and accepted image maximum length is {AppSettings.AcceptedImageMaximumLength}!");
                }
                else
                {
                    var result = _productService.Add(product);
                    if (result.IsSuccessful)
                    {
                        TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", result.Message);
                }
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CategoryId"] = new SelectList(_categoryService.GetList(), "Id", "Name", product.CategoryId);
            ViewBag.Stores = new MultiSelectList(_storeService.GetList(), "Id", "Name", product.StoreIds);
            return View(product);
        }

        private bool? UpdateImage(Product entity, IFormFile uploadedFile)
        {
            #region Image Validation
            bool? result = null;
            string uploadedFileExtension = null;
            if (uploadedFile != null && uploadedFile.Length > 0) // yüklenen imaj verisi varsa
            {
                uploadedFileExtension = Path.GetExtension(uploadedFile.FileName);
                result = FileUtil.CheckFileExtension(uploadedFileExtension, AppSettings.AcceptedImageExtensions).IsSuccessful;
                if (result == true) // eğer imaj uzantısı validasyonunu geçtiyse imaj boyutunu valide edelim
                {
                    // 1 byte = 8 bits
                    // 1 kilobyte = 1024 bytes
                    // 1 megabyte = 1024 kilobytes = 1024 * 1024 bytes = 1.048.576 bytes
                    result = FileUtil.CheckFileLength(uploadedFile.Length, AppSettings.AcceptedImageMaximumLength).IsSuccessful;
                }
            }
            #endregion

            #region Dosyanın kaydedilmesi
            if (result == true)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    uploadedFile.CopyTo(memoryStream);
                    entity.Image = memoryStream.ToArray();
                    entity.ImageExtension = uploadedFileExtension;
                }
            }
            #endregion

            return result;
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Product product = _productService.GetItem(id);
            if (product == null)
            {
                return NotFound();
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CategoryId"] = new SelectList(_categoryService.GetList(), "Id", "Name", product.CategoryId);
            ViewBag.Stores = new MultiSelectList(_storeService.GetList(), "Id", "Name", product.StoreIds);
            return View(product);
        }

        // POST: Products/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (UpdateImage(product, image) == false) // imaj uzantı ve boyut validasyonlarını geçememiş demektir
                {
                    ModelState.AddModelError("", $"Image could not be uploaded: Accepted image extensions are {AppSettings.AcceptedImageExtensions} and accepted image maximum length is {AppSettings.AcceptedImageMaximumLength}!");
                }
                else
                {
                    var result = _productService.Update(product);
                    if (result.IsSuccessful)
                    {
                        TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", result.Message);
                }
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CategoryId"] = new SelectList(_categoryService.GetList(), "Id", "Name", product.CategoryId);
            ViewBag.Stores = new MultiSelectList(_storeService.GetList(), "Id", "Name", product.StoreIds);
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Login", "Home", new { area = "Accounts" });
            Product product = _productService.GetItem(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Delete
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    if (!User.IsInRole("Admin"))
        //        return RedirectToAction("Login", "Home", new { area = "Accounts" });
        //    _productService.Delete(p => p.Id == id);
        //    return RedirectToAction(nameof(Index));
        //}

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteItem(int id)
        {
            TempData["Message"] = _productService.Delete(p => p.Id == id).Message;
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteImage(int id)
        {
            _productService.DeleteImage(id);
            return RedirectToAction(nameof(Details), new { id = id });
        }

        [AllowAnonymous]
        public IActionResult DownloadImage(int id)
        {
            Product product = _productService.GetItem(id);
            if (product == null)
            {
                return NotFound();
            }
            string fileName = "Product" + product.ImageExtension;
            return File(product.Image, FileUtil.GetContentType(product.ImageExtension), fileName);
        }
    }
}
