using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business2Business.Common.Products.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Column]
        public int ProductId { get; set; }
        [Column("ProductName")]
        public string? ProductName { get; set; }
        [Column("ProductSku")]
        public string? ProductSku { get; set; }
        [Column("ProductPrice")]
        public decimal ProductPrice { get; set; }
        [Column("ProductUnitQunatity")]
        public int? ProductUnitQuantity { get; set; }
        [Column("ProductCategory")]
        public string? ProductCategory { get; set; }
        [Column("ActionedById")]
        public Guid ActionedById { get; set; }
        [Column("DateActioned")]
        public DateTime DateActioned { get; set; }
    }
}
