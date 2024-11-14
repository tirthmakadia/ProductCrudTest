using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCrud.Domain.Entities.ProductModels;
using ProductCrud.Service.Interfaces;

namespace ProductCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var response = products.Select(p => new ProductResponse
            {
                ProductId = p.ProductId,
                Name = p.Name,
                SKU = p.SKU,
                CategoryName = p.Category?.CategoryName,
                BasePrice = p.BasePrice,
                MRP = p.MRP,
                Description = p.Description,
                Currency = p.Currency,
                ManufacturedDate = p.ManufacturedDate,
                ExpireDate = p.ExpireDate
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var response = new ProductResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                SKU = product.SKU,
                CategoryName = product.Category?.CategoryName,
                BasePrice = product.BasePrice,
                MRP = product.MRP,
                Description = product.Description,
                Currency = product.Currency,
                ManufacturedDate = product.ManufacturedDate,
                ExpireDate = product.ExpireDate
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
        {
            if (request == null)
                return BadRequest();

            

            var product = new Product
            {
                Name = request.Name,
                //SKU = request.SKU,
                SKU = GenerateUniqueSKU(),
                CategoryId = request.CategoryId,
                BasePrice = request.BasePrice,
                MRP = request.MRP,
                Description = request.Description,
                Currency = request.Currency,
                ManufacturedDate = request.ManufacturedDate,
                ExpireDate = request.ExpireDate
            };
            try
            {
                var createdProduct = await _productService.CreateProductAsync(product);

                var response = new ProductResponse
                {
                    ProductId = createdProduct.ProductId,
                    Name = createdProduct.Name,
                    SKU = createdProduct.SKU,
                    CategoryName = createdProduct.Category?.CategoryName,
                    BasePrice = createdProduct.BasePrice,
                    MRP = createdProduct.MRP,
                    Description = createdProduct.Description,
                    Currency = createdProduct.Currency,
                    ManufacturedDate = createdProduct.ManufacturedDate,
                    ExpireDate = createdProduct.ExpireDate
                };

                return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductId }, response);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest request)
        {
            if (id <= 0 || request == null)
                return BadRequest();

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            product.Name = request.Name;
            product.SKU = request.SKU;
            product.CategoryId = request.CategoryId;
            product.BasePrice = request.BasePrice;
            product.MRP = request.MRP;
            product.Description = request.Description;
            product.Currency = request.Currency;
            product.ManufacturedDate = request.ManufacturedDate;
            product.ExpireDate = request.ExpireDate;

            var updatedProduct = await _productService.UpdateProductAsync(product);

            var response = new ProductResponse
            {
                ProductId = updatedProduct.ProductId,
                Name = updatedProduct.Name,
                SKU = updatedProduct.SKU,
                CategoryName = updatedProduct.Category?.CategoryName,
                BasePrice = updatedProduct.BasePrice,
                MRP = updatedProduct.MRP,
                Description = updatedProduct.Description,
                Currency = updatedProduct.Currency,
                ManufacturedDate = updatedProduct.ManufacturedDate,
                ExpireDate = updatedProduct.ExpireDate
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        string GenerateUniqueSKU()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var sku = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
            return sku;
        }

    }
}
