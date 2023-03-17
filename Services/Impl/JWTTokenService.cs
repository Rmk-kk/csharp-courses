using System.Linq.Expressions;
using System.Text;
using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetCoreCourse.Services
{
    public class JWTTokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        public JWTTokenService(IConfiguration config, UserManager<User> userManager) 
        {
            _config = config;
            _userManager = userManager;
        }
        public async Task<UserSignInResponseDTO> GenerateTokenAsync(User user)
        {
            //Payload
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            };

            //Secret
            var secret = _config["Jwt:Secret"];
            var signingKey = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                SecurityAlgorithms.HmacSha256
            );
            
            //Expiration
            var expirationDate = DateTime.Now.AddHours(1);
            // (string issuer = null, 
            // string audience = null, 
            // IEnumerable<Claim> claims = null,
            // DateTime? notBefore = null, 
            // DateTime? expires = null, 
            // SigningCredentials signingCredentials = null)
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"], 
                _config["Jwt:Audience"],
                claims, 
                expires: expirationDate, 
                signingCredentials: signingKey
                );

            var tokenWriter = new JwtSecurityTokenHandler();

            var result = new UserSignInResponseDTO()
            {
                Token = tokenWriter.WriteToken(token),
                Expiration = expirationDate
            };

            return result;
        }
    }
}