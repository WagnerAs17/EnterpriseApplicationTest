using System;
using Volvo.Frota.API.Utils.Enum;

namespace Volvo.Frota.API.Dtos
{
    public class CaminhaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ModeloCaminhao Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
    }
}
