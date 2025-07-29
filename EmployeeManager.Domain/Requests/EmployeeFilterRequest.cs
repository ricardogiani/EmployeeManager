using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Enums;

namespace EmployeeManager.Domain.Requests
{
    public class EmployeeFilterRequest
    {
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public JobLevelEnum? JobLevel { get; set; }

        public Boolean JobLevelUp { get; set; } = false;       

        public bool? Active { get; set; }
    }
}