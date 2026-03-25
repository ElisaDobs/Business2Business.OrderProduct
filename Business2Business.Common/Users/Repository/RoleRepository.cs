using Business2Business.Common.B2BDAL;
using Business2Business.Common.Users.Interfaces;
using Business2Business.Common.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business2Business.Common.Users.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private B2BDBContext _dbContext;
        public RoleRepository(B2BDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Role> GetAllRoles()
        {
            var roles = _dbContext.Roles;

            return roles.OrderBy(_role => _role.RoleId).ToList();
        }
        public IEnumerable<RoleFunction> GetAllFunctions()
        {
            var functions = _dbContext.RoleFunctions;

            return functions.OrderBy(_function => _function.FunctionId).ToList();
        }
        public IEnumerable<RoleFunction> GetRoleFunctionsByRoleId(int roleId)
        {
            var functions = _dbContext.RoleFunctions.Where(_function => _function.RoleId == roleId);

            return functions.OrderBy(_function => _function.FunctionId).ToList();
        }
    }
}
