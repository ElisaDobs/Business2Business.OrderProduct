using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.B2BDAL;
using Business2Business.Common.Users.Models;
using Business2Business.Common.Users.Interfaces;

namespace Business2Business.Common.Users.Repository
{
    public class UserRepository : IUserRepository
    {
        private B2BDBContext _dbContext;
        public UserRepository(B2BDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddUser(User user)
        {
            user.DateActioned = DateTime.Now;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        public UserRole? ValidateUser(UserCredentials credentials)
        {
            var user = _dbContext.Users.Where(_user => _user.Email == credentials.Username && _user.Password == credentials.Password)
                                       .Join(_dbContext.Roles,
                                            _user => _user.RoleId,
                                            _role => _role.RoleId,
                                            (_user, _role) => new UserRole()
                                            {
                                                UserId = _user.UserId,
                                                FirstName = _user.FirstName,
                                                LastName = _user.LastName,
                                                Username = _user.Email,
                                                RoleId = _role.RoleId,
                                                RoleName = _role.RoleName
                                            }
                                        ).SingleOrDefault();
            return user;
        }
    }
}
