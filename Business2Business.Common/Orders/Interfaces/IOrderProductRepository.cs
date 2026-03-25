using Business2Business.Common.Orders.DTO;
using Business2Business.Common.Orders.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business2Business.Common.Orders.Interfaces
{
    public interface IOrderProductRepository
    {
        void ProcessProductOrder(int orderId, int productId, decimal productUnitPrice, int productQuantity);
        IEnumerable<OrderProductDTO> GetAllOrders(int pageNumber, int pageSize);
        IEnumerable<ProductOrder> GetProductOrderByOrderId(int orderId);
    }
}
