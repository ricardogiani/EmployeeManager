using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Interfaces.Repositories;
using EmployeeManager.Domain.Interfaces.Services;
using EmployeeManager.Domain.Requests;

namespace EmployeeManager.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        //private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public Task<EmployeeEntity> ChangePassword(string plainPassword, string newPlainPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeEntity> Create(EmployeeEntity employeeEntity)
        {
            //employeeEntity
            var created = await _employeeRepository.Save(employeeEntity);

            return created;
        }

        public async Task<bool> Delete(Guid id)
        {
            await _employeeRepository.Delete(id);

            return true;
        }

        public async Task<IEnumerable<EmployeeEntity>> Get(EmployeeFilterRequest filter)
        {
            var resultList = await _employeeRepository.Query(filter);

            return resultList;
        }

        public async Task<EmployeeEntity> GetById(Guid id)
        {
            var result = await _employeeRepository.Load(id);

            return result;
        }

        public async Task<EmployeeEntity> Update(Guid id, EmployeeEntity employeeEntity)
        {
            var result = await _employeeRepository.Save(employeeEntity);

            return result;
        }
    }
}