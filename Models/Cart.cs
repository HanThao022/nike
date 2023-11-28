using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nike.Models
{
    [Serializable]

    public class Cart
    {
        public Product Product { get; set; }
        public float Price {
            get
            {
                return (float)Product.Price;
            }
        }
        public int Quantity { get; set; }
        public float Total
        {
            get
            {
                return Price * Quantity;
            }
        }
        public float TotalAmount { get; set; }
    }
}