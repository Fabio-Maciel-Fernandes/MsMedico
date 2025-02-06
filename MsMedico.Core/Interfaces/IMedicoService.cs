using MsMedico.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MsMedico.Core.Interfaces
{
    public interface IMedicoService
    {
        Medico Login(string crm, string senha);
        void CadastrarHorario(string crm, Horario horario);
        void EditarHorario(string crm, Horario horario);
        void AceitarConsulta(string crm, Consulta consulta);
        void RecusarConsulta(string crm, Consulta consulta);
        List<Medico> ObterMedicos(string especialidade, double latitude, double longitude, double? distancia, double? avaliacao);
        void AgendarConsulta(string crm, Consulta consulta);
        void CancelarConsulta(string crm, int consultaId, string justificativa);
        void AdicionarRoleAoMedico(string crm, string role);
        List<string> ObterRolesDoMedico(string crm);
    }
}
