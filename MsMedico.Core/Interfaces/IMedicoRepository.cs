using MsMedico.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Interfaces
{
    public interface IMedicoRepository
    {
        Medico ObterMedicoPorCrmESenha(string crm, string senha);
        Medico ObterMedicoPorCrm(string crm);
        void AtualizarMedico(Medico medico);
        List<Medico> ObterMedicosComFiltros(string especialidade, double latitude, double longitude, double? distancia, double? avaliacao);
        void AdicionarRoleAoMedico(string crm, string role);
        List<string> ObterRolesDoMedico(string crm);
    }
}
