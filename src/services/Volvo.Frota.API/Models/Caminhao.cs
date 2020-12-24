using System;
using Volvo.Frota.API.Models.Interfaces;
using Volvo.Frota.API.Utils.Enum;

namespace Volvo.Frota.API.Models
{
    public class Caminhao : Entity, ISoftDelete
    {
        public string Nome { get; set; }
        public ModeloCaminhao Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public bool Excluido { get; set; }
        public DateTime? ExcluidoEm { get ; set ; }
    }
}
