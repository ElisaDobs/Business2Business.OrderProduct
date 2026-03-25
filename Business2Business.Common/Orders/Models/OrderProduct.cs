using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Business2Business.Common.Orders.Models
{
    [Table("ProductOrder")]
    public class OrderProduct
    {
        [Key]
        [Column("ProductOrderId")]
        public int ProductOrderId { get; set; }
        [Column("OrderId")]
        public int OrderId { get; set; }
        [Column("ProductId")]
        public int ProductId { get; set; }
        [Column("OrderUnitQuantity")]
        public int OrderUnitQuantity { get; set; }
        [Column("OrderProductPrice")]
        public decimal OrderProductPrice { get; set; }
    }
}
