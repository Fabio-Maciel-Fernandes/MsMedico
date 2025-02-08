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
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMedicoService _medicoService;
        private readonly IConfiguration _configuration;

        public LoginController(IMedicoService medicoService, IConfiguration configuration)
        {
            _medicoService = medicoService;
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult Login([FromBody] MedicoLoginRequest request)
        {
            var medico = _medicoService.Login(request.CRM, request.Senha);
            if (medico == null)
                return Unauthorized("CRM ou senha inválidos");

            var token = GenerateJwtToken(medico);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Medico medico)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, medico.CRM),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, medico.Role)
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
    }   
}
