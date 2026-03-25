using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Claims;
using Business2Business.Common.Users.Models;
using Business2Business.Common.Authentication.Interfaces;
using Microsoft.IdentityModel.Protocols;

namespace Business2Business.Common.Authentication.BusinessLogic
{
    public class B2BAuthenticationManager : IB2BAuthenticationManager
    {
        private readonly string? _issuer;
        private readonly string? _audience;
        public B2BAuthenticationManager(string? issuer, string? audience)
        {
            _issuer = issuer;
            _audience = audience;
        }
        public string? Authenticate(UserRole user)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken? token = null;
            RSA rsa = RSA.Create(2048);
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.RoleName)
                }),
                Issuer = _issuer,
                Audience = _audience,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            };
            token = handler.CreateToken(securityTokenDescriptor);
            string? tokenString = handler.WriteToken(token);
            rsa.Dispose();

            return tokenString;
        }
    }
}
