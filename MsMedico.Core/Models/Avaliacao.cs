using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public string PacienteId { get; set; }
        public int Pontuacao { get; set; } // Pontuação de 1 a 5
        public string Feedback { get; set; }
        public DateTime Data { get; set; }
    }
}
