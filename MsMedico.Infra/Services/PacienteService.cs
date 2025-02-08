using MsMedico.Core.Interfaces;
using MsMedico.Core.Models;
using MsMedico.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Infra.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;

        public PacienteService(IPacienteRepository pacienteRepository, IMedicoRepository medicoRepository)
        {
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
        }

        public Paciente Authenticate(string email, string cpf, string password)
        {
            var paciente = _pacienteRepository.ObterPacientePorEmailESenha(email, password);
            return paciente?.CPF.Decrypt() == cpf ? paciente : null;
        }

        public void AgendarConsulta(string crm, Consulta consulta)
        {
            var medico = _medicoRepository.ObterMedicoPorCrm(crm);
            if (medico == null)
            {
                throw new Exception("Médico não encontrado");
            }

            //lock (medico.HorariosDisponiveisLock)
            {
                consulta.Id = medico.ConsultasAgendadas.Count() + 1;

                var horarioOcupado = medico.ConsultasAgendadas.Any(c => c.Data == consulta.Data);
                if (horarioOcupado)
                {
                    throw new Exception("Horário já ocupado");
                }

                consulta.Valor = medico.ValorConsulta;
                medico.ConsultasAgendadas.Add(consulta);
                _medicoRepository.AtualizarMedico(medico);

                var paciente = _pacienteRepository.ObterPacientePorCPF(consulta.PacienteCPF);
                paciente?.ConsultasAgendadas.Add(consulta);
                if (paciente != null)
                {
                    _pacienteRepository.AtualizarPaciente(paciente);
                }
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
                }
            }
        }
    }
}
