using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volvo.Frota.API.Data.Repositories.Interfaces;
using Volvo.Frota.API.Models;
using Volvo.Frota.API.Utils.Pagination;

namespace Volvo.Frota.API.Data.Repositories
{
    public class CaminhaoRepository : ICaminhaoRepository
    {
        private readonly Entities _entities;
        private readonly IQueryable<Caminhao> _defaultQuery;

        public CaminhaoRepository(Entities entities)
        {
            _entities = entities;
            _defaultQuery = entities.Query<Caminhao>();
        }

        public async Task<PagedList<Caminhao>> GetAllPaginated(PaginationFilter filter)
        {
            var query = _defaultQuery;

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(x => x.Nome.Contains(filter.Search));

            return await _entities.GetPagedListAsync(query, filter);
        }

        public async Task<Caminhao> GetByIdAsync(int id)
        {
            return await _defaultQuery.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> CreateAsync(Caminhao caminhao)
        {
            await _entities.AddAsync(caminhao);

            return caminhao.Id;
        }

        public async Task UpdateAsync(Caminhao caminhao)
        {
            await _entities.UpdateAsync(caminhao);
        }

        public async Task PartialUpdateAsync(Caminhao caminhao, params string[] properties)
        {
            await _entities.PartialUpdateAsync(caminhao, properties);
        }

        public async Task RemoveAsync(Caminhao caminhao)
        {
            await _entities.RemoveAsync(caminhao);
        }

        public async Task<bool> ExistAsync(Expression<Func<Caminhao, bool>> expression)
        {
            return await _entities.ExistsAsync(expression);
        }
    }
}
