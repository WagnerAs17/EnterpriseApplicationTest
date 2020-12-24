using System.Threading.Tasks;
using Volvo.Frota.API.Dtos;
using Volvo.Frota.API.Dtos.Pagination;
using Volvo.Frota.API.Utils.Result;

namespace Volvo.Frota.API.Business.Interfaces
{
    public interface ICaminhaoBusiness
    {
        Task<Result> GetByIdAsync(int id);
        Task<Result> AddAsync(NewCaminhaoDto caminhaoDto);
        Task<Result> UpdateAsync(UpdateCaminhaoDto caminhaoDto);
        Task<Result> GetAllPaginatedAsync(PaginationFilterDto paginationFilter);
        Task<Result> RemoveAsync(int id);
    }
}
