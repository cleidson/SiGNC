using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiGNC.Application.Web.NET5.Models.Conformidade;
using SiGNC.Core.Services.Interfaces.Conformidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiGNC.Application.Web.NET5.Controllers
{

    //[Route("api/[controller]")]
    //[Route("Conformidade")]
    //[ApiController]
    public class ConformidadeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrigemConformidadeService _origemService;
        private readonly IStatusConformidadeService _statusConformidadeService;
        private readonly ITipoAcaoConformidadeService _tipoAcaoConformidadeService;
        private readonly IUsuarioConformidadeService _usuarioConformidadeService;
        private readonly ICausaRaizConformidadeService _causaRaizConformidadeService;

        public ConformidadeController(
            ILogger<HomeController> logger,
            IOrigemConformidadeService origemService,
            IStatusConformidadeService statusConformidadeService,
            ITipoAcaoConformidadeService tipoAcaoConformidadeService,
            IUsuarioConformidadeService usuarioConformidadeService,
            ICausaRaizConformidadeService causaRaizConformidadeService
            )
        {
            _logger = logger;
            _origemService = origemService;
            _statusConformidadeService = statusConformidadeService;
            _tipoAcaoConformidadeService = tipoAcaoConformidadeService;
            _usuarioConformidadeService = usuarioConformidadeService;
            _causaRaizConformidadeService = causaRaizConformidadeService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Pendentes")]
        public IActionResult Pendentes()
        {
            return View();
        }

        [HttpGet]
        [Route("EmAndamento")]
        public IActionResult EmAndamento()
        {
            return View();
        }

        [HttpGet]
        [Route("Finalizadas")]
        public IActionResult Finalizadas()
        {
            return View();
        }

        //[Route("salvar")]
        //[HttpPost("salvar")]
        //public IActionResult SalvarConformidade(ConformidadeViewModel conformidade)
        //{
        //    try
        //    {
        //        return Json(conformidade);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost("salvar")]
        [Route("salvar")]
        public async Task<HttpResponseMessage> SalvarConformidade(ConformidadeViewModel conformidade)
        {
            return await Task.Run(() => new HttpResponseMessage() { StatusCode = (System.Net.HttpStatusCode)200 });

        }

        [HttpGet("origem/list")]
        [Route("origem/list")]
        public async Task<JsonResult> GetOrigens()
        {
            var origens = (from or in await _origemService.GetOrigensConformidadeSync()
                           select new OrigemConformidadeViewModel
                           {
                               Id = or.Id,
                               Nome = or.Nome
                           }).ToList();
            return Json(origens);
        }


        [HttpGet("acao/tipo/list")]
        [Route("acao/tipo/list")]
        public async Task<JsonResult> GetTipoAcoesConformidade()
        {
            var tipoAcoes = (from or in await _tipoAcaoConformidadeService.GetTipoAcoesConformidadeSync()
                             select new TipoAcaoViewModel
                             {
                                 Id = or.Id,
                                 Nome = or.Nome
                             }).ToList();
            return Json(tipoAcoes);
        }


        [HttpGet("causaraiz/list")]
        [Route("causaraiz/list")]
        public async Task<JsonResult> GetCausasRaize()
        {
            var causaRaizes = (from cr in await _causaRaizConformidadeService.GetCausaRaizConformidade()
                             select new CausaRaizConformidadeViewModel
                             {
                                 Id = cr.Id,
                                 Nome = cr.Nome,
                                 Descricao = cr.Descricao
                                 
                             }).ToList();
            return Json(causaRaizes);
        }



        [HttpPost("usuario/search")]
        [Route("usuario/search")]
        public async Task<IActionResult> SearchUsers(string term)
        {

            //string term = HttpContext.Request.Query["term"].ToString();
            var usuarios = (from user in await _usuarioConformidadeService.SearchUsuarios(term)
                            select new UsuarioViewModel
                            {
                                Id = user.Id,
                                Nome = user.Nome + " " + user.SobreNome
                            }).ToList();
            return Ok(usuarios);
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
        public IActionResult DetalhaConformidade(int id)
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
