using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Apivscode2.Models;
using Microsoft.IdentityModel.Tokens;

namespace Apivscode2.Services
{
    public static class TokenServices
    {
        public static string GenerateToken(FilmesResponse Filmes)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.secret);
            var tokeDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, Filmes.Nome),
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokeDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
