using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiGNC.Application.Web.NET5.Models.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Controllers
{ 
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] UsuarioViewModel user)
        {
            return View();
        } 


    }
}
