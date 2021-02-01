using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SiGNC.Core.Services.DTOs.Conformidade;
using SiGNC.Core.Services.Interfaces.Conformidade;
using SiGNC.Infra.Data.Context;
using SiGNC.Infra.Data.Models;
using SiGNC.Infra.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Core.Services.Services.Conformidade
{
    public class ConformidadeService : IConformidadeService
    {
        private readonly AppSettings _appSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public ConformidadeService(IOptions<AppSettings> appSettings, ApplicationDbContext db, SignInManager<ApplicationUser> signInManager)
        {
            _appSettings = appSettings.Value;
            _db = db;
            _signInManager = signInManager;
        }

        public bool EditarConformidade(ConformidadeDto conformidade)
        {
            throw new NotImplementedException();
        }

        public Task<List<ConformidadeDto>> GetConformidadesSync()
        {
            try
            {
                var conformidades = (from or in _db.Conformidades
                                     where or.StatusConformidadeId == 1
                                     select new ConformidadeDto
                                     {
                                         Id = or.Id,
                                         UsuarioSolicitante = new UsuarioDto
                                         {
                                             Id = or.UsuarioSolicitanteId,
                                             Nome = or.UsuarioSolicitante.Nome
                                         },
                                         NumeroConformidade = or.NumeroConformidade,
                                         StatusConformidade = new StatusConformidadeDto
                                         {
                                             Id = or.StatusConformidade.Id,
                                             Nome = or.StatusConformidade.Nome

                                         },
                                         DataEmissao = or.DataCadastro.Value.ToString("dd/MM/yyyy")
                                     }).AsQueryable();

                return conformidades.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ConformidadeDto> GetConformidadeSync(int id)
        {
            try
            {

                var conformidades = (from conformidade in _db.Conformidades
                                     where conformidade.Id == id
                                     select new ConformidadeDto
                                     {
                                         OrigemConformidadeId = conformidade.OrigemConformidadeId.ToString(),
                                         OrigemConformidade = new OrigemDto { Id = conformidade.OrigemConformidade.Id, Nome = conformidade.OrigemConformidade.Nome },
                                         StatusConformidadeId = conformidade.StatusConformidadeId.ToString(),
                                         StatusConformidade = new StatusConformidadeDto { Id = conformidade.StatusConformidade.Id, Nome = conformidade.StatusConformidade.Nome },
                                         UsuarioSolicitanteId = conformidade.UsuarioSolicitanteId,
                                         UsuarioSolicitante = new UsuarioDto { Id = conformidade.UsuarioSolicitante.Id, Nome = conformidade.UsuarioSolicitante.Nome + "" + conformidade.UsuarioSolicitante.Sobrenome },
                                         UsuarioGestorId = conformidade.UsuarioGestorId,
                                         UsuarioGestor = new UsuarioDto { Id = conformidade.UsuarioGestor.Id, Nome = conformidade.UsuarioGestor.Nome + "" + conformidade.UsuarioGestor.Sobrenome },
                                         TipoConformidadeId = conformidade.TipoConformidadeId.ToString(),
                                         Reincidente = conformidade.Reincidente,
                                         Requisito = conformidade.Requisito,
                                         NumeroConformidade = conformidade.NumeroConformidade,
                                         DataEmissao = conformidade.DataCadastro.Value.ToString("dd/MM/yyyy"),
                                         AcaoCorretiva = (from acao in conformidade.AcaoCorretivaConformidades
                                                          select new AcaoCorretivaConformidadeDto
                                                          {
                                                              Responsavel = new UsuarioDto { Nome = acao.Responsavel.Nome + "" + acao.Responsavel.Sobrenome },
                                                              DataImplantacao = acao.DataLimite.Value.ToString("dd/MM/yyyy"),
                                                              Descricao = acao.Descricao,
                                                              RiscoOportunidade = acao.RiscoOportunidade,
                                                              TipoAcaoNome = acao.TipoAcao.Nome,
                                                              TipoAcaoId = acao.TipoAcao.Id
                                                          }
                                        ).FirstOrDefault(),
                                         Detalhamentos = (from dt in conformidade.DetalhaConformidades
                                                          select new DetalhaConformidadeDto
                                                          {
                                                              Descricao = dt.Descricao,
                                                              Detalhamento = dt.Abrangencia,
                                                              Id = dt.Id
                                                          }).ToList(),
                                         CausaRaizes = (from cr in conformidade.ConformidadeHasCausaRaizs
                                                        select new ConformidadeHasCausaRaizDto
                                                        {
                                                            Id = cr.Id,
                                                            ConformidadeId = (int)cr.ConformidadeId,
                                                            CausaRaizConformidadeId = cr.Id,
                                                            CausaRaizId = cr.CausaRaizConformidade.Id,
                                                            CausaRaizDescricao = cr.CausaRaizConformidade.Descricao,
                                                            Ocorreu = cr.Ocorreu,
                                                            OcorreuFormated = cr.Ocorreu == true ? "Sim" : "Nao",
                                                            Quais = cr.Quais == null ? "" : cr.Quais
                                                        }).ToList()
                                     }
                          ).AsQueryable();

                return conformidades.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> GetNumConformidade(string numConformidade)
        {
            try
            {
                return await _db.Conformidades.Where(t => t.NumeroConformidade.Equals(numConformidade)).AnyAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<List<ConformidadeDto>> GetConformidadesSync(int statusConformidadeId)
        {
            try
            { 
                var conformidades = (from or in _db.Conformidades
                                     where or.StatusConformidadeId == statusConformidadeId
                                     select new ConformidadeDto
                                     {
                                         Id = or.Id,
                                         UsuarioSolicitante = new UsuarioDto
                                         {
                                             Id = or.UsuarioSolicitanteId,
                                             Nome = or.UsuarioSolicitante.Nome
                                         },
                                         NumeroConformidade = or.NumeroConformidade,
                                         StatusConformidade = new StatusConformidadeDto
                                         {
                                             Id = or.StatusConformidade.Id,
                                             Nome = or.StatusConformidade.Nome

                                         },
                                         DataEmissao = or.DataCadastro.Value.ToString("dd/MM/yyyy")
                                     }).AsQueryable();

                return conformidades.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> EditarConformidadeSync(ConformidadeDto conformidade)
        {
            try
            {
                using (_db)
                { 
                    var data = _db.Conformidades.Where(t => t.Id == conformidade.Id).FirstOrDefault(); 
                    data.StatusConformidadeId = int.Parse(conformidade.StatusConformidadeId);
                    _db.Entry(data).State = EntityState.Modified; 
                    var result = _db.SaveChanges() > 0;
                    await _db.DisposeAsync();

                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> SalvarConformidadeSync(ConformidadeDto conformidade)
        {
            try
            {
                using (_db)
                {
                    var data = new Infra.Data.Models.Conformidade
                    {
                        OrigemConformidadeId = int.Parse(conformidade.OrigemConformidadeId),
                        StatusConformidadeId = int.Parse(conformidade.StatusConformidadeId),
                        UsuarioSolicitanteId = conformidade.UsuarioSolicitanteId,
                        UsuarioGestorId = "96f6259c-c65b-47ea-a696-b2a628f4ed72", //Usuário logado
                        TipoConformidadeId = 3,
                        Reincidente = conformidade.Reincidente,
                        Requisito = conformidade.Requisito,
                        NumeroConformidade = conformidade.NumeroConformidade,
                        DataCadastro = DateTime.Parse(conformidade.DataEmissao),
                        AcaoCorretivaConformidades = new List<AcaoCorretivaConformidade>{
                            new AcaoCorretivaConformidade
                            {
                                 ResponsavelId = conformidade.AcaoCorretiva.Responsavel.Id,
                                 DataLimite = DateTime.Parse(conformidade.AcaoCorretiva.DataImplantacao),
                                 Descricao = conformidade.AcaoCorretiva.Descricao,
                                 RiscoOportunidade = conformidade.AcaoCorretiva.RiscoOportunidade,
                                 TipoAcaoId  = conformidade.AcaoCorretiva.Id
                            }
                        },
                        DetalhaConformidades = (from dt in conformidade.Detalhamentos
                                                select new DetalhaConformidade
                                                {
                                                    Descricao = dt.Descricao,
                                                    Abrangencia = dt.Detalhamento
                                                }).ToList(),
                        ConformidadeHasCausaRaizs = (from cr in conformidade.CausaRaizes
                                                     select new ConformidadeHasCausaRaiz
                                                     {
                                                         CausaRaizConformidadeId = cr.CausaRaizConformidadeId,
                                                         Ocorreu = cr.Ocorreu,
                                                         Quais = cr.Quais
                                                     }).ToList()
                    };

                    _db.Conformidades.Add(data);
                    var result = _db.SaveChanges() > 0;
                    await _db.DisposeAsync();

                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public  List<TotalConformidadeWeekDto> TotalPorSemanaSync()
        {
            try
            {
                DateTime start = new DateTime(2021, 1, 1);
                DateTime end = DateTime.Now;
                var res = Enumerable
                                   .Range(0, 1 + (end - start).Days)
                                   .Select(x => start.AddDays(x))
                                   .GroupJoin(_db.Conformidades,
                                       dt => dt, o => o.DataCadastro,
                                       (dt, orders) => new TotalConformidadeWeekDto { Dia = dt.ToString("dd/MM/yyyy"), Total = orders.Count() })
                                   .ToList();

                return res;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
