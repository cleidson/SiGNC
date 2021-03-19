using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiGNC.Application.Web.NET5.Models.Conformidade;
using SiGNC.Application.Web.NET5.Models.Tables;
using SiGNC.Core.Services.DTOs.Conformidade;
using SiGNC.Core.Services.Interfaces.Conformidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly IConformidadeService _conformidadeService;
        private static Random random = new Random();

        public ConformidadeController(
            ILogger<HomeController> logger,
            IOrigemConformidadeService origemService,
            IStatusConformidadeService statusConformidadeService,
            ITipoAcaoConformidadeService tipoAcaoConformidadeService,
            IUsuarioConformidadeService usuarioConformidadeService,
            ICausaRaizConformidadeService causaRaizConformidadeService,
             IConformidadeService conformidadeService
            )
        {
            _logger = logger;
            _origemService = origemService;
            _statusConformidadeService = statusConformidadeService;
            _tipoAcaoConformidadeService = tipoAcaoConformidadeService;
            _usuarioConformidadeService = usuarioConformidadeService;
            _causaRaizConformidadeService = causaRaizConformidadeService;
            _conformidadeService = conformidadeService;
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
        [Route("EmImplantacao")]
        public IActionResult EmImplantacao()
        {
            return View();
        }


        [HttpGet]
        [Route("Finalizadas")]
        public IActionResult Finalizadas()
        {
            return View();
        }
         

        [HttpPost("salvar")]
        [Route("salvar")]
        public async Task<HttpResponseMessage> SalvarConformidade(ConformidadeViewModel conformidade)
        {
            bool result;

            try
            {
                bool resultRandom = false;

                do
                {
                    result = false;
                    var RandomStringValue = "NC" + RandomString(10);

                    if (await _conformidadeService.GetNumConformidade(RandomStringValue) == false)
                    {
                        result = await _conformidadeService.SalvarConformidadeSync(new ConformidadeDto
                        {

                            OrigemConformidadeId = conformidade.OrigemConformidadeId,
                            StatusConformidadeId = conformidade.StatusConformidadeId,
                            UsuarioSolicitanteId = conformidade.Eminente.Id,
                            UsuarioGestorId = "107fa1ac-6191-4541-8096-e08d5bcc7690", //Usuário logado
                            TipoConformidadeId = "3",
                            Reincidente = conformidade.Reincidente,
                            Requisito = conformidade.Requisito,
                            NumeroConformidade = RandomStringValue,
                            DataEmissao = conformidade.DataEmissao,
                            AcaoCorretiva = new AcaoCorretivaConformidadeDto
                            {
                                Responsavel = new UsuarioDto
                                {
                                    Id = conformidade.AcaoCorretiva.Responsavel.Id
                                },
                                Descricao = conformidade.AcaoCorretiva.Descricao,
                                DataImplantacao = conformidade.AcaoCorretiva.DataImplantacao,
                                RiscoOportunidade = conformidade.AcaoCorretiva.RiscoOportunidade,
                                Id = conformidade.AcaoCorretiva.TipoAcaoId
                            },
                            Detalhamentos = (from dt in conformidade.Detalhamentos
                                             select new DetalhaConformidadeDto
                                             {
                                                 Descricao = dt.Descricao,
                                                 Detalhamento = dt.Detalhamento
                                             }).ToList(),
                            CausaRaizes = (from cr in conformidade.CausaRaizes
                                           select new ConformidadeHasCausaRaizDto
                                           {
                                               CausaRaizConformidadeId = cr.CausaRaizConformidadeId,
                                               Ocorreu = cr.Ocorreu == "Sim",
                                               Quais = cr.Quais
                                           }).ToList()
                        });

                        resultRandom = true;
                    }
                    else
                    {
                        resultRandom = false;
                    }



                } while (resultRandom == false);

                if (result)
                    return await Task.Run(() => new HttpResponseMessage() { StatusCode = (System.Net.HttpStatusCode)200 });
                else
                    return await Task.Run(() => new HttpResponseMessage() { StatusCode = (System.Net.HttpStatusCode)400 });
            }
            catch (WebException ex)
            {
                return await Task.Run(() => new HttpResponseMessage() { StatusCode = (HttpStatusCode)ex.Status });
                throw;
            }

        }



        [HttpPost("editar")]
        [Route("editar")]
        public async Task<HttpResponseMessage> EditarConformidade(ConformidadeViewModel conformidade)
        {
            bool result;

            try
            {
                result = await _conformidadeService.EditarConformidadeSync(new ConformidadeDto
                {
                    Id = conformidade.Id,
                    StatusConformidadeId = conformidade.StatusConformidadeId
                });


                if (result)
                    return await Task.Run(() => new HttpResponseMessage() { StatusCode = (System.Net.HttpStatusCode)200 });
                else
                    return await Task.Run(() => new HttpResponseMessage() { StatusCode = (System.Net.HttpStatusCode)400 });
            }
            catch (WebException ex)
            {
                return await Task.Run(() => new HttpResponseMessage() { StatusCode = (HttpStatusCode)ex.Status });
                throw;
            }

        }


        [HttpGet("conformidade/list/{Id}")]
        [Route("conformidade/list/{Id}")]
        public async Task<JsonResult> GetConformidades(int Id)
        {
            try
            {
                var conformidades = (from or in await _conformidadeService.GetConformidadesSync(Id)
                                     select new ConformidadeTableViewModel
                                     {
                                         Id = or.Id,
                                         UsuarioEmitente = or.UsuarioSolicitante.Nome + "" + or.UsuarioSolicitante.SobreNome,
                                         DataEmissao = or.DataEmissao,
                                         NumeroConformidade = or.NumeroConformidade,
                                         DescricaoStatusConformidade = or.StatusConformidade.Nome,
                                         IdStatusConformidade = or.StatusConformidade.Id
                                     }).ToList().OrderByDescending(t => t.Id);
                return Json(conformidades);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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


        [HttpGet("status/list")]
        [Route("status/list")]
        public async Task<JsonResult> GetStatus()
        {
            var origens = (from or in await _statusConformidadeService.GetStatusConformidadeSync()
                           select new StatusConformidadeViewModel
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


        //[HttpPost, Route("details")]
        //public IActionResult DetalhaConformidade(string id)
        //{
        //    ViewBag.ConformidadeHiddenId = id;
        //    return View();
        //}



        [HttpGet, Route("details/{id}")]
        public IActionResult DetalhaConformidade(int Id)
        {
            ViewBag.ConformidadeHiddenId = Id;
            return View();
        }

        [HttpGet, Route("details/finalizadas/{id}")]
        public IActionResult DetalhaConformidadeFinalizadas(int Id)
        {
            ViewBag.ConformidadeHiddenId = Id;
            return View();
        }

        [HttpGet, Route("details/implantacao/{id}")]
        public IActionResult DetalhaConformidadeImplantacao(int Id)
        {
            ViewBag.ConformidadeHiddenId = Id;
            return View();
        }



        [HttpGet, Route("edit/{id}")]
        public IActionResult EditarConformidade(int Id)
        {
            try
            {
                ViewBag.ConformidadeHiddenId = Id;
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpGet, Route("edit/implantacao/{id}")]
        public IActionResult EditarConformidadeImplantacao(int Id)
        {
            try
            {
                ViewBag.ConformidadeHiddenId = Id;
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost("detalhe")]
        [Route("detalhe")]
        public async Task<IActionResult> DetalhaConformidade(string requestId)
        {
            try
            {
                var conformidades = await _conformidadeService.GetConformidadeSync(int.Parse(requestId));
                return Json(conformidades);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
