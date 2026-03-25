using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Business2Business.Common.ErrorLoggers.Models;
using Business2Business.Common.Orders.Models;
using Business2Business.Common.Products.Models;
using Business2Business.Common.Users.Models;

namespace Business2Business.Common.B2BDAL
{
    public class B2BDBContext : DbContext
    {
        private string? _sqlConnString;
        private static B2BDBContext? b2bDbInstance;
        private B2BDBContext(string? sqlConnString)
        {
            _sqlConnString = sqlConnString;
        }
        public static B2BDBContext? CreateInstance(string? sqlConnString)
        {
            if(b2bDbInstance is null)
            {
                b2bDbInstance = new B2BDBContext(sqlConnString);
            }

            return b2bDbInstance;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_sqlConnString);
        }

        public virtual DbSet<ErrorMessage> ErrorMessages { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleFunction> RoleFunctions { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
