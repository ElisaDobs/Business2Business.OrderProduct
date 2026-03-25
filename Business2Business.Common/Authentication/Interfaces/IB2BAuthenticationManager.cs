using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.Users.Models;

namespace Business2Business.Common.Authentication.Interfaces
{
    public interface IB2BAuthenticationManager
    {
        string? Authenticate(UserRole user);
    }
}
