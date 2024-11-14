using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCrud.Domain.Entities.ProductModels;

namespace ProductCrud.Domain.Entities.CategoryModel
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Category Name cannot exceed 100 characters.")]
        public string CategoryName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
