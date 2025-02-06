using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Core.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public bool Aceita { get; set; }
    }
}
