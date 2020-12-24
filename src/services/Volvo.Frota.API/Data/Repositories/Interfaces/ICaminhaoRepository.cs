using System.Threading.Tasks;
using Volvo.Frota.API.Models;

namespace Volvo.Frota.API.Data.Repositories.Interfaces
{
    public interface ICaminhaoRepository : IRepository<Caminhao>
    {
        Task PartialUpdateAsync(Caminhao src, params string[] properties);
    }
}
