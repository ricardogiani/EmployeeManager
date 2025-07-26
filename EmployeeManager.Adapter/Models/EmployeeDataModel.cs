using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using EmployeeManager.Domain.Enums;

namespace EmployeeManager.Adapter.Models
{
    [Table("employees")] // Assuming your table is named 'employees' in snake_case
    public class EmployeeDataModel
    {
        //[Key] // This indicates 'id' is the primary key for Dapper.Contrib
        //public Guid id { get; set; }

        [Key]
        public Int64? id { get; set; } // Mapped to the database column
        /*public Guid Id 
        { 
            get { return new Guid(IdString); } 
            set { IdString = value.ToString(); } 
        }*/

        public bool active { get; set; } // Already snake_case

        public string first_name { get; set; } // Already snake_case

        public string last_name { get; set; } // Converted from LastName

        public string email { get; set; } // Converted from Email

        public string document_number { get; set; } // Converted from DocumentNumber

        public string phone_number { get; set; } // Converted from PhoneNumber

        public DateTime birth_date { get; set; } // Converted from BirthDate

        public JobLevelEnum job_level { get; set; } // Converted from JobLevel (assuming JobLevelEnum is defined elsewhere)

        public Int64? manager_id { get; set; } // Converted from ManagerId

        public string password_hash { get; set; } // Converted from PasswordHash

        public string password_salt { get; set; } // Converted from PasswordSalt

        public DateTime? created_at { get; set; } // Converted from CreatedAt

        public DateTime? updated_at { get; set; } // Converted from UpdatedAt
    }
}