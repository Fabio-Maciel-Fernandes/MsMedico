using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Models
{
    public class Paciente
    {
        [Required(ErrorMessage = "Nome obrigatório.")]
        [MaxLength(255)]
        public string Nome { get; set; }
        public string CPF { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public List<Consulta> ConsultasAgendadas { get; set; }

        public Paciente()
        {
            ConsultasAgendadas = new List<Consulta>();
        }
    }
}
