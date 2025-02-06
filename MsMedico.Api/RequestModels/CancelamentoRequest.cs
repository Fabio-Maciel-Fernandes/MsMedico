namespace MsMedico.Api.RequestModels
{
    public class CancelamentoRequest
    {
        public string CRM { get; set; }
        public int ConsultaId { get; set; }
        public string Justificativa { get; set; }
    }
}
