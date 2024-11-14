using ProductCrud.Domain.Entities.CategoryModel;
using ProductCrud.Repository.Interfaces;
using ProductCrud.Repository.Repository;
using ProductCrud.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var existingCategory = await _categoryRepository.GetByNameAsync(category.CategoryName);
            if (existingCategory != null)
            {
                throw new InvalidOperationException("A category with this name already exists.");
            }
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            // Check if there are products associated with the category
            var products = await _productRepository.GetByCategoryIdAsync(id);
            if (products.Any())
            {
                // Prevent category deletion if products exist
                return false;
            }
            return await _categoryRepository.DeleteAsync(id);
        }
    }
}
