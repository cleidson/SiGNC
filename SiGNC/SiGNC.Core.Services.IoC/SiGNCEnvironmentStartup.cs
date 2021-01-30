
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.IdentityModel.Tokens;
using SiGNC.Core.Services.Interfaces;
using SiGNC.Core.Services.Interfaces.Conformidade;
using SiGNC.Core.Services.IoC;
using SiGNC.Core.Services.Services;
using SiGNC.Core.Services.Services.Conformidade;
using SiGNC.Infra.Data.Context;
using SiGNC.Infra.Data.Models;
using SiGNC.Infra.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 


namespace SiGNC.Core.Services.IoC
{
    public static class SiGNCEnvironmentStartup
    {
        //https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences
        public static void RegisterServices(this IServiceCollection services)
        { 
            services.AddScoped<IAuthorizationSiGncService, AuthorizationSiGncService>();
            services.AddScoped<IConformidadeService, ConformidadeService>();
            services.AddScoped<ICausaRaizConformidadeService, CausaRaizConformidadeService>();
            services.AddScoped<IImplantaConformidadeService, ImplantaConformidadeService>();
            services.AddScoped<IOrigemConformidadeService, OrigemConformidadeService>();
            services.AddScoped<IStatusConformidadeService, StatusConformidadeService>();
            services.AddScoped<ITipoAcaoConformidadeService, TipoAcaoConformidadeService>();
            services.AddScoped<ITipoConformidadeService, TipoConformidadeService>();
            services.AddScoped<IUsuarioConformidadeService, UsuarioConformidadeService>();


            
        }
    }
}
