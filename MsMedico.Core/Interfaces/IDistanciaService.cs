using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Interfaces
{
    public interface IDistanciaService
    {
        public double ObterDistanciaHaversine(double latitudePontoA, double longitudePontoA, double latitudePontoB, double longitudePontoB);
    }
}
