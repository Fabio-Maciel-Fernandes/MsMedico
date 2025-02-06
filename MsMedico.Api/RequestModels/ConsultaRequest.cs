using MsMedico.Core.Models;

namespace MsMedico.Api.RequestModels
{
    public class ConsultaRequest
    {
        public string CRM { get; set; }
        public Consulta Consulta { get; set; }
    }
}
