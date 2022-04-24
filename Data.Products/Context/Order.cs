using System;
using System.Collections.Generic;

namespace Data.Products.Context
{
    public partial class Order
    {
        public long OrderId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedDateUtc { get; set; }
        public int Quantity { get; set; }
        public int OrderStateId { get; set; }

        public virtual OrderState OrderState { get; set; } = null!;
    }
}
