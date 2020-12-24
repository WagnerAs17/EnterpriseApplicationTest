using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace Volvo.Frota.API.Utils.Result
{
    public class Result
    {
        public object Value { get; set; }
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
        [JsonIgnore]
        public OperationTypeResult OperationTypeResult { get; set; }
        [JsonIgnore]
        public bool Success { get; set; }
        [JsonIgnore]
        public bool Failure => !Success;

        public Result(bool success, object value, OperationTypeResult operationType)
        {
            Success = success;
            Value = value;
            OperationTypeResult = operationType;
        }

        public Result(bool sucess, OperationTypeResult operationType)
        {
            Success = sucess;
            OperationTypeResult = operationType;
        }

        public Result(bool success, ValidationResult validationResult, OperationTypeResult operationType)
        {
            Success = success;
            ValidationResult = validationResult;
            OperationTypeResult = operationType;
        }
    }
}
