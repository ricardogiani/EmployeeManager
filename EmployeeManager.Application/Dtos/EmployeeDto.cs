using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Enums;

namespace EmployeeManager.Application.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string DocumentNumber { get; private set; }

        public int Age { get; private set; }

        public JobLevelEnum JobLevel { get; private set; }

        public List<string> PhoneNumbers { get; private set; } = new();

        public Guid? ManagerId { get; private set; }

        public DateTime? CreatedAt { get; private set; }
        
        public DateTime? UpdatedAt { get; private set; }
    }
}