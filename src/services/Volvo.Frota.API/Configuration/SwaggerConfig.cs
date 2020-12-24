using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Volvo.Frota.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Volvo Enterprise Frota API",
                    Description = "Volvo Enterprise API para controle da frota.",
                    Contact = new OpenApiContact { Name = "Wagner Santos", Email = "almeidawagner405@gmail.com"},
                    License = new OpenApiLicense { Name = "Wagner GitHub", Url = new Uri("https://github.com/WasAlmeida/applicationTest") }
                });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
