using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTWebAPI.Context;
using JWTWebAPI.Interfaces;
using JWTWebAPI.Models;
using JWTWebAPI.Models.RequestModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;

namespace JWTWebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtContext _jwtService;
        private readonly IConfiguration _configuration;
        public AuthService(JwtContext jwtContext, IConfiguration configuration)
        {
            _jwtService = jwtContext;
            _configuration = configuration;
        }
        public User AddUser(User user)
        {
            var Addeduser = _jwtService.Users.Add(user);
            _jwtService.SaveChanges();
            return Addeduser.Entity;

        }

        public string Login(LoginRequest loginRequest)
        {
            if (loginRequest.UserName != null && loginRequest.Password != null)
            {
                var user = _jwtService.Users.SingleOrDefault(s => s.Email == loginRequest.UserName && s.Password == loginRequest.Password);
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["jwt:Subject"]),
                        new Claim("Id",user.Id.ToString()),
                        new Claim("UserName",user.Name),
                        new Claim("Email",user.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires:DateTime.UtcNow.AddMinutes(10),
                        signingCredentials:signIn);
            
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;

                }
                else
                {
                    throw new Exception("user is not valid");
                }
            }
            else
            {
                throw new Exception("credentials are not valid");
            }
        }
    }
}
