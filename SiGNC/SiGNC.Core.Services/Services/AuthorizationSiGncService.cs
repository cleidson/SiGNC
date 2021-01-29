using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SiGNC.Core.Services.DTOs;
using SiGNC.Core.Services.DTOs.Authorization;
using SiGNC.Core.Services.Interfaces;
using SiGNC.Infra.Data.Context; 
using SiGNC.Infra.Data.Models;
using SiGNC.Infra.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Core.Services.Services
{
    public class AuthorizationSiGncService : IAuthorizationSiGncService
    { 
        private readonly AppSettings _appSettings;  
        private readonly SignInManager<ApplicationUser> _signInManager; 
        private readonly ApplicationDbContext _db; 

        public AuthorizationSiGncService(IOptions<AppSettings> appSettings, ApplicationDbContext db, SignInManager<ApplicationUser> signInManager)
        {
            _appSettings = appSettings.Value;
            _db = db;
            _signInManager = signInManager;
        }


        public AuthenticateResponseDto Authenticate(AuthenticateRequestDto request)
        {  
            var user = new Usuario()
            {
                Id = "10",
                Nome = "Cleidson",
                Email = request.Email
            };

            if (user == null) return null; 
            var userToken = generateJwtToken(user); 
            return new AuthenticateResponseDto(user, userToken.Token, userToken.Expiration);
        }



        public async Task<AuthenticateResponseDto> AuthenticateSync(AuthenticateRequestDto request)
        { 
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha,
                 isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded == false) return null;

            var usuario = _db.Users.FirstOrDefault(t => t.Email == request.Email);

            var usuarioRule = _db.UserRoles.FirstOrDefault(t => t.UserId == usuario.Id);
            
            var rule = _db.Roles.FirstOrDefault(t => t.Id == usuarioRule.RoleId);

            var user = new Usuario()
            {
                Id = usuario.Id, //Adiiconar
                Email = request.Email,
                Role = rule.Name 
            };


            //claims.Add("Administrador", "admin");

            var userToken = generateJwtToken(user);

            return new AuthenticateResponseDto(user, userToken.Token, userToken.Expiration);
        }

        public int GetById(int userId)
        {
            return 1;
        }



        private UserToken BuildToken(UserInfo userInfo)
        { 
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email), 
                new Claim(ClaimTypes.Role, userInfo.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // tempo de expiração do token: 1 hora
            var expiration = DateTime.UtcNow.AddDays(5);
            JwtSecurityToken token = new JwtSecurityToken(
              
                issuer:"SiGN.Api",
                 audience:"SiGN.Api",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);
            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        private UserToken generateJwtToken(Usuario user)
        {
            return BuildToken(new UserInfo { Email = user.Email, Password = user.Senha, Role = user.Role });

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Issuer = "SiGN.Api",
            //    Audience = "SiGN.Api",
            //    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),

            //    //Claims = (IDictionary<string, object>)claims,
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            //return new UserToken
            //{
            //    Expiration = tokenDescriptor.Expires.Value,
            //    Token = tokenHandler.WriteToken(token)
            //};
        }
    }
}
