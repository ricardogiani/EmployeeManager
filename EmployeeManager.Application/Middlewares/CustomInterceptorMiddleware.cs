using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeManager.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManager.Application.Middlewares
{
    public class CustomInterceptorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomInterceptorMiddleware> _logger;
        private IEmployeeService _employeeService;

        public CustomInterceptorMiddleware(RequestDelegate next,
            ILogger<CustomInterceptorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            // 1. Verificar se o usuário está autenticado
            if (context.User.Identity.IsAuthenticated)
            {
                // 2. Acessar as claims do usuário
                ClaimsPrincipal currentUser = context.User;

                string customId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                string userName = currentUser.FindFirst(ClaimTypes.Name)?.Value;

                // Resolva o serviço Scoped diretamente do serviceProvider da requisição
                var employeeService = serviceProvider.GetService<IEmployeeService>();
                employeeService?.SetLoggedInEmployee(int.Parse(customId), userName);

                _logger.LogInformation($"[CustomInterceptor] Requisição de usuário autenticado. ID: {customId}, UserName: {userName}");
            }
            else
            {
                _logger.LogWarning("[CustomInterceptor] Requisição de usuário NÃO autenticado.");
            }

            // Continua para o próximo middleware no pipeline (e eventualmente para o controller)
            await _next(context);
        }
    }
}