using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Application.Dtos;
using EmployeeManager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;            
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (request.UserName == "admin" && request.Password == "123456")
            {
                var token = _authService.GenerateJwtToken(request.UserName);
                return Ok(new { token });
            }
            return Unauthorized("Usuário ou senha inválidos");
        }
    }
}