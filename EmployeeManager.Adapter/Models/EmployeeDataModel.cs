using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using EmployeeManager.Domain.Enums;

namespace EmployeeManager.Adapter.Models
{
    [Table("employees")]
    public class EmployeeDataModel
    {
        [Key]
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string DocumentNumber { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public JobLevelEnum JobLevel { get; set; }

        public Guid? ManagerId { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}