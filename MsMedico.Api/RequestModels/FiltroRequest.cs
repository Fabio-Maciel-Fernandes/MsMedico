namespace MsMedico.Api.RequestModels
{
    public class FiltroRequest
    {
        public string Especialidade { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Distancia { get; set; }
        public double? Avaliacao { get; set; }
    }
}
