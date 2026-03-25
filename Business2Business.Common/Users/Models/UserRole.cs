using System;
using System.Collections.Generic;
using System.Text;

namespace Business2Business.Common.Users.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
