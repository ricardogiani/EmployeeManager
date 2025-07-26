using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManager.Application.Dtos;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Exceptions;
using EmployeeManager.Domain.Interfaces.Services;
using EmployeeManager.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Retorna todos os funcionários.        
        /// </summary>
        /// <returns>Uma lista de EmployeeDto.</returns>
        /// 
        [Authorize]
        [HttpGet] // Atributo para indicar que este é um endpoint HTTP GET
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDto>))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            _logger.LogInformation("Get All Employee");

            var employees = await _employeeService.Get(new EmployeeFilterRequest());

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            //var employees = new List<EmployeeDto>() { new EmployeeDto() };
            return Ok(employeesDto);
        }

        /// </summary>
        /// Retorna um funcionário
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            _logger.LogInformation($"Get {id}");
            var employee = await _employeeService.GetById(id);
            if (employee == null)
            {
              return NotFound($"Employee with ID {id} not found."); 
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return Ok(employeeDto);
        }

        /// <summary>
        /// Cria um novo funcionário.
        /// Exemplo de uso: POST /api/employees
        /// Corpo da requisição (JSON): { "name": "Novo Empregado", "email": "novo@example.com", "position": "Developer" }
        /// </summary>
        /// <param name="employeeDto">Os dados do novo funcionário.</param>
        /// <returns>O EmployeeDto criado com o ID atribuído e a URL para acessá-lo.</returns>
        [Authorize]
        [HttpPost] // Atributo para indicar que este é um endpoint HTTP POST
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeDto>> Post([FromBody] EmployeeDto employeeDto)
        {
            _logger.LogInformation($"[Post] DocumentNumber: {employeeDto?.DocumentNumber}");
            try
            {
                var employee = _mapper.Map<EmployeeEntity>(employeeDto);
                var createdEmployee = await _employeeService.Create(employee);

                return CreatedAtAction(nameof(Get), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> Put(int id, [FromBody] EmployeeDto employeeDto)
        {
            _logger.LogInformation($"[Put] DocumentNumber: {employeeDto?.DocumentNumber}");

            try
            {
                // Validação básica: verifica se o ID na rota corresponde ao ID no corpo da requisição (se houver).
                // Isso ajuda a evitar confusão e erros de cliente.
                if (employeeDto == null || employeeDto.Id != id)
                {
                    return BadRequest("ID do funcionário no corpo da requisição não corresponde ao ID da rota ou dados inválidos.");
                }

                var employee = _mapper.Map<EmployeeEntity>(employeeDto);
                var updatedEmployee = await _employeeService.Update(id, employee);                

                return Ok(new EmployeeDto());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }
        
        [Authorize]
        [HttpDelete("{id}")] // Atributo para indicar que este é um endpoint HTTP DELETE com um parâmetro de rota 'id'
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _employeeService.Delete(id);
            if (!deleted)
            {
                return NotFound($"Funcionário com ID {id} não encontrado para exclusão.");
            }

            // Retorna 204 No Content para indicar que a requisição foi bem-sucedida,
            // mas não há conteúdo para retornar no corpo da resposta (porque o recurso foi excluído).
            return NoContent();
        }
        
    }
}