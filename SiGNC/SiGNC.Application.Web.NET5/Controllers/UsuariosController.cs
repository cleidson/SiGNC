using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SiGNC.Core.Services.DTOs.Authorization;
using SiGNC.Core.Services.Interfaces;
using SiGNC.Infra.Data.Entities;
using SiGNC.Infra.Data.Models;
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
        private readonly RoleManager<IdentityRole> _roleManager;



        public UsuariosController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IAuthorizationSiGncService autorization,
            RoleManager<IdentityRole> roleManager
            )
        {
            _autorization = autorization;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
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
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Nome = model.Name};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //-------------------atribuir role ao user------------------------------
                var applicationRole = await _roleManager.FindByNameAsync(Role.Admin);
                if (applicationRole != null)
                {
                    IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                }
                //-------------------atribuir role ao user------------------------------


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
            var Token = _autorization.AuthenticateSync(new AuthenticateRequestDto
            {
                Email = request.Email,
                Senha = request.Password,

            }).Result;

            return new UserToken()
            {
                Token = Token.Token,
                Expiration = Token.ExpirationToken
            };
        }
    }

}