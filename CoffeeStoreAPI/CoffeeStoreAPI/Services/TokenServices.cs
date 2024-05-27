using CoffeeStoreAPI.Iterfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeeStoreAPI.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly string _secretKey;
        private readonly SymmetricSecurityKey _key;

        public TokenServices(IConfiguration configuration)
        {
            _secretKey = configuration.GetSection("TokenKey").GetSection("JWT").Value.ToString();
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        }
        public string GenerateToken(int UserId, string Role)
        {
            string token = string.Empty;
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Name,UserId.ToString()),
                new Claim(ClaimTypes.Role, Role.ToString())
            };
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var myToken = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddDays(2), signingCredentials: credentials);
            token = new JwtSecurityTokenHandler().WriteToken(myToken);
            return token;
        }
    }
}
