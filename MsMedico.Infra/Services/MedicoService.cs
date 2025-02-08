using MsMedico.Core.Interfaces;
using MsMedico.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Infra.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IDistanciaService _distanciaService;

        public MedicoService(IMedicoRepository medicoRepository, IDistanciaService distanciaService)
        {
            _medicoRepository = medicoRepository;
            _distanciaService = distanciaService;
        }

        public Medico Login(string crm, string senha)
        {
            return _medicoRepository.ObterMedicoPorCrmESenha(crm, senha);
        }

        public void CadastrarHorario(string crm, Horario horario)
        {
            var medico = _medicoRepository.ObterMedicoPorCrm(crm);
            if (medico != null)
            {
                medico.HorariosDisponiveis.Add(horario);
                _medicoRepository.AtualizarMedico(medico);
            }
        }

        public void EditarHorario(string crm, Horario horario)
        {
            var medico = _medicoRepository.ObterMedicoPorCrm(crm);
            if (medico != null)
            {
                var h = medico.HorariosDisponiveis.FirstOrDefault(h => h.Id == horario.Id);
                if (h != null)
                {
                    medico.HorariosDisponiveis.Remove(h);
                    medico.HorariosDisponiveis.Add(horario);
                    _medicoRepository.AtualizarMedico(medico);
                }
            }
        }

        public void AceitarConsulta(string crm, Consulta consulta)
        {
            var medico = _medicoRepository.ObterMedicoPorCrm(crm);
            if (medico != null)
            {
                var c = medico.ConsultasAgendadas.FirstOrDefault(c => c.Id == consulta.Id);
                if (c != null)
                {
                    c.Aceita = true;
                    _medicoRepository.AtualizarMedico(medico);
                }
            }
        }

        public void RecusarConsulta(string crm, Consulta consulta)
        {
            var medico = _medicoRepository.ObterMedicoPorCrm(crm);
            if (medico != null)
            {
                var c = medico.ConsultasAgendadas.FirstOrDefault(c => c.Id == consulta.Id);
                if (c != null)
                {
                    c.Aceita = false;
                    _medicoRepository.AtualizarMedico(medico);
                }
            }
        }

        public Medico ObterMedicoPorCrm(string crm)
        {
            return _medicoRepository.ObterMedicoPorCrm(crm);
        }

        public List<Medico> ObterMedicos(string especialidade, double latitude, double longitude, double? distancia, double? avaliacao)
        {
            return _medicoRepository.ObterMedicosComFiltros(especialidade, latitude, longitude, distancia, avaliacao)
                .Where(m => _distanciaService.ObterDistanciaHaversine(latitude, longitude, m.Latitude, m.Longitude) <= 100)
                .Select(m => new Medico
                {
                    Nome = m.Nome,
                    CRM = m.CRM,
                    Senha = m.Senha,
                    Especialidade = m.Especialidade,
                    DistanciaKm = _distanciaService.ObterDistanciaHaversine(latitude, longitude, m.Latitude, m.Longitude),
                    Avaliacao = m.Avaliacao,
                    HorariosDisponiveis = m.HorariosDisponiveis,
                    ConsultasAgendadas = m.ConsultasAgendadas,
                    Role = m.Role,
                    Latitude = m.Latitude,
                    Longitude = m.Longitude,
                    ValorConsulta = m.ValorConsulta
                })
                .Where(m => (distancia == null || m.DistanciaKm <= distancia)
                        && (avaliacao == null || m.Avaliacao >= avaliacao))
                .OrderBy(m => m.DistanciaKm)
                .Take(10)
                .ToList();
        }

        public void AgendarConsulta(string crm, Consulta consulta)
        {
            var medico = _medicoRepository.ObterMedicoPorCrm(crm);
            if (medico != null)
            {
                medico.ConsultasAgendadas.Add(consulta);
                _medicoRepository.AtualizarMedico(medico);
            }
        }

        public void CancelarConsulta(string crm, int consultaId, string justificativa)
        {
            var medico = _medicoRepository.ObterMedicoPorCrm(crm);
            if (medico != null)
            {
                var consulta = medico.ConsultasAgendadas.FirstOrDefault(c => c.Id == consultaId);
                if (consulta != null)
                {
                    medico.ConsultasAgendadas.Remove(consulta);
                    _medicoRepository.AtualizarMedico(medico);
                }
            }
        }

        public void AdicionarRoleAoMedico(string crm, string role)
        {
            _medicoRepository.AdicionarRoleAoMedico(crm, role);
        }

        public List<string> ObterRolesDoMedico(string crm)
        {
            return _medicoRepository.ObterRolesDoMedico(crm);
        }

        public AgendaMedico ObterAgenda(string crm)
        {
            var medico = _medicoRepository.ObterMedicoPorCrm(crm);
            if (medico == null)
            {
                return null;
            }

            return new AgendaMedico(medico);
        }
    }
}
