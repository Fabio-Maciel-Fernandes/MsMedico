
using MsMedico.Core.Models;

namespace MsMedico.Core.Interfaces
{
    public interface IAvaliacaoService
    {
        void AdicionarAvaliacao(string crm, Avaliacao avaliacao);
        double CalcularMediaAvaliacao(string crm);
        double ObterMediaAvaliacao(string crm);
    }
}