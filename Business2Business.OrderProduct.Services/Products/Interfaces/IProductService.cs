using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.Products.Models;

namespace Business2Business.OrderProduct.Services.Products.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts(int pageNumber = 1, int pageSize = 10);
    }
}
