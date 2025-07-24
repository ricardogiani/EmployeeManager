using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Requests
{
    public class EmployeeFilterRequest
    {
        public Guid? Id { get; private set; }
        public string? DocumentNumber { get; set; }
    }
}