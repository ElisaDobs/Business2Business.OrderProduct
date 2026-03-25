using Microsoft.AspNetCore.Mvc;
using Business2Business.Common.Users.Models;
using Business2Business.Common.Authentication.Interfaces;
using Business2Business.Common.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Business2Business.OrderProduct.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IB2BAuthenticationManager _authenticationMgr;
        private readonly IUserRepository _userRepo;
        public UsersController(IB2BAuthenticationManager authenticationMgr, IUserRepository userRepo)
        {
            _authenticationMgr = authenticationMgr;
            _userRepo = userRepo;
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult AuthorizeUser(UserCredentials userCredentials)
        {
            UserRole? user = _userRepo.ValidateUser(userCredentials);
            if (user != null)
            {
                var token = _authenticationMgr.Authenticate(user);
                if (token == null)
                {
                    return Unauthorized();
                }
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
