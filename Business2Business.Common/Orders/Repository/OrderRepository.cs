using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.Orders.Models;
using Business2Business.Common.Orders.Interfaces;
using Business2Business.Common.B2BDAL;

namespace Business2Business.Common.Orders.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private B2BDBContext _dbContext;
        public OrderRepository(B2BDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddOrder(Guid userId)
        {
            Order order = new Order()
            {
                OrderedById = userId,
                DateActioned = DateTime.Now
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return order.OrderId;
        }
        public IEnumerable<Order> GetOrderByUserId(Guid userId)
        {
            var orders = _dbContext.Orders.Where(_order => _order.OrderedById == userId);

            return orders.OrderBy(_order => _order.OrderId).ToList();
        }
    }
}
