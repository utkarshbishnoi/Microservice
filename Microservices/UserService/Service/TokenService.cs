using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Models;

namespace UserService.Service
{
    public class TokenService
    {   private readonly JwtOptions _jwtOptions;
        public TokenService(IOptions<JwtOptions> jwtOptions) 
        {
            _jwtOptions = jwtOptions.Value;
        }   
        public string GenerateToken(User user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.Username),
                    new Claim("role", role)
                }),
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
