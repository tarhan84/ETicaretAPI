﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Models.Products
{
    public class UpdateProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
    }
}
