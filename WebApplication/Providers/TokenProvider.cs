using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Models.Entities;

namespace WebApplication.Providers
{
    public class TokenProvider(IConfiguration configuration)
    {
        public string CreateToken(Users user)
        {
            string? secretKey = configuration["JWT:Secret"];

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("JWT:Secret is missing in configuration!");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                        new Claim(ClaimTypes.Role, "User")
                    ]
                ),
                Expires = DateTime.UtcNow.AddHours(configuration.GetValue<int>("JWT:Expiration")),
                SigningCredentials = creds,
                Issuer = configuration["JWT:Isser"],
                Audience = configuration["JWT:Audience"]
            };

            var handle = new JsonWebTokenHandler();

            return handle.CreateToken(tokenDescriptor);
        }
    }
}
