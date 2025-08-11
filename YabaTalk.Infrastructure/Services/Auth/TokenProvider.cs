using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using YabaTalk.Application.Dtos.Auth;

namespace YabaTalk.Infrastructure.Services.Auth
{
    public sealed class TokenProvider(IConfiguration configuration)
    {
        public string Create(TokenUserDto user)
        {
            string secretKey = configuration["JWT:Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                  new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneE164),
                ]),
                Expires = DateTime.UtcNow.AddYears(configuration.GetValue<int>("Jwt:Expiration")),
                SigningCredentials = credentials,
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"]
            };

            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
