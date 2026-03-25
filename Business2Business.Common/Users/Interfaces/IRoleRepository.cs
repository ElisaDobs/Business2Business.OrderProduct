using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.Users.Models;

namespace Business2Business.Common.Users.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();
        IEnumerable<RoleFunction> GetAllFunctions();
        IEnumerable<RoleFunction> GetRoleFunctionsByRoleId(int roleId);
    }
}
