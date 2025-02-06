using MsMedico.Core.Interfaces;
using MsMedico.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Infra.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public void AdicionarAvaliacao(string crm, Avaliacao avaliacao)
        {
            _avaliacaoRepository.AdicionarAvaliacao(crm, avaliacao);
        }

        public double ObterMediaAvaliacao(string crm)
        {
            return _avaliacaoRepository.CalcularMediaAvaliacao(crm);
        }
    }
}