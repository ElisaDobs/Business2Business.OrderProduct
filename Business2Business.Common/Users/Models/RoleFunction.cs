using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Business2Business.Common.Users.Models
{
    public class RoleFunction
    {
        [Key]
        [Column("FunctionId")]
        public int FunctionId { get; set; }
        [Column("FunctionName")]
        public string? FunctionName { get; set; }
        [Column("RoleId")]
        public int? RoleId { get; set; }
    }
}
