using MsMedico.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Interfaces
{
    public interface IAvaliacaoRepository
    {
        void AdicionarAvaliacao(string crm, Avaliacao avaliacao);
        double CalcularMediaAvaliacao(string crm);
    }
}
