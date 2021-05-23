using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nibo.ExtractBank.Domain.Interfaces.IRepository;
using Nibo.ExtractBank.Domain.Interfaces.IServices;
using Nibo.ExtractBank.Infrastructure.Context;
using Nibo.ExtractBank.Infrastructure.Repository;
using Nibo.ExtractBank.Service.ExtractBankService;
using Nibo.ExtractBank.Service.FileImportService;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.ExtractBank.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<NiboExtractBankDbContext>();

            services.AddScoped<IFileImportService, FileImportService>();
            services.AddScoped<IExtractBankService, ExtractBankService>();
            services.AddScoped<IExtractBankRepository, ExtractBankRepository>();


            return services;
        }
    }
}
