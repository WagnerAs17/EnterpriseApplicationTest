using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volvo.Frota.API.Configuration;
using Volvo.Frota.API.Data;
using Microsoft.Extensions.Configuration;

namespace Volvo.Frota.API
{
    public class StartupTest
    {
        public IConfiguration Configuration { get; }
        public StartupTest(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<VolvoContext>(options =>
            {
                options.UseInMemoryDatabase("integrationTests");
            });

            services.RegisterDependencyInjection();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiConfiguration(env);

            UseDatabaseConfiguration(app);
        }

        public void UseDatabaseConfiguration(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            using (var context = scope.ServiceProvider.GetService<VolvoContext>())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
