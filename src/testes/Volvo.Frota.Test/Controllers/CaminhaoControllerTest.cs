using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volvo.Frota.API.Utils.Messages;
using Volvo.Frota.Test.Fixture;
using Volvo.Frota.Test.Stub;
using Xunit;

namespace Volvo.Frota.Test.Controllers
{
    [Collection(nameof(CaminhaoFixtureCollection))]
    public class CaminhaoControllerTest
    {
        private readonly CaminhaoTestFixture _caminhaoTestFixture;
        public CaminhaoControllerTest(CaminhaoTestFixture caminhaoTestFixture)
        {
            _caminhaoTestFixture = caminhaoTestFixture;
        }

        [Fact(DisplayName = "Cadastro realizado com sucesso.")]
        [Trait("Caminhao", "cadastrar")]
        public async Task Post_CadastrarCaminhao_CadastroRealizadoComSucessoDeveRetornarCreated()
        {
            //ARRANGE
            var stringContent = ObterDado(NewCaminhaoDtoStub.CompletoModeloCaminhaoFH());

            //ACT
            var response = await _caminhaoTestFixture.HttpClient.PostAsync("api/caminhoes", stringContent);

            //ASSERT
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact(DisplayName = "Cadastro informando ano do modelo inválido.")]
        [Trait("Caminhao", "cadastrar")]
        public async Task Post_CadastrarCaminhao_CadastroInformandoAnoDoModeloInvalidoDeveRetornarBadRequest()
        {
            //ARRANGE
            var stringContent = ObterDado(NewCaminhaoDtoStub.AnoModeloInvalido());

            //ACT
            var response = await _caminhaoTestFixture.HttpClient.PostAsync("api/caminhoes", stringContent);

            //ASSERT
            var @string = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(CaminhaoValidationMessage.AnoModeloInvalido, @string);
        }

        [Fact(DisplayName = "Cadastro informando modelo de caminhão invalido.")]
        [Trait("Caminhao", "cadastrar")]
        public async Task Post_CadastrarCaminhao_CadastroInformandoModeloDoCaminhaoInvalidoDeveRetornarBadRequest()
        {
            //ARRANGE
            var stringContent = ObterDado(NewCaminhaoDtoStub.ModeloCaminhaoInvalido());

            //ACT
            var response = await _caminhaoTestFixture.HttpClient.PostAsync("api/caminhoes", stringContent);

            //ASSERT
            var @string = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(CaminhaoValidationMessage.ModeloInvalido, @string);
        }

        [Fact(DisplayName = "Cadastro não informando o nome.")]
        [Trait("Caminhao", "cadastrar")]
        public async Task Post_CadastrarCaminhao_CadastroSemONomeDeveRetornarBadRequest()
        {
            //ARRANGE
            var stringContent = ObterDado(NewCaminhaoDtoStub.SemNome());

            //ACT
            var response = await _caminhaoTestFixture.HttpClient.PostAsync("api/caminhoes", stringContent);

            //ASSERT
            var @string = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(CaminhaoValidationMessage.NomeObrigatorio, @string);
        }

        [Fact(DisplayName = "Atualização de caminhão com sucesso.")]
        [Trait("Caminhao", "atualizar")]
        public async Task Put_AtualizarCaminhao_AtualizacaoComSucessoDeveRetornarNoContent()
        {
            //ARRANGE
            var id = await CadastrarNovoCaminhaoRetornarId(_caminhaoTestFixture.HttpClient);

            var stringContent =  ObterDado(UpdateCaminhaoDtoStub.Completo(id));
            //ACT
            var response = await _caminhaoTestFixture.HttpClient.PutAsync("api/caminhoes", stringContent);

            //ASSERT
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact(DisplayName = "Atualização com ano do modelo inválido.")]
        [Trait("Caminhao", "atualizar")]
        public async Task Put_AtualizarCaminhao_AnoDoModeloInvalidoDeveRetornarBadRequest()
        {
            //ARRANGE
            var id = await CadastrarNovoCaminhaoRetornarId(_caminhaoTestFixture.HttpClient);

            var stringContent = ObterDado(UpdateCaminhaoDtoStub.AnoInvalido(id));
            //ACT
            var response = await _caminhaoTestFixture.HttpClient.PutAsync("api/caminhoes", stringContent);

            //ASSERT
            var @string = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(CaminhaoValidationMessage.AnoModeloInvalido, @string);
        }

        [Fact(DisplayName = "Atualização com modelo do caminhão inválido.")]
        [Trait("Caminhao", "atualizar")]
        public async Task Put_AtualizarCaminhao_InformandoUmModeloDeCaminhaoInvalidoDeveRetornarBadRequest()
        {
            //ARRANGE
            var id = await CadastrarNovoCaminhaoRetornarId(_caminhaoTestFixture.HttpClient);

            var stringContent = ObterDado(UpdateCaminhaoDtoStub.ModeloInvalido(id));
            //ACT
            var response = await _caminhaoTestFixture.HttpClient.PutAsync("api/caminhoes", stringContent);

            //ASSERT
            var @string = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(CaminhaoValidationMessage.ModeloInvalido, @string);
        }

        [Fact(DisplayName = "Atualização com do caminhão com id inválido.")]
        [Trait("Caminhao", "atualizar")]
        public async Task Put_AtualizarCaminhao_InformandoIdInvalidoDeveRetornarBadRequest()
        {
            //ARRANGE
            var id = 0;
            var stringContent = ObterDado(UpdateCaminhaoDtoStub.IdInvalido(id));

            //ACT
            var response = await _caminhaoTestFixture.HttpClient.PutAsync("api/caminhoes", stringContent);

            //ASSERT
            var @string = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(CaminhaoValidationMessage.IdObrigatorio, @string);
        }

        [Fact(DisplayName = "Removendo caminhão com sucesso.")]
        [Trait("Caminhao", "remover")]
        public async Task Delete_RemoverCaminhao_RemovendoOCaminhaoComSucessoDeveRetornarNoContent()
        {
            //ARRANGE
            var id = await CadastrarNovoCaminhaoRetornarId(_caminhaoTestFixture.HttpClient);

            //ACT
            var response = await _caminhaoTestFixture.HttpClient.DeleteAsync($"api/caminhoes/{id}");

            //ASSERT
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact(DisplayName = "Removendo o caminhão e id não encontrado.")]
        [Trait("Caminhao", "remover")]
        public async Task Delete_RemoverCaminhao_IdInformanodNaoEncontradoDeveRetornarNotFound()
        {
            //ARRANGE
            var id = 0;

            //ACT
            var response = await _caminhaoTestFixture.HttpClient.DeleteAsync($"api/caminhoes/{id}");

            //ASSERT
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact(DisplayName = "Obtendo caminhão com sucesso.")]
        [Trait("Caminhao", "obter")]
        public async Task GetById_ObterCaminhao_ObtendoOObjetoComSucessoDeveRetornarOK()
        {
            //ARRANGE
            var id = await CadastrarNovoCaminhaoRetornarId(_caminhaoTestFixture.HttpClient);

            //ACT
            var response = await _caminhaoTestFixture.HttpClient.GetAsync($"api/caminhoes/{id}");

            //ASSERT
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Obtendo caminhão id não encontrado com sucesso.")]
        [Trait("Caminhao", "obter")]
        public async Task GetById_ObterCaminhao_IdInformadoNaoEncontradoOK()
        {
            //ARRANGE
            var id = 0;

            //ACT
            var response = await _caminhaoTestFixture.HttpClient.GetAsync($"api/caminhoes/{id}");

            //ASSERT
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<int> CadastrarNovoCaminhaoRetornarId(HttpClient client)
        {
            var response = await CadastrarCaminhao(client);

            return ObterIdAposNovoCadastro(response);
        }

        private async Task<HttpResponseMessage> CadastrarCaminhao(HttpClient client)
        {
            var stringContent = ObterDado(NewCaminhaoDtoStub.CompletoModeloCaminhaoFH());

            var response = await client.PostAsync("api/caminhoes", stringContent);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private int ObterIdAposNovoCadastro(HttpResponseMessage response)
        {
            return Convert.ToInt32(response.Headers.Location.LocalPath.Split("/").Last());
        }

        private StringContent ObterDado(object dado)
        {
            return new StringContent
            (
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json"
            );
        }
    }
}
