using System.ComponentModel.DataAnnotations;

namespace MsMedico.Api.RequestModels
{
    public class CancelamentoRequest
    {
        public string CRM { get; set; }
        public int ConsultaId { get; set; }
        [Required(ErrorMessage = "Justificativa é obrigatório.")]
        [Range(minimum: 6, maximum: 2000)]
        public string Justificativa { get; set; }
    }
}
