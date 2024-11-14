using ProductCrud.Domain.Entities.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Domain.Entities.ProductModels
{
    public class ProductRequest
    {
        [Required]
        [StringLength(256, ErrorMessage = "Name cannot exceed 256 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "SKU must be 6 characters.")]
        public string SKU { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Base Price must be a positive number.")]
        public decimal BasePrice { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "MRP must be a positive number.")]
        public decimal MRP { get; set; }

        public string Description { get; set; }

        [Required]
        public CurrencyType Currency { get; set; }

        [Required]
        public DateTime ManufacturedDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }
    }
}
