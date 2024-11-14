using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCrud.Domain.Entities.enums;
using ProductCrud.Domain.Entities.CategoryModel;

namespace ProductCrud.Domain.Entities.ProductModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "Name cannot exceed 256 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "SKU must be 6 characters.")]
        public string SKU { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

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
        //[FutureDate(ErrorMessage = "Expire Date must be a future date.")]
        public DateTime ExpireDate { get; set; }


    }
}
