using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.Products.Models;
using Business2Business.Common.Products.Interfaces;
using Business2Business.OrderProduct.Services.Products.Interfaces;

namespace Business2Business.OrderProduct.Services.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo; 
        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public IEnumerable<Product> GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            return _productRepo.GetAllProducts(pageNumber, pageSize);
        }
    }
}
