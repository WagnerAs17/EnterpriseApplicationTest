using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volvo.Frota.API.Extensions;
using Volvo.Frota.API.Models;
using Volvo.Frota.API.Models.Interfaces;

namespace Volvo.Frota.API.Data
{
    public class VolvoContext : DbContext
    {
        public static readonly ILoggerFactory DbCommandDebugLoggerFactory = LoggerFactory.Create(config =>
        {
            config.AddDebug();
        });

        public VolvoContext(DbContextOptions<VolvoContext> options) : base(options)
        {}

        public DbSet<Caminhao> Caminhoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => 
                e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VolvoContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(DbCommandDebugLoggerFactory);

            base.OnConfiguring(optionsBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var deletedEntries = ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Deleted)
                .Where(x => typeof(ISoftDelete).IsAssignableFrom(x.Entity.GetType()));

            foreach (var entry in deletedEntries)
            {
                entry.CurrentValues["Excluido"] = true;
                entry.CurrentValues["ExcluidoEm"] = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
