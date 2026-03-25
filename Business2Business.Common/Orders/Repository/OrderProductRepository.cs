using Business2Business.Common.B2BDAL;
using Business2Business.Common.Orders.Interfaces;
using Business2Business.Common.Orders.Models;
using Business2Business.Common.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.Orders.DTO;

namespace Business2Business.Common.Orders.Repository
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private B2BDBContext _dbContext;
        public OrderProductRepository(B2BDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void ProcessProductOrder(int orderId, int productId, decimal productUnitPrice, int productQuantity)
        {
            OrderProduct product = new OrderProduct()
            {
                OrderId = orderId,
                ProductId = productId,
                OrderUnitQuantity = productQuantity,
                OrderProductPrice = productUnitPrice
            };
            _dbContext.OrderProducts.Add(product);
            _dbContext.SaveChanges();
        }
        public IEnumerable<OrderProductDTO> GetAllOrders(int pageNumber, int pageSize)
        {
            var orders = _dbContext.Orders.Select(_order => new OrderProductDTO()
            {
                OrderProduct = _order,
                OrderProducts = GetProductOrderByOrderId(_order.OrderId)
            });
            int skipPage = (pageNumber - 1) * pageSize;

            return orders.Skip(skipPage).Take(pageSize).ToList();
        }
        public IEnumerable<ProductOrder> GetProductOrderByOrderId(int orderId)
        {
            IEnumerable<ProductOrder> productOrders = _dbContext.Products
                      .Join(_dbContext.OrderProducts,
                            _product => _product.ProductId,
                            _orderProduct => _orderProduct.ProductId,
                            (_product, _orderProduct) => new ProductOrder()
                            {
                              OrderId = _orderProduct.OrderId,
                              ProductId = _product.ProductId,
                              ProductName = _product.ProductName,
                              OrderUnitQuantity = _orderProduct.OrderUnitQuantity,
                              ProductSku = _product.ProductSku,
                              OrderUnitPrice = _orderProduct.OrderUnitQuantity,
                              ProductCategory = _product.ProductCategory
                            }
                       ).Where(_model => _model.OrderId == orderId);

            return productOrders.ToList();
        }
    }
}
