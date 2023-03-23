using api.Model;
using api.Repositories.category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Controllers.category
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Create Category. Only users with ADMIN privileges can make this request.
        /// </summary>
        /// <param name="category"></param>
        [HttpPost]
        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createCategory = await _categoryRepository.Post(category);
            return CreatedAtAction(nameof(CreateCategory), new { id = createCategory.Id }, createCategory);
        }
    }
}
