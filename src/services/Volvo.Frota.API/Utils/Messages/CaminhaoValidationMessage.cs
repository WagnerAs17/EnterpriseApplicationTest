namespace Volvo.Frota.API.Utils.Messages
{
    public class CaminhaoValidationMessage
    {
        public const string NomeObrigatorio = "O nome é obrigatório.";
        public const string ModeloInvalido = "O modelo de caminhão informado é inválido.";
        public const string AnoModeloInvalido = "O ano do modelo é invalido. Deve ser ano atual ou subsequente.";
        public const string IdObrigatorio = "Id informado é inválido.";
    }
}
