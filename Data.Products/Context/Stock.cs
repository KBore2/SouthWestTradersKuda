using System;
using System.Collections.Generic;

namespace Data.Products.Context
{
    public partial class Stock
    {
        public long StockId { get; set; }
        public long ProductId { get; set; }
        public int AvailableStock { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
