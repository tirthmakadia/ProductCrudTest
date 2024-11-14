using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Domain.Entities.CategoryModel
{
    public class CategoryRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Category Name cannot exceed 100 characters.")]
        public string CategoryName { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
