using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MsMedico.Api.RequestModels;
using MsMedico.Core.Interfaces;
using MsMedico.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MsMedico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        private readonly IMedicoService _medicoService;
        private readonly IConfiguration _configuration;

        public PacienteController(IPacienteService pacienteService, IMedicoService medicoService, IConfiguration configuration)
        {
            _pacienteService = pacienteService;
            _medicoService = medicoService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] PacienteLoginRequest request)
        {
            var paciente = _pacienteService.Authenticate(request.Email, request.CPF, request.Senha);
            if (paciente == null)
            {
                return Unauthorized("Email, CPF ou senha inválidos");
            }

            var token = GenerateJwtToken(paciente);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Paciente paciente)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, paciente.CPF),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, paciente.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("medicos")]
        [Authorize(Roles = "Paciente")]
        public ActionResult<List<Medico>> GetMedicos([FromQuery] FiltroRequest filtros)
        {
            var medicosFiltrados = _medicoService.ObterMedicos(filtros.Especialidade, filtros.Latitude, filtros.Longitude, filtros.Distancia, filtros.Avaliacao);
            return Ok(medicosFiltrados);
        }

        [HttpGet("medico/{crm}/agenda")]
        [Authorize(Roles = "Paciente")]
        public ActionResult<AgendaMedico> GetAgendaMedico(string crm)
        {
            var agenda = _medicoService.ObterAgenda(crm);
            if (agenda == null)
            {
                return NotFound("Médico não encontrado");
            }
            return Ok(agenda);
        }

        [HttpPost("agendar-consulta")]
        [Authorize(Roles = "Paciente")]
        public ActionResult AgendarConsulta([FromBody] AgendamentoRequest request)
        {
            _pacienteService.AgendarConsulta(request.CRM, request.Consulta);
            return Ok("Consulta agendada com sucesso");
        }

        [HttpDelete("cancelar-consulta")]
        [Authorize(Roles = "Paciente")]
        public ActionResult CancelarConsulta([FromBody] CancelamentoRequest request)
        {
            _pacienteService.CancelarConsulta(request.CRM, request.ConsultaId, request.Justificativa);
            return Ok("Consulta cancelada com sucesso");
        }

        [HttpDelete("cancelar-consulta2")]
        [Authorize(Roles = "Paciente")]
        public ActionResult CancelarConsulta2([FromBody] CancelamentoRequest request)
        {
            _pacienteService.CancelarConsulta(request.CRM, request.ConsultaId, request.Justificativa);
            return Ok("Consulta cancelada com sucesso");
        }
    }
}
