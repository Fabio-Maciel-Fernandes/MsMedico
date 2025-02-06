using MsMedico.Core.Models;

namespace MsMedico.Api.RequestModels
{
    public class HorarioRequest
    {
        public string CRM { get; set; }
        public Horario Horario { get; set; }
    }
}
