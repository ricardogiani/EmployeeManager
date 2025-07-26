using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Enums;

namespace EmployeeManager.Application.Dtos
{
    public class EmployeeDto
    {
        public int? Id { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public JobLevelEnum JobLevel { get; set; }

        public string PhoneNumber { get; set; }

        public Guid? ManagerId { get; set; }

        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}