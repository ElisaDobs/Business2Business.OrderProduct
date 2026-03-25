using Microsoft.AspNetCore.Mvc;
using Business2Business.Common.Orders.Models;
using Business2Business.Common.Orders.DTO;
using Business2Business.Common.Orders.Interfaces;
using Business2Business.Common.ErrorLoggers.Interfaces;
using Business2Business.Common.Products.Models;
using Business2Business.Common.Products.Interfaces;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Business2Business.OrderProduct.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderProductRepository _orderProductRepo;
        private readonly ILoggerRepository _loggerRepo;
        public OrderController(IProductRepository productRepo, 
                               IOrderRepository orderRepo, 
                               IOrderProductRepository orderProductRepo, 
                               ILoggerRepository loggerRepo) 
        {
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _orderProductRepo = orderProductRepo;
            _loggerRepo = loggerRepo;
        }
        [Authorize(Roles = "Customer")]
        [HttpPost("SubmitOrderProduct")]
        public ActionResult SubmitOrderProduct(Guid useId, int productId, int productQuantity)
        {
            bool processed = true;
            string resultMessage = "Order processed sucessfully";
           try
            {
                int orderId = _orderRepo.AddOrder(useId);
                Common.Products.Models.Product? product = _productRepo.GetProductByProductId(productId);
                if (product == null)
                {
                    throw new Exception("The product is not found.");
                }
                else
                {
                    if (product.ProductUnitQuantity < productQuantity)
                    {
                        throw new Exception(string.Format("There's only {0} of {1}", product.ProductUnitQuantity, product.ProductName));
                    }
                    _orderProductRepo.ProcessProductOrder(orderId, productId, product.ProductPrice, productQuantity);
                    product.ProductUnitQuantity = product.ProductUnitQuantity - productQuantity;
                    _productRepo.UpdateProduct(product, useId);
                }
            }
            catch(Exception exception)
            {
                processed = false;
                resultMessage = exception.Message;
                _loggerRepo.WriteMessage("Order", "SubmitOrderProduct", exception.ToString(), useId.ToString());
            }
            if (processed)
            {
                return Ok(resultMessage);
            }
            else
            {  
                return BadRequest(resultMessage); 
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllOrders")]
        public IEnumerable<OrderProductDTO>? GetAllOrders(int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<OrderProductDTO>? orders = null;
            try
            {
                orders = _orderProductRepo.GetAllOrders(pageNumber, pageSize);
            }
            catch(Exception exception)
            {
                _loggerRepo.WriteMessage("Order", "GetAllOrders", exception.ToString());
            }
            return orders;
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("GetOrderProductByUserId")]
        public IEnumerable<OrderProductDTO>? GetOrderProductByUserId(Guid userId)
        {
            IEnumerable<OrderProductDTO>? orderProducts = null;
            try
            {
                IEnumerable<Common.Orders.Models.Order> orders = _orderRepo.GetOrderByUserId(userId);
                orderProducts = orders.Select(_order => new OrderProductDTO()
                {
                    OrderProduct = _order,
                    OrderProducts = _orderProductRepo.GetProductOrderByOrderId(_order.OrderId)
                });
            }
            catch (Exception exception)
            {
                _loggerRepo.WriteMessage("Order", "GetOrderProductByUserId", exception.ToString(), userId.ToString());
            }
            return orderProducts;
        }
    }
}
