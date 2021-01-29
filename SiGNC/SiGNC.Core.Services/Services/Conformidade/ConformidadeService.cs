﻿using Microsoft.AspNetCore.Identity;
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

        public Task<List<ConformidadeDto>> ListarConformidades()
        {
            throw new NotImplementedException();
        }

        public bool SalvarConformidade(ConformidadeDto conformidade)
        {
            throw new NotImplementedException();
        }
    }
}
