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
    public class StatusConformidadeService : IStatusConformidadeService
    {

        private readonly AppSettings _appSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public StatusConformidadeService(IOptions<AppSettings> appSettings, ApplicationDbContext db, SignInManager<ApplicationUser> signInManager)
        {
            _appSettings = appSettings.Value;
            _db = db;
            _signInManager = signInManager;
        } 
        public Task<List<StatusConformidadeDto>> GetStatusConformidadeSync()
        {
            try
            {
                var status = (from or in _db.StatusConformidades
                              where or.Id != 2
                               select new StatusConformidadeDto
                               {
                                   Id = or.Id,
                                   Nome = or.Nome
                               }).AsQueryable();

                return status.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
