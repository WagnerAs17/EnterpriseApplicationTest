using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volvo.Frota.API.Utils.Pagination;
using Volvo.Frota.API.Utils.Reflection;

namespace Volvo.Frota.API.Data.Repositories
{
    public class Entities
    {
        private readonly VolvoContext _context;
        public Entities(VolvoContext context)
        {
            _context = context;

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public IQueryable<T> Query<T>() where T : class => _context.Set<T>().AsQueryable();

        public async Task AddAsync(object model)
        {
            await _context.AddAsync(model);

            await _context.SaveChangesAsync();

            _context.Entry(model).State = EntityState.Detached;
        }

        public async Task RemoveAsync(object model)
        {
            _context.Remove(model);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(object model)
        {
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            _context.Entry(model).State = EntityState.Detached;
        }

        public async Task PartialUpdateAsync(object model, params string[] properties)
        {
            _context.Entry(model).State = EntityState.Modified;

            var navigationProperties = _context.Entry(model).Metadata
                .GetNavigations().Select(x => x.PropertyInfo);

            var propertiesNames = _context.Entry(model).CurrentValues.Properties
                .Select(x => x.PropertyInfo)
                .Except(navigationProperties)
                .Select(x => x.Name);

            foreach (var property in propertiesNames)
            {
                if (properties.Contains(property))
                {
                    _context.Entry(model).Property(property).IsModified = true;
                    continue;
                }

                _context.Entry(model).Property(property).IsModified = false;
            }

            await _context.SaveChangesAsync();

            _context.Entry(model).State = EntityState.Detached;
        }

        public async Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> expression) where T  : class
        {
            return await this.Query<T>().AnyAsync(expression);
        }

        public async Task<PagedList<T>> GetPagedListAsync<T>(IQueryable<T> query, PaginationFilter filter) where T : class
        {
            var total = await this.Query<T>().CountAsync();
            var totalFiltered = await query.CountAsync();

            var page = filter.Page > 0 ? filter.Page : 1;
            var perPage = filter.PerPage > 0 ? filter.PerPage : 15;

            var queryAplyOrder = ApplyOrder(query, filter)
                .Skip((page - 1) * filter.PerPage).Take(perPage);

            var result = queryAplyOrder.ToList();

            return new PagedList<T>(total, totalFiltered, page, perPage, result);
        } 

        private IOrderedEnumerable<T> ApplyOrder<T>(IQueryable<T> query, PaginationFilter filter)
        {
            Func<T, object> order = x =>
            {
                var type = x.GetType();

                try
                {
                    return ReflectionUtil.GetPropertyValue(x, filter.OrderBy ?? "Id");
                }
                catch (Exception)
                {
                    return null;
                }
            };

            if(filter.OrderByDirection != null && filter.OrderByDirection.ToLower() == "desc")
            {
                return query.OrderByDescending(order);
            }
            else
            {
                return query.OrderBy(order);
            }
        }
    }
}
