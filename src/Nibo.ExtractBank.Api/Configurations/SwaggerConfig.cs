using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Nibo.ExtractBank.Api.Configurations
{
    public static class SwaggerConfig
    {

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "API - Nibo Extract Bank",
                    Description = "Esta API realiza a importação de conciliações bancárias da empresa com as entradas e saídas.",
                    Contact = new OpenApiContact() { Name = "Allan Bernardes", Email = "allanbernardes85@gmail.com" }
                });

                //c.MapType(typeof(IFormFile), () => new OpenApiSchema() { Type = "file", Format = "binary" });                

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            //app.UseMiddleware<SwaggerAuthorizedMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    {
                        options.SwaggerEndpoint($"/swagger/v1/swagger.json", "NIBO_EXTRACT_BANK");
                    }
                });
            return app;
        }


    }

}
