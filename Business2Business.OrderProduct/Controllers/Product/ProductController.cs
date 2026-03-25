using Business2Business.Common.ErrorLoggers.Interfaces;
using Business2Business.Common.Products.Interfaces;
using Business2Business.Common.Products.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Business2Business.OrderProduct.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly ILoggerRepository _loggerRepo;
        public ProductController(IProductRepository productRepo, ILoggerRepository loggerRepo)
        {
            _productRepo = productRepo;
            _loggerRepo = loggerRepo;
        }
        [Authorize]
        [HttpGet("GetAllProducts")]
        public IEnumerable<Common.Products.Models.Product>? GetAllProducts(int pageNumber = 1, int pageSize = 10, string field = "", string searchItem = "")
        {
            IEnumerable<Common.Products.Models.Product>? products = null;
            try
            {
                products = _productRepo.GetAllProducts(pageNumber, pageSize, field, searchItem);
            }
            catch (Exception exception)
            {
                _loggerRepo.WriteMessage("Product", "GetAllProduct", exception.ToString());
            }
            return products;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddProduct")]
        public ActionResult AddProduct(Common.Products.Models.Product product)
        {
            bool added = true;
            string resultMessage = "Product added successfully";
            try
            {
                _productRepo.AddProduct(product);
            }
            catch(Exception exception)
            {
                added = false;
                resultMessage = exception.Message;
                _loggerRepo.WriteMessage("Product", "AddProduct", exception.ToString(), product.ActionedById.ToString());
            }
            if (added)
            {
                return Ok(resultMessage);
            }
            else
            {
                return BadRequest(resultMessage);
            }
        }
    }
}
