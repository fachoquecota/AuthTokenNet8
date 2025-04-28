using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Validaciones.Interfaces;
using Validaciones.Models;

namespace Validaciones.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }

        public string GenerateToken(LoginModel loginModel)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginModel.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var securityTokens = new JwtSecurityToken(
                claims :claims, 
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityTokens);
            return token;
        }
    }
}
