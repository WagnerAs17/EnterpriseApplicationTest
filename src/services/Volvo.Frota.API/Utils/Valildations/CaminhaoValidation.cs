using System;
using System.Threading.Tasks;
using Volvo.Frota.API.Data.Repositories.Interfaces;
using Volvo.Frota.API.Utils.Enum;

namespace Volvo.Frota.API.Utils.Valildations
{
    public class CaminhaoValidation
    {
        public static bool ValidarModelo(ModeloCaminhao modelo)
        {
            return modelo == ModeloCaminhao.FH || modelo == ModeloCaminhao.FM;
        }

        public static bool ValidarAnoModelo(int ano)
        {
            return ano == DateTime.Now.Year || ano == DateTime.Now.Year + 1;
        }   
    }
}
