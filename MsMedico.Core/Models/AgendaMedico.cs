using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Models
{
    public class AgendaMedico
    {
        public string CRM { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public decimal ValorConsulta { get; set; }
        public List<Horario> HorariosDisponiveis { get; set; }
        public List<Consulta> ConsultasAgendadas { get; set; }

        public AgendaMedico(Medico medico)
        {
            CRM = medico.CRM;
            Nome = medico.Nome;
            Especialidade = medico.Especialidade;
            HorariosDisponiveis = medico.HorariosDisponiveis;
            ConsultasAgendadas = medico.ConsultasAgendadas;
            ValorConsulta = medico.ValorConsulta;
        }
    }
}
