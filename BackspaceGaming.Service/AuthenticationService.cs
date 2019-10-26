using BackspaceGaming.Entity.Model;
using BackspaceGaming.Repository;
using BackspaceGaming.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackspaceGaming.Service
{
    public class AuthenticationService : ServiceBase<Authentication>, IAuthenticationService
    {
        private readonly IAuthenticationRepository _repository;
        private readonly IConfiguration _configuration;
        public AuthenticationService(IAuthenticationRepository repository, IConfiguration configuration) : base(repository)
        {
            this._repository = repository;
            this._configuration = configuration;
        }

        public async Task<dynamic> GetJWTToken(AuthenticationBodyModel authenticationBody)
        {
            var token = GenerateToken(new Authentication { });
            
            return new {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }

        private JwtSecurityToken GenerateToken(Authentication authentication)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt_Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            IEnumerable<Claim> claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Sub, email, JwtSecurityTokenHandler.JsonClaimTypeProperty),
                //new Claim(JwtRegisteredClaimNames.UniqueName, userName, JwtSecurityTokenHandler.JsonClaimTypeProperty),
                //new Claim("UserId", userId, JwtSecurityTokenHandler.JsonClaimTypeProperty),
                //new Claim("UserName", userName, JwtSecurityTokenHandler.JsonClaimTypeProperty),
                //new Claim("UserFullName", userFullName, JwtSecurityTokenHandler.JsonClaimTypeProperty),
                //new Claim("Email", email, JwtSecurityTokenHandler.JsonClaimTypeProperty),
                new Claim("PSK", "ANQH4P3WD3BBI5KE", JwtSecurityTokenHandler.JsonClaimTypeProperty),
                //new Claim("Rights", rightids, JwtSecurityTokenHandler.JsonClaimTypeProperty),
                //new Claim("TimeZoneId", timezoneId, JwtSecurityTokenHandler.JsonClaimTypeProperty),
               
         
            };
            var token = new JwtSecurityToken(_configuration["service_base"], _configuration["origins"], claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: credentials);
           

            return token;
        }
    }
}
