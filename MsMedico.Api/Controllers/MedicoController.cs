using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MsMedico.Api.RequestModels;
using MsMedico.Core.Interfaces;
using MsMedico.Core.Models;

namespace MsMedico.Api.Controllers
{
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;
        private readonly IDistanciaService _distanciaService;

        public MedicoController(IMedicoService medicoService, IDistanciaService distanciaService)
        {
            _medicoService = medicoService;
            _distanciaService = distanciaService;
        }


        [HttpPost("cadastro")]
        [Authorize(Roles = "Medico")]
        public ActionResult Cadastro([FromBody] HorarioRequest request)
        {
            _medicoService.CadastrarHorario(request.CRM, request.Horario);
            return Ok("Horário cadastrado com sucesso");
        }

        [HttpPut("editar-horario")]
        [Authorize]
        public ActionResult EditarHorario([FromBody] HorarioRequest request)
        {
            _medicoService.EditarHorario(request.CRM, request.Horario);
            return Ok("Horário editado com sucesso");
        }

        [HttpPost("aceitar-consulta")]
        [Authorize]
        public ActionResult AceitarConsulta([FromBody] ConsultaRequest request)
        {
            _medicoService.AceitarConsulta(request.CRM, request.Consulta);
            return Ok("Consulta aceita");
        }

        [HttpPost("recusar-consulta")]
        [Authorize]
        public ActionResult RecusarConsulta([FromBody] ConsultaRequest request)
        {
            _medicoService.RecusarConsulta(request.CRM, request.Consulta);
            return Ok("Consulta recusada");
        }

        [HttpGet("medicos")]
        public ActionResult<List<Medico>> GetMedicos([FromQuery] FiltroRequest filtros)
        {
            var medicosFiltrados = _medicoService.ObterMedicos(filtros.Especialidade, filtros.Latitude, filtros.Longitude, filtros.Distancia, 
                filtros.Avaliacao);
            return Ok(medicosFiltrados);
        }

        [HttpPost("agendar-consulta")]
        public ActionResult AgendarConsulta([FromBody] AgendamentoRequest request)
        {
            _medicoService.AgendarConsulta(request.CRM, request.Consulta);
            return Ok("Consulta agendada com sucesso");
        }

        [HttpDelete("cancelar-consulta")]
        public ActionResult CancelarConsulta([FromBody] CancelamentoRequest request)
        {
            _medicoService.CancelarConsulta(request.CRM, request.ConsultaId, request.Justificativa);
            return Ok("Consulta cancelada com sucesso");
        }

        [HttpGet("cancular-distancia")]
        public ActionResult CalcularDistancia()
        {
            //var distancia = _distanciaService.ObterDistanciaHaversine(-27.456840, -48.408810, -27.600470793922568, -48.54948838054793);

            var distancia = _distanciaService.ObterDistanciaHaversine(-27.456840, -48.408810, -27.43660580083045, -48.39629879463427);

           
            return Ok(distancia);
        }
    }
}
