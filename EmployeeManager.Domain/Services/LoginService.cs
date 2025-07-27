using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces.Repositories;
using EmployeeManager.Domain.Interfaces.Services;
using EmployeeManager.Domain.Requests;

namespace EmployeeManager.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IEmployeeRepository _employeeRepository;

        // TODO colocar ILogger<LoginService>
        public LoginService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<bool> ChangePassword(string plainPassword, string newPlainPassword)
        {
            throw new NotImplementedException();
        }

        public Task CreatePassword(EmployeeEntity newEmployee, string plainPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, int)> Login(string userName, string password)
        {
            var employee = (await _employeeRepository.Query(new EmployeeFilterRequest() { Email = userName }))
                .FirstOrDefault();

            if (employee?.AuthenticatePassword(password) == true)
                return (true, employee.Id);
                
            return (false, 0);
        }
    }
}