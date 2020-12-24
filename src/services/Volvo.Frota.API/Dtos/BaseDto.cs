using FluentValidation.Results;
using System;

namespace Volvo.Frota.API.Dtos
{
    public abstract class BaseDto
    {
        private ValidationResult ValidationResult { get; set; }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }

        public void AdicionarErroResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ObterValidationResult()
        {
            return ValidationResult;
        }
    }
}
