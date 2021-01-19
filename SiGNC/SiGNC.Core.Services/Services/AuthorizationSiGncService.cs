using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SiGNC.Core.Services.Interfaces;
using SiGNC.Infra.Data.Dtos.Authorization;
using SiGNC.Infra.Data.Models;
using SiGNC.Infra.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SiGNC.Core.Services.Services
{
    public class AuthorizationSiGncService : IAuthorizationSiGncService
    { 
        private readonly AppSettings _appSettings;
        private Dictionary<string, string> claims = new Dictionary<string, string>();

        public AuthorizationSiGncService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponseDto Authenticate(AuthenticateRequestDto request)
        { 
           
            //TODO: Modificar para o banco de dados.
            var user = new Usuario()
            {
                Id = 10,
                Nome = "Cleidson",
                Email = request.Email
            };

            if (user == null) return null;

            //claims.Add("Administrador", "admin");

            string token = generateJwtToken(user, claims);

            return new AuthenticateResponseDto(user, token); 
        }

        public int GetById(int userId)
        {
            return 1;
        }

        private string generateJwtToken(Usuario user, Dictionary<string, string> claims)
        {

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SecretKey));

            //var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var claims1 = new[] { new Claim(ClaimTypes.Email, user.Email) };
            //var token = new JwtSecurityToken(
            //         issuer: "teste",
            //         audience: "teste",
            //         claims: claims1,
            //         expires: DateTime.UtcNow.AddDays(7),
            //         signingCredentials: credential);

            //return new JwtSecurityTokenHandler().WriteToken(token);

            // generate token that is valid for 7 days 

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "SiGN.Api",
                Audience = "SiGN.Api",
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),

                //Claims = (IDictionary<string, object>)claims,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
