using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Volvo.Frota.API.Models.Interfaces;

namespace Volvo.Frota.API.Extensions
{
    public static class EfExtension
    {
        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder});
        }

        static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(EfExtension)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, ISoftDelete
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.Excluido);
        }

        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
