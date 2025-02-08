using System.ComponentModel.DataAnnotations;

namespace MsMedico.Api.RequestModels
{
    public class FiltroRequest
    {
        public string Especialidade { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        public double? Distancia { get; set; }
        [Range(minimum: 0, maximum: 5)]
        public double? Avaliacao { get; set; }
    }
}
