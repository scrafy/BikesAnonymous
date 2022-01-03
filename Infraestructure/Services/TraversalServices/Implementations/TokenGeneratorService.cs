using Core.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TraversalServices.Interfaces;
using TraversalServices.Models;

namespace TraversalServices.Implementations
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private IOptions<TokenSettings> _tokenSettings;


        public TokenGeneratorService(IOptions<TokenSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings;
        }

        public string CreateToken(string secret, ROLE role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, role.ToString())                 
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Value.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
;