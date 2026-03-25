using Business2Business.Common.Orders.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business2Business.Common.Orders.DTO
{
    public class OrderProductDTO
    {
        public Order? OrderProduct { get; set; }
        public IEnumerable<ProductOrder>? OrderProducts { get; set; }
    }
}
