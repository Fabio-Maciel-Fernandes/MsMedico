using Microsoft.AspNetCore.Mvc;
using MsMedico.Api.RequestModels;
using MsMedico.Core.Interfaces;
using MsMedico.Core.Models;

namespace MsMedico.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoController(IAvaliacaoService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        [HttpPost]
        public ActionResult AdicionarAvaliacao([FromBody] AvaliacaoRequest request)
        {
            var avaliacao = new Avaliacao
            {
                PacienteId = request.PacienteId,
                Pontuacao = request.Pontuacao,
                Feedback = request.Feedback,
                Data = DateTime.Now
            };

            _avaliacaoService.AdicionarAvaliacao(request.CRM, avaliacao);
            return Ok("Avaliação adicionada com sucesso.");
        }

        [HttpGet("{crm}")]
        public ActionResult<double> ObterMediaAvaliacao(string crm)
        {
            var media = _avaliacaoService.ObterMediaAvaliacao(crm);
            return Ok(media);
        }
    }
}
