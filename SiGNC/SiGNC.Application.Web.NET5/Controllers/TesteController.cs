using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGNC.Application.Web.NET5.Models.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize()]
    public class TesteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet] 
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
