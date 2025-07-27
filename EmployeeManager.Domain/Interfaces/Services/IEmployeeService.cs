using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Requests;

namespace EmployeeManager.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {

        public Task<EmployeeEntity> Create(EmployeeEntity newEmployee, string plainPassword);

        public Task<EmployeeEntity> Update(int id, EmployeeEntity employeeEntity);

        public Task<IEnumerable<EmployeeEntity>> Get(EmployeeFilterRequest filter);

        public Task<EmployeeEntity> GetById(int id);

        public Task<bool> Delete(int id);

        public void SetLoggedInEmployee(int id, string userName);
    }
}