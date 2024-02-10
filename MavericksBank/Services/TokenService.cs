using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MavericksBank.Interfaces;
using MavericksBank.Models.DTO;
using Microsoft.IdentityModel.Tokens;

namespace MavericksBank.Services
{
	public class TokenService:ITokenService
    {
        private readonly string _keyString;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _keyString = configuration["SecretKey"].ToString();
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_keyString));
        }
        public async Task<string> GenerateToken(CustomerLoginDTO user)
        {
            string token = string.Empty;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName),
                new Claim(ClaimTypes.Role,user.UserType)
            };
            //Algorithm Signature with secret key
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            //Giving the token decription
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };

            //Putting the token together
            var tokenHandler = new JwtSecurityTokenHandler();
            var myToken = tokenHandler.CreateToken(tokenDescription);
            token = tokenHandler.WriteToken(myToken);
            return token;
        }
    }
}

