using MsMedico.Core.Interfaces;
using MsMedico.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsMedico.Core.Interfaces;
using MsMedico.Infra.Services;

namespace MsMedico.Infra.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private static List<Medico> Medicos = new List<Medico>();

        public MedicoRepository()
        {
            if (Medicos.Count == 0)
            {
                Medicos.Add(new Medico
                {
                    Nome = "Rodrigo Oliveira Fernandes",
                    CRM = "123456",
                    Senha = "123456",
                    Especialidade = "Cardiologia",
                    Avaliacao = 4.5,
                    Latitude = -27.43660580083045,
                    Longitude = -48.39629879463427,
                    ValorConsulta = 400,
                    Role = "Medico",
                    HorariosDisponiveis = new List<Horario>
                    {
                        new Horario { Id = 1, Data = DateTime.Today, HoraInicio = new TimeSpan(9, 0, 0), HoraFim = new TimeSpan(12, 0, 0) },
                        new Horario { Id = 2, Data = DateTime.Today.AddDays(1), HoraInicio = new TimeSpan(14, 0, 0), HoraFim = new TimeSpan(17, 0, 0) }
                    },
                    ConsultasAgendadas = new List<Consulta>
                    {
                        new Consulta { Id = 1, Data = DateTime.Today.AddDays(1), PacienteId = 1, Valor = 300, PacienteCPF = "89553608191" },
                        new Consulta { Id = 2, Data = DateTime.Today.AddDays(1).AddHours(1), PacienteId = 1, Valor = 300, PacienteCPF = "89553608191" },
                        new Consulta { Id = 3, Data = DateTime.Today.AddDays(1).AddHours(2), PacienteId = 1, Valor = 400, PacienteCPF = "89553608191" }
                    }
                });

                Medicos.Add(new Medico
                {
                    Nome = "Evandro Schimits",
                    CRM = "111111",
                    Senha = "111111",
                    Especialidade = "Cardiologia",
                    Avaliacao = 3,
                    Latitude = -27.600470793922568,
                    Longitude = -48.54948838054793,
                    ValorConsulta = 300,
                    Role = "Medico",
                    HorariosDisponiveis = new List<Horario>
                    {
                        new Horario { Id = 1, Data = DateTime.Today, HoraInicio = new TimeSpan(9, 0, 0), HoraFim = new TimeSpan(12, 0, 0) },
                        new Horario { Id = 2, Data = DateTime.Today.AddDays(1), HoraInicio = new TimeSpan(14, 0, 0), HoraFim = new TimeSpan(17, 0, 0) }
                    },
                    ConsultasAgendadas = new List<Consulta>
                    {
                        new Consulta { Id = 1, Data = DateTime.Today.AddDays(1), PacienteId = 2, Valor = 300, PacienteCPF = "98765432100" },
                        new Consulta { Id = 2, Data = DateTime.Today.AddDays(1).AddHours(1), PacienteId = 2, Valor = 300, PacienteCPF = "98765432100" },
                        new Consulta { Id = 3, Data = DateTime.Today.AddDays(1).AddHours(2), PacienteId = 2, Valor = 300, PacienteCPF = "98765432100" }
                    }
                });
            }
        }

        public void AdicionarRoleAoMedico(string crm, string role)
        {
            var medico = ObterMedicoPorCrm(crm);
            if (medico != null)
            {
                medico.Role = role;
                AtualizarMedico(medico);
            }
        }

        public void AtualizarMedico(Medico medico)
        {
            var index = Medicos.FindIndex(m => m.CRM == medico.CRM);
            if (index != -1)
            {
                Medicos[index] = medico;
            }
        }

        public Medico ObterMedicoPorCrm(string crm)
        {
            return Medicos.FirstOrDefault(m => m.CRM == crm);
        }

        public Medico ObterMedicoPorCrmESenha(string crm, string senha)
        {
            return Medicos.FirstOrDefault(m => m.CRM == crm);
        }

        public List<Medico> ObterMedicosComFiltros(string especialidade, double latitude, double longitude, double? distancia, double? avaliacao)
        {
            return Medicos
                .Where(m => (string.IsNullOrEmpty(especialidade) || m.Especialidade == especialidade))
                .ToList();
        }

        public List<string> ObterRolesDoMedico(string crm)
        {
            var medico = ObterMedicoPorCrm(crm);
            return medico != null ? new List<string> { medico.Role } : new List<string>();
        }
    }
}
