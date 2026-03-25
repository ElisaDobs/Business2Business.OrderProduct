using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.Products.Interfaces;
using Business2Business.Common.Products.Models;
using Business2Business.Common.B2BDAL;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Business2Business.Common.Products.Repository
{
    public class ProductRepository : IProductRepository
    {
        private B2BDBContext _dbContext;
        public ProductRepository(B2BDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Product? GetProductByProductId(int productId)
        {
            var product = _dbContext.Products.SingleOrDefault(_product => _product.ProductId == productId);

            return product;
        }
        public IEnumerable<Product>? GetAllProducts(int pageNumber, int pageSize, string field, string searchItem)
        {
            IEnumerable<Product>? products = null;
            //Filter by provided field
            if (!string.IsNullOrEmpty(field) && !string.IsNullOrEmpty(searchItem))
            {
                products = _dbContext.Products.Where(_product => EF.Property<string>(_product, field) == searchItem);
            }
            //Sort by provided field
            if (!string.IsNullOrEmpty(field) && string.IsNullOrEmpty(searchItem))
            {
                products = _dbContext.Products.OrderBy(_product => EF.Property<string>(_product, field));
            }

            if (products != null && products.Any())
            {
                var skipNumber = (pageNumber - 1) * pageSize;

                return products.Skip(skipNumber)
                               .Take(pageSize)
                               .ToList();
            }
            return null;
        }
        public void AddProduct(Product product)
        {
            product.DateActioned = DateTime.Now;
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }
        public void UpdateProduct(Product product, Guid userId)
        { 
            product.ActionedById = userId;
            product.DateActioned = DateTime.Now;
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }
    }
}
