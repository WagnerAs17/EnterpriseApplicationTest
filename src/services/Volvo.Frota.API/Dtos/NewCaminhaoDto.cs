using FluentValidation;
using Volvo.Frota.API.Utils.Enum;
using Volvo.Frota.API.Utils.Messages;
using Volvo.Frota.API.Utils.Valildations;

namespace Volvo.Frota.API.Dtos
{
    public class NewCaminhaoDto : BaseDto
    {
        public string Nome { get; set; }
        public ModeloCaminhao Modelo { get; set; }
        public int AnoModelo { get; set; }

        public override bool EhValido()
        {
            AdicionarErroResult(new RegistrarNovoCaminhaoValidation().Validate(this));

            return ObterValidationResult().IsValid;
        }
    }

    public class RegistrarNovoCaminhaoValidation : AbstractValidator<NewCaminhaoDto>
    {
        public RegistrarNovoCaminhaoValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(CaminhaoValidationMessage.NomeObrigatorio);

            RuleFor(x => x.Modelo)
                .Must(CaminhaoValidation.ValidarModelo)
                .WithMessage(CaminhaoValidationMessage.ModeloInvalido);

            RuleFor(x => x.AnoModelo)
                .Must(CaminhaoValidation.ValidarAnoModelo)
                .WithMessage(CaminhaoValidationMessage.AnoModeloInvalido);
        }
    }
}
