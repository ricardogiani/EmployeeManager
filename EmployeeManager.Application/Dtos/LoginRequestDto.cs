using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Dtos
{
    public class LoginRequestDto
    {    
        public string UserName { get; set; }
        public string Password { get; set; }    
    }
}