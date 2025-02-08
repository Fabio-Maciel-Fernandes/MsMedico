using MsMedico.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Interfaces
{
    public interface IPacienteRepository
    {
        void AtualizarPaciente(Paciente paciente);
        Paciente ObterPacientePorCPF(string cpf);
        Paciente ObterPacientePorEmailESenha(string email, string senha);
        List<Paciente> ObterPacientes();
        List<string> ObterRolesDoPaciente(string cpf);
    }
}
