using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Models
{
    public class Consulta
    {
        public decimal Valor { get; set; }
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string PacienteCPF { get; set; }
        public DateTime Data { get; set; }
        public bool Aceita { get; set; }
    }
}
