using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volvo.Frota.API.Utils.Pagination;

namespace Volvo.Frota.API.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<PagedList<T>> GetAllPaginated(PaginationFilter filter);
        Task<int> CreateAsync(T model);
        Task UpdateAsync(T model);
        Task RemoveAsync(T model);
        Task<bool> ExistAsync(Expression<Func<T, bool>> expression);
    }
}
