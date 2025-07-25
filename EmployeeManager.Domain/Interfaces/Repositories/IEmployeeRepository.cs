using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Requests;

namespace EmployeeManager.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeEntity?> Load(Guid id);

        Task<EmployeeEntity> LoadByDocumentNumber(string documentNumber);

        Task<IEnumerable<EmployeeEntity>> Query(EmployeeFilterRequest filter);

        Task<EmployeeEntity> Save(EmployeeEntity documentModel);

        Task<bool> Delete(Guid id);
        
    }
}