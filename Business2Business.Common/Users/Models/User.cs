using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Business2Business.Common.Users.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column]
        public Guid UserId { get; set; }
        [Column("FirstName")]
        public string? FirstName { get; set; }
        [Column("LastName")]
        public string? LastName { get; set; }
        [Column("Email")]
        public string? Email { get; set; }
        [Column("Password")]
        public string? Password { get; set; }
        [Column("Gender")]
        public string? Gender { get; set; }
        [Column("RoleId")]
        public int RoleId { get; set; }
        [Column("DateActioned")]
        public DateTime DateActioned { get; set; }
    }
}
