using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Requests;

namespace EmployeeManager.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task<EmployeeEntity> ChangePassword(string plainPassword, string newPlainPassword)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeEntity> Create(EmployeeEntity employeeEntity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeEntity>> Get(EmployeeFilterRequest filter)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeEntity> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeEntity> Update(Guid id, EmployeeEntity employeeEntity)
        {
            throw new NotImplementedException();
        }
    }
}