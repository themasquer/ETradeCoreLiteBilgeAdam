#nullable disable
using DataAccess.Entities;
using DataAccess.Services.CRUD;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        // Add service injections here
        private readonly StoreServiceBase _storeService;

        public StoresController(StoreServiceBase storeService)
        {
            _storeService = storeService;
        }

        // GET: api/Stores
        [HttpGet]
        public IActionResult Get()
        {
            List<Store> storeList = _storeService.GetList();
            return Ok(storeList);
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Store store = _storeService.GetItem(id);
			if (store == null)
            {
                return NotFound();
            }
			return Ok(store);
        }

		// POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(Store store)
        {
            if (ModelState.IsValid)
            {
                var result = _storeService.Add(store);
                if (result.IsSuccessful)
                {
			        //return CreatedAtAction("Get", new { id = store.Id }, store);
                    return Ok(store);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(Store store)
        {
            if (ModelState.IsValid)
            {
                var result = _storeService.Update(store);
                if (result.IsSuccessful)
                {
			        //return NoContent();
                    return Ok(store);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _storeService.Delete(m => m.Id == id);
            if (result.IsSuccessful)
            {
                //return NoContent();
                return Ok(id);
            }
            ModelState.AddModelError("", result.Message);
            return BadRequest(ModelState);
        }
	}
}
