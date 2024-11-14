﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Domain.Entities.CategoryModel
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }
    }
}
