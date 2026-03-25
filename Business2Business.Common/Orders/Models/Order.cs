using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Business2Business.Common.Orders.Models
{
    [Table("Order")]
    public class Order
    {
        [Column("OrderId")]
        public int OrderId { get; set; }
        [Column("OrderedById")]
        public Guid OrderedById { get; set; }
        [Column("DateActioned")]
        public DateTime DateActioned { get; set; }
    }
}
