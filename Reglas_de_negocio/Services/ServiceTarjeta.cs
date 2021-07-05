using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas_de_negocio.Services
{
    public class ServiceTarjeta
    {
        public bool ValidarTarjeta(string numtarjeta)
        {
            return numtarjeta.ToString().All(char.IsDigit) && numtarjeta.ToString().Reverse()
                .Select(c => c - 48)
                .Select((thisNum, i) => i % 2 == 0
                    ? thisNum
                    : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                ).Sum() % 10 == 0;
        }
    }
}
