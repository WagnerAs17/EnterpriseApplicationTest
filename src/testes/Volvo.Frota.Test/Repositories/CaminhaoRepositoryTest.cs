using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volvo.Frota.API.Data.Repositories;
using Volvo.Frota.API.Models;
using Volvo.Frota.API.Utils.Enum;
using Volvo.Frota.API.Utils.Pagination;
using Xunit;

namespace Volvo.Frota.Test.Repositories
{

    public class CaminhaoRepositoryTest : RepositoryTest
    {
        private readonly IQueryable<Caminhao> _defaultQuery;
        public CaminhaoRepositoryTest()
        {
            _defaultQuery = Entities.Query<Caminhao>();
        }

        [Theory(DisplayName = "Obtendo o caminhao por id retornando o objeto.")]
        [InlineData(1)]
        [Trait("Caminhao", "Obter")]
        public async Task GetByIdAsync_ObterCaminhao_ComSucesso(int id)
        {
            //ARRANGE
            var novoCaminhao = CreateCaminhaoObject();
            await  Entities.AddAsync(novoCaminhao);

            //ACT
            var caminhao = await GetCaminhao(id);

            //ASSERT
            Assert.Equal(novoCaminhao, caminhao);
        }

        [Theory(DisplayName = "Obtendo o caminhao por id não encontrando o objeto.")]
        [InlineData(0)]
        [Trait("Caminhao", "Obter")]
        public async Task GetByIdAsync_ObterCaminhao_NaoEncontrandoObjetoComOIdInformado(int id)
        {
            var caminhao = await GetCaminhao(id);

            //ASSERT
            Assert.Null(caminhao);
        }

        [Fact(DisplayName = "Obtendo os dados da base paginado.")]
        [Trait("Caminhao", "Obter")]
        public async Task GetAllPaginated_Caminhao_ObterTodosOsRegistrosPaginado()
        {
            //ARRANGE
            var volvoFH = CreateCaminhaoObject();
            var volvoFM = CreateCaminhaoObject();

            await Entities.AddAsync(volvoFH);
            await Entities.AddAsync(volvoFM);

            //ACT
            var parameterPagination = ParametersPagination();
            var result = await Entities.GetPagedListAsync(_defaultQuery, parameterPagination);

            //ASSERT
            Assert.Equal(parameterPagination.Page, result.Page);
            Assert.Equal(parameterPagination.PerPage, result.PerPage);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal(0, result.NextPageNumber);
            Assert.False(result.HasNextPage);
            Assert.False(result.HasPreviousPage);
        }

        [Fact(DisplayName = "Adicionando um novo caminhao no banco de dados com sucesso.")]
        [Trait("Caminhao", "Cadastrar")]
        public async Task AddAsync_Caminhao_ComSucesso()
        {
            //ARRANGEs
            var caminhao = CreateCaminhaoObject();

            //ACT
            await Entities.AddAsync(caminhao);
            
            //ASSERT
            Assert.True(caminhao.Id > 0);
        }

        [Fact(DisplayName = "Realizando update do caminhao com sucesso.")]
        [Trait("Caminhao", "Atualizar")]
        public async Task UpdateAsync_Caminhao_ComSucesso()
        {
            //ARRANGE
            var novoCaminhao = CreateCaminhaoObject();
            await Entities.AddAsync(novoCaminhao);

            var caminhaoUpdate = CreateCaminhaoObject();
            caminhaoUpdate.Id = novoCaminhao.Id;

            //ACT
            await Entities.UpdateAsync(caminhaoUpdate);
            
            var caminhao = await GetCaminhao(caminhaoUpdate.Id);
            //ASSERT
            Assert.NotEqual(novoCaminhao.Nome, caminhao.Nome);
        }

        [Fact(DisplayName = "Realizando Update partial realizado com sucesso.")]
        [Trait("Caminhao", "Atualizar")]
        public async Task PartialUpdateAsync_Caminhao_ComSucesso()
        {
            //ARRANGE
            var novoCaminhao = CreateCaminhaoObject();
            await Entities.AddAsync(novoCaminhao);
            var propertiesUpdate = PropertiesToUpdate(novoCaminhao);

            //ACT
            await Entities.PartialUpdateAsync(novoCaminhao, propertiesUpdate);

            //ASSERT
            var caminhao = await GetCaminhao(novoCaminhao.Id);
            Assert.NotEqual(0, caminhao.AnoFabricacao);
        }

        [Fact(DisplayName = "Removendo caminhao da base de dados com sucessso.")]
        [Trait("Caminhao", "Remover")]
        public async Task RemoveAsync_RemovendoCaminhao_ObjetoCaminhaoRemovidoComSucesso()
        {
            //ARRANGE
            var novoCaminhao = CreateCaminhaoObject();
            await Entities.AddAsync(novoCaminhao);

            //ACT
            await Entities.RemoveAsync(novoCaminhao);

            //ASSERT
            var caminhao = await GetCaminhao(novoCaminhao.Id);
            Assert.Null(caminhao);
        }

        [Fact(DisplayName = "Verficando nome de caminhao registrado na base de dados.")]
        [Trait("Caminhao", "Verificar")]
        public async Task ExistAsync_Caminhao_VerificandoSeExisteCaminhaoRetornandoTrue()
        {
            //ARRANGE
            var novoCaminhao = CreateCaminhaoObject();
            await Entities.AddAsync(novoCaminhao);

            //ACT
            var result = await Entities
                .ExistsAsync<Caminhao>(x => x.Nome.Equals(novoCaminhao.Nome));

            //ASSERT
            Assert.True(result);
        }

        [Fact(DisplayName = "Verificando nome de caminhao não registrado na base de dados.")]
        [Trait("Caminhao", "Verificar")]
        public async Task ExistAsync_Caminhao_VerificandoSeExisteCaminhaoRetornandoFalse()
        {
            //ARRANGE
            var novoCaminhao = CreateCaminhaoObject();
            await Entities.AddAsync(novoCaminhao);

            //ACT
            var result = await Entities
                .ExistsAsync<Caminhao>(x => x.Nome.Equals("Caminhao"));

            //ASSERT
            Assert.False(result);
        }

        

        private async Task<Caminhao> GetCaminhao(int id)
        {
            return await _defaultQuery.FirstOrDefaultAsync(x => x.Id == id);
        }

        private Caminhao CreateCaminhaoObject()
        {
            var faker = new Faker("pt_BR");

            return new Caminhao
            {
                Nome = faker.Random.String2(15),
                AnoFabricacao = DateTime.Now.Year,
                AnoModelo = DateTime.Now.Year + 1,
                Modelo = ModeloCaminhao.FM
            };
        }

        public string[] PropertiesToUpdate(Caminhao caminhao)
        {
            return new string[] 
                {
                    nameof(caminhao.AnoModelo),
                    nameof(caminhao.Modelo),
                    nameof(caminhao.Nome)
                };
        }

        private PaginationFilter ParametersPagination()
        {
            return new PaginationFilter
            {
                Search = "",
                OrderBy = "Nome",
                OrderByDirection = "desc",
                Page = 1,
                PerPage = 5
            };
        }
    }
}
