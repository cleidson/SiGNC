using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SiGNC.Application.Web.NET5.Models.Autenticacao;
using SiGNC.Core.Services.Interfaces;
using SiGNC.Infra.Data.Dtos.Authorization;
using SiGNC.Infra.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Controllers
{
  
        [Route("api/[controller]")]
        [ApiController]
        public class UsuariosController : ControllerBase
        {

            protected readonly IAuthorizationSiGncService _autorization;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IConfiguration _configuration;
            public UsuariosController(UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration,
                IAuthorizationSiGncService autorization
                )
            {
                _autorization = autorization;
                _userManager = userManager;
                _signInManager = signInManager;
                _configuration = configuration;
            }
            [HttpGet]
            public ActionResult<string> Get()
            {
                return " << Controlador UsuariosController :: WebApiUsuarios >> ";
            }
            [HttpPost("Criar")]
            [Route("Criar")]
            public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, NormalizedUserName = "CLEIDSON" };
                var result = await _userManager.CreateAsync(user, model.Password);
             
                if (result.Succeeded)
                {
                    return BuildToken(model);
                }
                else
                {
                    return BadRequest("Usuário ou senha inválidos");
                }
            }
            [HttpPost("Login")]
            public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password,
                     isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return BuildToken(userInfo);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "login inválido.");
                    return BadRequest(ModelState);
                }
            }
            private UserToken BuildToken(UserInfo request)
            {
                var Token = _autorization.Authenticate(new AuthenticateRequestDto
                {
                    Email = request.Email,
                    Senha = request.Password,

                });

                return new UserToken()
                {
                    Token = Token.Token
                };
            }
        }
    
}