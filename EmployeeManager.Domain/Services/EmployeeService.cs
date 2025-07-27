using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Exceptions;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Interfaces.Repositories;
using EmployeeManager.Domain.Interfaces.Services;
using EmployeeManager.Domain.Requests;

namespace EmployeeManager.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private (int Id, string userName) loggedInEmployee;

        private readonly IEmployeeRepository _employeeRepository;
        //private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task CreatePassword(EmployeeEntity newEmployee, string plainPassword)
        {
            newEmployee.GeneratePassword(plainPassword);

            await _employeeRepository.Save(newEmployee);
        }

        public Task<EmployeeEntity> ChangePassword(string plainPassword, string newPlainPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeEntity> Create(EmployeeEntity newEmployee, string plainPassword)
        {
            await BusinessValidate(newEmployee);

            if (!string.IsNullOrEmpty(plainPassword))
                newEmployee.GeneratePassword(plainPassword);

            var created = await _employeeRepository.Save(newEmployee);

            return created;
        }

        public async Task<bool> Delete(int id)
        {
            await _employeeRepository.Delete(id);

            return true;
        }

        public async Task<IEnumerable<EmployeeEntity>> Get(EmployeeFilterRequest filter)
        {
            var resultList = await _employeeRepository.Query(filter);

            return resultList;
        }

        public async Task<EmployeeEntity> GetById(int id)
        {
            var result = await _employeeRepository.Load(id);

            return result;
        }

        public async Task<EmployeeEntity> Update(int id, EmployeeEntity employee)
        {
            var employeeToUpdate = await _employeeRepository.Load(id);
            if (employeeToUpdate == null)
                throw new NotFoundException($"Employee id {id} not found");

            await BusinessValidate(employee);

            employee.UpdatedAt = DateTime.Now;
            employee.CreatedAt = employeeToUpdate.CreatedAt;
            
            var result = await _employeeRepository.Save(employee);

            return result;
        }

        private async Task BusinessValidate(EmployeeEntity employee)
        {
            bool isNewEmployee = employee.Id == 0;

            var validBirthDate = employee.BusinessValidateBirthDate();                        
            var ValidJobLevel = await BusinessValidateJobLevel(employee);
            var validUniqueDocument = isNewEmployee ? await BusinessValidateUniqueDocument(employee) : true;

            if (validBirthDate && validUniqueDocument && ValidJobLevel) return;

            var errors = new List<string> {};

            if (!validBirthDate)
                errors.Add(BusinessRuleValidationMessages.MessageBirthDate);

            if (!validUniqueDocument)
                errors.Add(BusinessRuleValidationMessages.MessageUniqueDocument);

            if (!ValidJobLevel)
                errors.Add(BusinessRuleValidationMessages.MessageHigherLevel);

            throw new BusinessRuleValidationException(errors);
        }
        
        private async Task<bool> BusinessValidateUniqueDocument(EmployeeEntity newEmployee)
        {
            var find = await _employeeRepository.LoadByDocumentNumber(newEmployee.DocumentNumber);
            return find == null;
        }

        private async Task<bool> BusinessValidateJobLevel(EmployeeEntity newEmployee)
        {
            var loggedEmployee = await _employeeRepository.Load(loggedInEmployee.Id);

             return (int)loggedEmployee.JobLevel >= (int)newEmployee.JobLevel;
        }

        public void SetLoggedInEmployee(int id, string userName)
        {
            loggedInEmployee = (id, userName);
        }
    }
}