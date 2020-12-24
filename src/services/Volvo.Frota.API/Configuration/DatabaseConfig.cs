using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volvo.Frota.API.Data;

namespace Volvo.Frota.API.Configuration
{
    public static class DatabaseConfig
    {
        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            using (var context = scope.ServiceProvider.GetService<VolvoContext>())
            {
                 context.Database.Migrate();
            }
        }
    }
}
