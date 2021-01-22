using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiGNC.Application.Web.NET5.Models.Conformidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiGNC.Application.Web.NET5.Controllers
{
    //[Route("Conformidade")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class ConformidadeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ConformidadeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pendentes()
        {
            return View();
        }

        public IActionResult EmAndamento()
        {
            return View();
        }

        public IActionResult Finalizadas()
        {
            return View();
        }


        #region Ações corretivas

        [HttpGet]
        public IActionResult AcaoCorretiva()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AcaoCorretiva([FromForm] AcaoCorretivaViewModel acaoCorretiva)
        {
            return View();
        }

        #endregion

        #region Implantar Conformidade

        [HttpGet]
        public IActionResult ImplantarConformidade()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImplantarConformidade([FromForm] ImplantacaoConformidadeViewModel implantarConformidade)
        {
            return View();
        }


        #endregion

        [HttpGet]
        public IActionResult DetalhaConformidade()
        {
            return View();
        } 



        //// GET: RelatorioController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: RelatorioController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: RelatorioController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: RelatorioController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    }
}
