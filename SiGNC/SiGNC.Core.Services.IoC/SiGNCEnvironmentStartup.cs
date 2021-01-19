using Microsoft.Extensions.DependencyInjection;
using SiGNC.Core.Services.Interfaces;
using SiGNC.Core.Services.Services;
using System;

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
