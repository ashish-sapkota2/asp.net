using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.API.Interface;
using Test.API.Models;

namespace Test.API.Services
{
    public class TokenService :ITokenService
    {
        private readonly SymmetricSecurityKey Key;

        public TokenService(IConfiguration config)
        {
            this.Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(20),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token= tokenHandler.CreateToken(tokenDescription);

            var tokenString = tokenHandler.WriteToken(token);
            Console.WriteLine($"Generated token for user {user.UserName}: {tokenString}");
            return tokenHandler.WriteToken(token);
        }
    }
}
