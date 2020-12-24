using FluentValidation.Results;
using System;

namespace Volvo.Frota.API.Utils.Result
{
    public static class OperationResult
    {
        public static Result Created<T>(T value) => new Result(true, value, OperationTypeResult.Created);
        public static Result Created() => new Result(true, OperationTypeResult.Created);
        public static Result NoContent() => new Result(true, OperationTypeResult.NoContent);
        public static Result NotFound() => new Result(false, OperationTypeResult.NotFound);
        public static Result ValidationError(ValidationResult validationResult) => new Result(false, validationResult, OperationTypeResult.ValidationError);
        public static Result OK<T>(T value) => new Result(true, value, OperationTypeResult.Ok);
        public static Result Error<T>(T value) => new Result(false, value, OperationTypeResult.Error);
        public static Result Error(string message) => new Result(false, message, OperationTypeResult.Error);
    }
}
