using System;

namespace Volvo.Frota.API.Models.Interfaces
{
    public interface ISoftDelete
    {
        bool Excluido { get; set; }
        DateTime? ExcluidoEm { get; set; }
    }
}
