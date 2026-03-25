using System;
using System.Collections.Generic;
using System.Text;

namespace Business2Business.Common.Orders.Models
{
    public class ProductOrder
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductSku { get; set; }
        public int? OrderUnitQuantity { get; set; }
        public decimal? OrderUnitPrice { get; set; }
        public string? ProductCategory { get; set; }
    }
}
