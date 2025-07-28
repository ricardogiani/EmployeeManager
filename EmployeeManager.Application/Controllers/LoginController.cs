using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Application.Dtos;
using EmployeeManager.Application.Services;
using EmployeeManager.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthProviderService _authService;
        private readonly ILoginService _loginService;

        public LoginController(IAuthProviderService authService, ILoginService loginService)
        {
            _loginService = loginService;
            _authService = authService;            
        }

        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                (var authenticated, var customId) = await _loginService.Login(request.UserName, request.Password);

                if (authenticated)
                {
                    var token = _authService.GenerateJwtToken(request.UserName, customId.ToString());
                    return Ok(new { token });
                }
                return Unauthorized(new { message = "Usuário ou senha inválidos" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
                
        }
    }
}