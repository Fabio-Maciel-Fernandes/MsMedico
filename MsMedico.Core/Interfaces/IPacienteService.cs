using MsMedico.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Interfaces
{
    public interface IPacienteService
    {
        Paciente Authenticate(string email, string cpf, string password);
        void AgendarConsulta(string crm, Consulta consulta);
        void CancelarConsulta(string crm, int consultaId, string justificativa);
    }
}
