using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business2Business.Common.Users.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [Column("RoleId")]
        public int RoleId { get; set; }
        [Column("RoleName")]
        public string? RoleName { get; set; }
    }
}
