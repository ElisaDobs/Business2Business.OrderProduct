using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.Products.Models;

namespace Business2Business.Common.Products.Interfaces
{
    public interface IProductRepository
    {
        Product? GetProductByProductId(int productId);
        IEnumerable<Product>? GetAllProducts(int pageNumber, int pageSize, string field, string searchItem);
        void AddProduct(Product product);
        void UpdateProduct(Product product, Guid userId);
    }
}
