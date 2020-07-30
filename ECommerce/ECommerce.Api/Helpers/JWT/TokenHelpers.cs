using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using ECommerce.Entity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Api.JWT
{
    public class TokenHelpers
    {
        public IOptions<appSettings_json> _configuration { get; }

        public TokenHelpers(IOptions<appSettings_json> configuration)
        {
            _configuration = configuration;
        }


        public TokenResultModel GenerateToken(User user)
        {

            DateTime finisDate = DateTime.Now.AddDays(10);

            var claims = new[]{
                new Claim(ECommerceClaimTypes.UserId,user.Id.ToString()),
                new Claim(ECommerceClaimTypes.Email,user.Email),
                new Claim(ECommerceClaimTypes.Role,user.Role.ToString())
            };

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.JWTKey_SigningKey));
            var token = new JwtSecurityToken(
                issuer: _configuration.Value.JWTKey_Issuer,
                audience: _configuration.Value.JWTKey_Audience,
                claims: claims,
                expires: finisDate,
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new TokenResultModel
            {
                token_type = "Bearer",
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expires_in_date = finisDate
            };
        }

        public string GenerateVerificationToken(string email)
        {
            DateTime finisDate = DateTime.Now.AddDays(1);

            var claims = new[]{
                new Claim(ECommerceClaimTypes.Email,email),
            };

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.JWTKey_SigningKey));
            var token = new JwtSecurityToken(
                issuer: _configuration.Value.JWTKey_Issuer,
                audience: _configuration.Value.JWTKey_Audience,
                claims: claims,
                expires: finisDate,
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
