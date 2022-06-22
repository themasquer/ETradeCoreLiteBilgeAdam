#nullable disable
using DataAccess.Entities;
using DataAccess.Services.CRUD;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Add service injections here
        private readonly ProductServiceBase _productService;

        public ProductsController(ProductServiceBase productService)
        {
            _productService = productService;
        }

        // GET: api/Products/ListProducts
        [HttpGet("ListProducts")]
        public IActionResult ListProducts()
        {
            List<Product> productList = _productService.GetList();
            return Ok(productList);
        }

        // GET: api/Products/ProductDetails/5
        [HttpGet("ProductDetails/{productId?}")]
        public IActionResult ProductDetails(int? productId)
        {
            if (!productId.HasValue)
                return NotFound();
            Product product = _productService.GetItem(productId.Value);
			if (product == null)
            {
                return NotFound();
            }
			return Ok(product);
        }

		// POST: api/Products/CreateProduct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var result = _productService.Add(product);
                if (result.IsSuccessful)
                    return Ok(product);
                ModelState.AddModelError("", result.Message);
            }
            return StatusCode(500, ModelState);
        }

        // PUT: api/Products/UpdateProduct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var result = _productService.Update(product);
                if (result.IsSuccessful)
                    return Ok(product);
                ModelState.AddModelError("", result.Message);
            }
            return StatusCode(500, ModelState);
        }

        // DELETE: api/Products/DeleteProduct/5
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _productService.Delete(p => p.Id == id);
            return Ok(id);
        }
	}
}
