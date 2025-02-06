using MsMedico.Core.Models;
using MsMedico.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Infra.Services
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly IMedicoRepository _medicoRepository;

        public AvaliacaoRepository(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public void AdicionarAvaliacao(string crm, Avaliacao avaliacao)
        {
            var medico = _medicoRepository.ObterMedicoPorCrm(crm);
            if (medico != null)
            {
                medico.Avaliacoes.Add(avaliacao);
                _medicoRepository.AtualizarMedico(medico);
            }
        }

        public double CalcularMediaAvaliacao(string crm)
        {
            var medico = _medicoRepository.ObterMedicoPorCrm(crm);
            if (medico != null && medico.Avaliacoes.Any())
            {
                return medico.Avaliacoes.Average(a => a.Pontuacao);
            }
            return 0;
        }      
    }
}
