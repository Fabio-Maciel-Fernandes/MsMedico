namespace MsMedico.Api.RequestModels
{
    public class PacienteLoginRequest
    {
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }
    }
}
