using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volvo.Frota.API.Business;
using Volvo.Frota.API.Business.Interfaces;
using Volvo.Frota.API.Data.Repositories.Interfaces;
using Volvo.Frota.API.Models;
using Volvo.Frota.API.Utils.Messages;
using Volvo.Frota.API.Utils.Result;
using Volvo.Frota.Test.Stub;
using Xunit;

namespace Volvo.Frota.Test.Business
{
    public class CaminhaoBusinessTest : BusinessTest
    {
        private readonly Mock<ICaminhaoRepository> _caminhaoRepositoryMock;
        private readonly Mock<ICaminhaoBusiness> _caminhaoBusinessMock;
        private readonly CaminhaoBusiness _caminhaoBusiness;

        public CaminhaoBusinessTest()
        {
            _caminhaoRepositoryMock = new Mock<ICaminhaoRepository>();
            _caminhaoBusinessMock = new Mock<ICaminhaoBusiness>();

            _caminhaoBusiness = new CaminhaoBusiness(_caminhaoRepositoryMock.Object, Mapper);
        }

        [Fact(DisplayName = "Cadastrando caminhao com sucesso")]
        [Trait("Caminhao", "Cadastro")]
        public async Task AddAsync_CadastrarCaminhao_QuandoCadastrarCaminhaoComSucessoRetornarOperationResultOK() 
        {
            //ARRANGE
            _caminhaoRepositoryMock.Setup(x => 
                x.CreateAsync(It.IsAny<Caminhao>()))
                .ReturnsAsync(It.IsAny<int>);

            var caminhaoDto = NewCaminhaoDtoStub.CompletoModeloCaminhaoFH();
            _caminhaoBusinessMock.Setup(x => 
                x.AddAsync(caminhaoDto))
                .ReturnsAsync(OperationResult.Created());

            //ACT
            var caminhaoMock = await _caminhaoBusinessMock.Object.AddAsync(caminhaoDto);
            var caminhao = await _caminhaoBusiness.AddAsync(caminhaoDto);

            //ASSERT
            Assert.Equal(caminhaoMock.Success, caminhao.Success);
            Assert.Equal(caminhaoMock.OperationTypeResult, caminhao.OperationTypeResult);
        }

        [Fact(DisplayName = "Cadastrando caminhao com falha, nome é obrigatório.")]
        [Trait("Caminhao", "Cadastro")]
        public async Task AddAsync_CadastrarCaminhao_AoTentarCadastrarSemInformarONomeDeveRetornarOperationResultValidationError()
        {
            //ARRANGE
            _caminhaoRepositoryMock.Setup(x =>
                x.CreateAsync(It.IsAny<Caminhao>()))
                .ReturnsAsync(It.IsAny<int>);

            var caminhaoDto = NewCaminhaoDtoStub.SemNome();

            //ACT
            var caminhao = await _caminhaoBusiness.AddAsync(caminhaoDto);

            //ASSERT
            var messageError = caminhao.ValidationResult
                .Errors.Select(x => x.ErrorMessage.Contains(CaminhaoValidationMessage.NomeObrigatorio)).First();

            Assert.True(messageError);
            Assert.True(caminhao.Failure);
        }

        [Fact(DisplayName = "Cadastrando caminhao com falha. Ano do modelo inválido.")]
        [Trait("Caminhao", "Cadastro")]
        public async Task AddAsync_CadastrarCaminhao_AoTentarCadastrarInformandoAnoDoModeloInvalidoDeveRetornarOperationResultValidationError()
        {
            //ARRANGE
            _caminhaoRepositoryMock.Setup(x =>
                x.CreateAsync(It.IsAny<Caminhao>()))
                .ReturnsAsync(It.IsAny<int>);

            var caminhaoDto = NewCaminhaoDtoStub.AnoModeloInvalido();

            //ACT
            var caminhao = await _caminhaoBusiness.AddAsync(caminhaoDto);

            //ASSERT
            var messageError = caminhao.ValidationResult
                .Errors.Select(x => x.ErrorMessage.Contains(CaminhaoValidationMessage.AnoModeloInvalido)).First();

            Assert.True(messageError);
            Assert.True(caminhao.Failure);
        }

        [Fact(DisplayName = "Cadastrando caminhao com falha. Modelo do caminhao informado inválido.")]
        [Trait("Caminhao", "Cadastro")]
        public async Task AddAsync_CadastrarCaminhao_AoTentarCadastrarInformandoModeloDeCaminhaoInvalidoDeveRetornarOperationResultValidationError()
        {
            //ARRANGE
            _caminhaoRepositoryMock.Setup(x =>
                x.CreateAsync(It.IsAny<Caminhao>()))
                .ReturnsAsync(It.IsAny<int>);

            var caminhaoDto = NewCaminhaoDtoStub.ModeloCaminhaoInvalido();

            //ACT
            var caminhao = await _caminhaoBusiness.AddAsync(caminhaoDto);

            //ASSERT
            var messageError = caminhao.ValidationResult
                .Errors.Select(x => x.ErrorMessage.Contains(CaminhaoValidationMessage.ModeloInvalido)).First();

            Assert.True(messageError);
            Assert.True(caminhao.Failure);
        }

        [Fact(DisplayName = "Atualizaçao com sucesso")]
        [Trait("Caminhao", "Atualizar")]
        public async Task UpdateAsync_AtualizarCaminhao_AtualizacaoDeCaminhaoComSucessoDeveRetornarOperationResultNoContent()
        {
            //ARRANGE
            _caminhaoRepositoryMock.Setup(x =>
                x.UpdateAsync(It.IsAny<Caminhao>()))
                .Returns(Task.CompletedTask);

            var caminhaoDto = UpdateCaminhaoDtoStub.Completo(1);
            _caminhaoBusinessMock.Setup(x => x.UpdateAsync(caminhaoDto)).ReturnsAsync(OperationResult.NoContent());

            //ACT
            var caminhaoMock = await _caminhaoBusinessMock.Object.UpdateAsync(caminhaoDto);
            var caminhao = await _caminhaoBusiness.UpdateAsync(caminhaoDto);

            //ASSERT
            Assert.Equal(caminhaoMock.Success, caminhao.Success);
            Assert.Equal(caminhaoMock.OperationTypeResult, caminhao.OperationTypeResult);
        }

        [Fact(DisplayName = "Atualização sem informar o nome do caminhão.")]
        [Trait("Caminhao", "Atualizar")]
        public async Task UpdateAsync_AtualizarCaminhao_AoTentarAtualizarSemInformarONomeDoCaminhaoDeveRetornarOperationResultValidationError()
        {
            //ARRANGE
            _caminhaoRepositoryMock.Setup(x =>
                x.UpdateAsync(It.IsAny<Caminhao>()))
                .Returns(Task.CompletedTask);

            var caminhaoDto = UpdateCaminhaoDtoStub.SemNome(1);

            //ACT
            var caminhao = await _caminhaoBusiness.UpdateAsync(caminhaoDto);

            //ASSERT
            var messageError = caminhao.ValidationResult
                .Errors.Select(x => x.ErrorMessage.Contains(CaminhaoValidationMessage.NomeObrigatorio)).First();

            Assert.True(messageError);
            Assert.True(caminhao.Failure);
        }

        [Fact(DisplayName = "Atualização passando um ano inválido para o modelo.")]
        [Trait("Caminhao", "Atualizar")]
        public async Task UpdateAsync_AtualizarCaminhao_AoTentarAtualizarInformandoAnoInvalidoDeveRetornarOperationResultValidationError()
        {
            //ARRANGE
            _caminhaoRepositoryMock.Setup(x =>
                x.UpdateAsync(It.IsAny<Caminhao>()))
                .Returns(Task.CompletedTask);

            var caminhaoDto = UpdateCaminhaoDtoStub.AnoInvalido(1);

            //ACT
            var caminhao = await _caminhaoBusiness.UpdateAsync(caminhaoDto);

            //ASSERT
            var messageError = caminhao.ValidationResult
                .Errors.Select(x => x.ErrorMessage.Contains(CaminhaoValidationMessage.AnoModeloInvalido)).First();

            Assert.True(messageError);
            Assert.True(caminhao.Failure);
        }

        [Fact(DisplayName = "Atualização passando modelo de caminhao invalido.")]
        [Trait("Caminhao", "Atualizar")]
        public async Task UpdateAsync_AtualizarCaminhao_AoTentarAtualizarInformandoModeloDeCaminhaoInvalidoDeveRetornarOperationResultValidationError()
        {
            //ARRANGE
            _caminhaoRepositoryMock.Setup(x =>
                x.UpdateAsync(It.IsAny<Caminhao>()))
                .Returns(Task.CompletedTask);

            var caminhaoDto = UpdateCaminhaoDtoStub.ModeloInvalido(1);

            //ACT
            var caminhao = await _caminhaoBusiness.UpdateAsync(caminhaoDto);

            //ASSERT
            var messageError = caminhao.ValidationResult
                .Errors.Select(x => x.ErrorMessage.Contains(CaminhaoValidationMessage.ModeloInvalido)).First();

            Assert.True(messageError);
            Assert.True(caminhao.Failure);
        }

        [Fact(DisplayName = "Atualização passando id inválido.")]
        [Trait("Caminhao", "Atualizar")]
        public async Task UpdateAsync_AtualizarCaminhao_AoTentarAtualizarInformandoIdInvalidoDeveRetornarOperationResultValidationError()
        {
            //ARRANGE
            _caminhaoRepositoryMock.Setup(x =>
                x.UpdateAsync(It.IsAny<Caminhao>()))
                .Returns(Task.CompletedTask);

            var caminhaoDto = UpdateCaminhaoDtoStub.IdInvalido(0);

            //ACT
            var caminhao = await _caminhaoBusiness.UpdateAsync(caminhaoDto);

            //ASSERT
            var messageError = caminhao.ValidationResult
                .Errors.Select(x => x.ErrorMessage.Contains(CaminhaoValidationMessage.IdObrigatorio)).First();

            Assert.True(messageError);
            Assert.True(caminhao.Failure);
        }

        [Fact(DisplayName = "Objeto encontrado com sucesso.")]
        [Trait("Caminhao", "Obter")]
        public async Task GetByIdAsync_ObterCaminhao_AoEncontrarOobjectoDeveretornarOperationResultOk()
        {
            //ARRANGE
            int id = 1;
            _caminhaoRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(CaminhaoStub.GetById(id));
            _caminhaoBusinessMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(OperationResult.OK(CaminhaoStub.GetById(id)));


            //ACT
            var caminhaoMock = await _caminhaoBusinessMock.Object.GetByIdAsync(id);
            var caminhao = await _caminhaoBusiness.GetByIdAsync(id);

            //ASSERT
            Assert.Equal(caminhaoMock.OperationTypeResult, caminhao.OperationTypeResult);
            Assert.Equal(caminhaoMock.Success, caminhao.Success);
        }

        [Fact(DisplayName = "Objeto não encontrado.")]
        [Trait("Caminhao", "Obter")]
        public async Task GetByIdAsync_ObterCaminhao_QuandoNaoEncontrarObjetoDeveOperationResultNotFound()
        {
            //ARRANGE
            int id = 1;
            _caminhaoRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(CaminhaoStub.GetById(id));
            _caminhaoBusinessMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(OperationResult.NotFound());


            //ACT
            var caminhaoMock = await _caminhaoBusinessMock.Object.GetByIdAsync(id);
            var caminhao = await _caminhaoBusiness.GetByIdAsync(0);

            //ASSERT
            Assert.Equal(caminhaoMock.OperationTypeResult, caminhao.OperationTypeResult);
            Assert.Equal(caminhaoMock.Failure, caminhao.Failure);
        }

        [Fact(DisplayName = "Removendo caminhao da base com sucesso.")]
        [Trait("Caminhao", "Remover")]
        public async Task RemoveAsync_RemoverCaminhao_CaminhaoRemovidoComSucessoDeveRetornarOperationResultNoContent()
        {
            //ARRANGE
            var id = 1;
            _caminhaoRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(CaminhaoStub.GetById(id));
            _caminhaoRepositoryMock.Setup(x => x.RemoveAsync(CaminhaoStub.GetById(id))).Returns(Task.CompletedTask);

            //ACT
            var result = await _caminhaoBusiness.RemoveAsync(id);

            //ASSERT
            Assert.True(result.Success);
            Assert.Equal(OperationTypeResult.NoContent, result.OperationTypeResult);
        }

        [Fact(DisplayName = "Removendo caminhao da base com falha.")]
        [Trait("Caminhao", "Remover")]
        public async Task RemoveAsync_RemoverCaminhao_AoTentarRemoverOCaminhaoENaoEncontrarOObjetoComOIdInformadoRetornarOperationResultNotFound()
        {
            //ARRANGE
            var id = 1;
            _caminhaoRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(CaminhaoStub.GetById(id));
            _caminhaoRepositoryMock.Setup(x => x.RemoveAsync(CaminhaoStub.GetById(id))).Returns(Task.CompletedTask);

            //ACT
            var result = await _caminhaoBusiness.RemoveAsync(0);

            //ASSERT
            Assert.True(result.Failure);
            Assert.Equal(OperationTypeResult.NotFound, result.OperationTypeResult);
        }
    }
}
