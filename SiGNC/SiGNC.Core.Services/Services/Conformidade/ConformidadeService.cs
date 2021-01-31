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
         
        public Task<List<ConformidadeDto>> GetConformidades()
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
                               }) .AsQueryable();

                return conformidades.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async  Task<bool> GetNumConformidade(string numConformidade)
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

        public async Task<bool> SalvarConformidade(ConformidadeDto conformidade)
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
                        UsuarioGestorId = "0a1ff47b-0cc3-43f8-8687-3d1c11bb00bf", //Usuário logado
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
                        DetalhaConformidades =  (from dt in conformidade.Detalhamentos
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
                    var result =  _db.SaveChanges() > 0;
                    await _db.DisposeAsync();

                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
