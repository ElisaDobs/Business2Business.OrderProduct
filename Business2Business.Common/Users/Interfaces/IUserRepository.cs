using Business2Business.Common.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business2Business.Common.Users.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        UserRole? ValidateUser(UserCredentials credentials);
    }
}
