using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.FluentMap.Mapping;
using EmployeeManager.Adapter.Models;

namespace EmployeeManager.Adapter.ModelMaps
{
    public class EmployeeModelMap : EntityMap<EmployeeDataModel>
    {
        public EmployeeModelMap()
        {
            /*Map(p => p.Id).ToColumn("id");
            Map(p => p.FirstName).ToColumn("first_name");
            Map(p => p.LastName).ToColumn("last_name");
            Map(p => p.Email).ToColumn("email");
            Map(p => p.DocumentNumber).ToColumn("document_number");
            Map(p => p.JobLevel).ToColumn("job_level");
            Map(p => p.BirthDate).ToColumn("birth_date");
            Map(p => p.CreatedAt).ToColumn("created_at");
            Map(p => p.UpdatedAt).ToColumn("updated_at");
            Map(p => p.PhoneNumber).ToColumn("phone_number");
            Map(p => p.Active).ToColumn("active");
            Map(p => p.ManagerId).ToColumn("manager_id");

            Map(p => p.PasswordHash).ToColumn("password_hash");
            Map(p => p.PasswordSalt).ToColumn("password_salt");*/
        }
    }
}