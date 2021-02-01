using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiGNC.Application.Web.NET5.Models;
using SiGNC.Core.Services.Interfaces.Conformidade;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConformidadeService _conformidadeService;
        
        public HomeController(ILogger<HomeController> logger, IConformidadeService conformidadeService)
        {
            _conformidadeService = conformidadeService;
               _logger = logger;
        }




        [HttpGet("chart/count")]
        [Route("chart/count")]
        public async Task<IActionResult> ChartConformidade()
        {
            try
            { 
                return Ok(await Task.Run(() => _conformidadeService.TotalPorSemanaSync().Where(t=>t.Total>0)));
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
