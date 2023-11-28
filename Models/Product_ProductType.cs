using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nike.Models
{
    public class Product_ProductType
    {
        public List<Product> ListProduct { get; set; }
        public List<ProductType> ListProductTypes { get; set; }
    }
}