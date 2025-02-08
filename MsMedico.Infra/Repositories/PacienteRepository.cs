using MsMedico.Core.Interfaces;
using MsMedico.Core.Models;
using MsMedico.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Infra.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private static List<Paciente> Pacientes = new List<Paciente>();

        public PacienteRepository()
        {
            if (Pacientes.Count == 0)
            {
                Pacientes.Add(new Paciente
                {
                    Nome = "Fábio Maciel Fernandes".Encrypt(),
                    CPF = "89553608191".Encrypt(),
                    Email = "fabbao@gmail.com".Encrypt(),
                    Senha = "123456".Encrypt(),
                    Role = "Paciente"
                });

                Pacientes.Add(new Paciente
                {
                    Nome = "Maria Oliveira".Encrypt(),
                    CPF = "98765432100".Encrypt(),
                    Email = "maria@example.com".Encrypt(),
                    Senha = "654321".Encrypt(),
                    Role = "Paciente"
                });
            }
        }

        public void AtualizarPaciente(Paciente paciente)
        {
            var index = Pacientes.FindIndex(p => p.CPF.Decrypt() == paciente.CPF.Decrypt());
            if (index != -1)
            {
                Pacientes[index] = paciente;
            }
        }

        public Paciente ObterPacientePorCPF(string cpf)
        {
            return Pacientes.FirstOrDefault(p => p.CPF == cpf.Encrypt());
        }

        public Paciente ObterPacientePorEmailESenha(string email, string senha)
        {
            return Pacientes.FirstOrDefault(p => p.Email.Decrypt() == email && p.Senha.Decrypt() == senha);
        }

        public List<Paciente> ObterPacientes()
        {
            return Pacientes.Select(p => new Paciente
            {
                Nome = p.Nome.Decrypt(),
                CPF = p.CPF.Decrypt(),
                Email = p.Email.Decrypt(),
                Senha = p.Senha.Decrypt(),
                Role = p.Role,
                ConsultasAgendadas = p.ConsultasAgendadas
            }).ToList();
        }

        public List<string> ObterRolesDoPaciente(string cpf)
        {
            var paciente = ObterPacientePorCPF(cpf);
            return paciente != null ? new List<string> { paciente.Role } : new List<string>();
        }
    }
}
