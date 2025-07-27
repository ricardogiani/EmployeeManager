using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task CreatePassword(EmployeeEntity newEmployee, string plainPassword);

        public Task<bool> ChangePassword(string plainPassword, string newPlainPassword);

        public Task<(bool, int)> Login(string userName, string password);
    }
}