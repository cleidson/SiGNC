using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SiGNC.Application.Web.NET5.Models.Autenticacao;
using SiGNC.Core.Services.DTOs;
using SiGNC.Core.Services.DTOs.Authorization;
using SiGNC.Core.Services.Interfaces; 
using SiGNC.Infra.Settings;
using SiGNC.Infra.Settings.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Controllers
{
    [ApiController]
    [Route("")]
    [Route("Token")]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        protected readonly IAuthorizationSiGncService _autorization;
        private readonly ILogger<HomeController> _logger;
        string resul = string.Empty;

        //Construtor
        public TokenController(ILogger<HomeController> logger, IConfiguration configuration, IAuthorizationSiGncService autorization)
        {
            _configuration = configuration;
            _autorization = autorization;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [Route("Login")]
        [HttpPost("Login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] UsuarioViewModel user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Senha))
                return BadRequest("Email e Senha inválidos");
            else
            {
                var Token = await _autorization.AuthenticateSync(new AuthenticateRequestDto
                {
                    Email = user.Email,
                    Senha = user.Senha,

                });

                if(Token == null)
                    return BadRequest("Email e Senha inválidos");

                return RedirectToAction("Index", "Home");
            };
           
        }



        [Route("page/login")]
        [HttpPost("page/login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginPage([FromForm] UsuarioViewModel user)
        {
            
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Senha))
            { 
                resul = "Email e Senha inválidos";
                return Ok(resul);
            }
            else
            {
                var Token = await _autorization.AuthenticateSync(new AuthenticateRequestDto
                {
                    Email = user.Email,
                    Senha = user.Senha,

                });

                if (Token == null)
                    resul = "Email e Senha inválidos";

                if (Token != null)
                    return Ok(Token);
                else
                    return Ok(resul);
            };  
        }


        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> RequestToken([FromBody] UsuarioViewModel request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
                return BadRequest("Email e Senha inválidos");
            else
            {
                var Token = await _autorization.AuthenticateSync(new AuthenticateRequestDto
                {
                    Email = request.Email,
                    Senha = request.Senha,

                });

                return Ok(new { token = Token.Token });
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
                lista.Add(new UsuarioViewModel { Id = i, Nome = "Usuario - " + i.ToString() });
            }

            return Ok(lista);
        }
    }
}
