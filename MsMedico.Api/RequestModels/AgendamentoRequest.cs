﻿using MsMedico.Core.Models;

namespace MsMedico.Api.RequestModels
{
    public class AgendamentoRequest
    {
        public string CRM { get; set; }
        public Consulta Consulta { get; set; }
    }
}
