namespace MsMedico.Api.RequestModels
{
    public class AvaliacaoRequest
    {
        public string PacienteId { get; set; }
        public string CRM { get; set; }
        public int Pontuacao { get; set; } // Pontuação de 1 a 5
        public string Feedback { get; set; }
    }
}
