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
    public class CausaRaizConformidadeService : ICausaRaizConformidadeService
    {

        private readonly AppSettings _appSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public CausaRaizConformidadeService(IOptions<AppSettings> appSettings, ApplicationDbContext db, SignInManager<ApplicationUser> signInManager)
        {
            _appSettings = appSettings.Value;
            _db = db;
            _signInManager = signInManager;
        }

        public Task<List<CausaRaizConformidadeDto>> GetCausaRaizConformidade()
        {
            try
            {
                var causaRaizes = (from cr in _db.CausaRaizConformidades
                             select new CausaRaizConformidadeDto
                             {
                                 Id = cr.Id,
                                 Nome = cr.Nome,
                                 Descricao = cr.Descricao
                             }).AsQueryable();

                return causaRaizes.ToListAsync(); 
            }
            catch (Exception)
            {

                throw;
            }
        } 
    }
}
