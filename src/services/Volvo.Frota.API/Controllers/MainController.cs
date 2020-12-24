using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Volvo.Frota.API.Utils.Result;

namespace Volvo.Frota.API.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected List<string> Errors = new List<string>();

        protected IActionResult CustomResponse(Result result = null)
        {
            switch (result?.OperationTypeResult)
            {
                case OperationTypeResult.ValidationError:
                    return ValidationError(result.ValidationResult);

                case OperationTypeResult.NoContent:
                    return NoContent();

                case OperationTypeResult.Created:
                    return StatusCode(StatusCodes.Status201Created);

                case OperationTypeResult.NotFound:
                    return NotFound();
            }

            return Ok(result);
        }

        protected IActionResult ValidationError(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErrosProcessamento(erro.ErrorMessage);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagem", Errors.ToArray()}
            }));
        }

        protected void AdicionarErrosProcessamento(string erro)
        {
            Errors.Add(erro);
        }

        protected void LimparProcessamento()
        {
            Errors.Clear();
        }
    }
}
