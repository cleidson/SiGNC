
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.IdentityModel.Tokens;
using SiGNC.Core.Services.Interfaces;
using SiGNC.Core.Services.IoC;
using SiGNC.Core.Services.Services;
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

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationSiGncService, AuthorizationSiGncService>();
        
        }
    }
}
