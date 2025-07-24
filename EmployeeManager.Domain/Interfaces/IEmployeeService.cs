using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Requests;

namespace EmployeeManager.Domain.Interfaces
{
    public interface IEmployeeService
    {
        public Task<EmployeeEntity> Create(EmployeeEntity employeeEntity);

        public Task<EmployeeEntity> Update(Guid id, EmployeeEntity employeeEntity);

        public Task<IEnumerable<EmployeeEntity>> Get(EmployeeFilterRequest filter);

        public Task<EmployeeEntity> GetById(Guid id);

        public Task<EmployeeEntity> ChangePassword(string plainPassword, string newPlainPassword);

        public Task<bool> Delete(Guid id);
    }
}