using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SiGNC.Application.Web.NET5.Models.Autenticacao;
using SiGNC.Core.Services.Interfaces;
using SiGNC.Infra.Data.Dtos.Authorization;
using SiGNC.Infra.Settings;
using SiGNC.Infra.Settings.Authorization;
using System.Collections.Generic;

namespace SiGNC.Application.Web.NET5.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        protected readonly IAuthorizationSiGncService _autorization;

        //Construtor
        public TokenController(IConfiguration configuration, IAuthorizationSiGncService autorization) { 
            _configuration = configuration;
            _autorization = autorization;
        }

        [AllowAnonymous]
        [HttpPost]

        public IActionResult RequestToken([FromBody] UsuarioViewModel request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
                return BadRequest("Email e Senha inválidos");
            else
            {
                var Token = _autorization.Authenticate(new AuthenticateRequestDto
                {
                    Email = request.Email,
                    Senha = request.Senha,
                    
                });

                return Ok(new { token = Token.Token});
            };
        }


        [HttpGet] 
        [Authorize()]
        [Route("getusuario")]
        public IActionResult GetUsuarios()
        {
            var lista = new List<UsuarioViewModel>();

            for (int i = 1; i <= 10; i++)
            {
                lista.Add(new UsuarioViewModel { Id  = i, Nome = "Usuario - " + i.ToString() });
            }

            return Ok(lista);
        }
    }
} 
