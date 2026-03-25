using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.Orders.Models;

namespace Business2Business.Common.Orders.Interfaces
{
    public interface IOrderRepository
    {
        int AddOrder(Guid userId);
        IEnumerable<Order> GetOrderByUserId(Guid userId);
    }
}
