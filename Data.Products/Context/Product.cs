using System;
using System.Collections.Generic;

namespace Data.Products.Context
{
    public partial class Product
    {
        public Product()
        {
            Stocks = new HashSet<Stock>();
        }

        public long ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
