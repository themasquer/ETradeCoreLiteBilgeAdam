#nullable disable
using DataAccess.Entities;
using DataAccess.Services.CRUD;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // Add service injections here
        private readonly CategoryServiceBase _categoryService;

        public CategoriesController(CategoryServiceBase categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public IActionResult Get()
        {
            List<Category> categoryList = _categoryService.GetList();
            return Ok(categoryList);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = _categoryService.GetItem(id);
			if (category == null)
            {
                return NotFound();
            }
			return Ok(category);
        }

		// POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Add(category);
                if (result.IsSuccessful)
                    return CreatedAtAction("Get", new { id = category.Id }, category);
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Update(category);
                if (result.IsSuccessful)
                    return NoContent();
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _categoryService.Delete(c => c.Id == id);
            if (result.IsSuccessful)
                return NoContent();
            return BadRequest(result.Message);
        }
	}
}
