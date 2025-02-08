using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Models
{
    public class Medico
    {
        [Required(ErrorMessage = "Nome obrigatório.")]
        [MaxLength(255)]
        public string Nome { get; set; }
        public string CRM { get; set; }
        public string Senha { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }
        public string Especialidade { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? DistanciaKm { get; set; }
        public double? Avaliacao { get; set; }
        public List<Horario> HorariosDisponiveis { get; set; } = new List<Horario>();
        public List<Consulta> ConsultasAgendadas { get; set; } = new List<Consulta>();
        public string Role { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
        public decimal ValorConsulta { get; set; }
    }
}
