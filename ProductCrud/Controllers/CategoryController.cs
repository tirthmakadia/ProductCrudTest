using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCrud.Domain.Entities.CategoryModel;
using ProductCrud.Service.Interfaces;

namespace ProductCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var response = categories.Select(c => new CategoryResponse
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                IsActive = c.IsActive
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            var response = new CategoryResponse
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                IsActive = category.IsActive
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            if (request == null)
                return BadRequest();

            var category = new Category
            {
                CategoryName = request.CategoryName,
                IsActive = request.IsActive
            };
            try
            {

                var createdCategory = await _categoryService.CreateCategoryAsync(category);

                var response = new CategoryResponse
                {
                    CategoryId = createdCategory.CategoryId,
                    CategoryName = createdCategory.CategoryName,
                    IsActive = createdCategory.IsActive
                };
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.CategoryId }, response);
            }
            catch (DbUpdateException ex)
            {
                // Check for duplicate category name (based on unique constraint)
                if (ex.InnerException?.Message.Contains("duplicate key") ?? false)
                {
                    return Conflict("A category with this name already exists.");
                }

                // Return generic server error if the exception is not related to duplication
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            if (id <= 0 || request == null)
                return BadRequest();

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            category.CategoryName = request.CategoryName;
            category.IsActive = request.IsActive;

            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);

            var response = new CategoryResponse
            {
                CategoryId = updatedCategory.CategoryId,
                CategoryName = updatedCategory.CategoryName,
                IsActive = updatedCategory.IsActive
            };

            return Ok(response);
        }   

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
