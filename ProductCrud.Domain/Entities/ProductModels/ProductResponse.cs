using ProductCrud.Domain.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Domain.Entities.ProductModels
{
    public class ProductResponse
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string SKU { get; set; }

        public string CategoryName { get; set; }

        public decimal BasePrice { get; set; }

        public decimal MRP { get; set; }

        public string Description { get; set; }

        public CurrencyType Currency { get; set; }

        public DateTime ManufacturedDate { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
