using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            // Exemplo simples: usuário e senha fixos
            if (request.Username == "admin" && request.Password == "Vera123456*")
            {
                //var token = GenerateJwtToken(request.Username);
                var token = string.Empty;
                return Ok(new { token });
            }
            return Unauthorized("Usuário ou senha inválidos");
        }
    }
}