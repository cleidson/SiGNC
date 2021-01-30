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
    public class UsuarioConformidadeService : IUsuarioConformidadeService
    {
        private readonly AppSettings _appSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public UsuarioConformidadeService(IOptions<AppSettings> appSettings, ApplicationDbContext db, SignInManager<ApplicationUser> signInManager)
        {
            _appSettings = appSettings.Value;
            _db = db;
            _signInManager = signInManager;
        }

        public Task<List<UsuarioDto>> SearchUsuarios(string keyword)
        {
            try
            {
                var users = (from user in _db.Users
                             where user.Nome.Contains(keyword)
                             select new UsuarioDto
                             {
                                 Id = user.Id,
                                 Nome = user.Nome,
                                 SobreNome = user.Sobrenome
                             }).AsQueryable();

                return users.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
