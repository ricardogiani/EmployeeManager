using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManager.Adapter.Models;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Adapter.Mappers
{
    public class EmployeeDataMappingProfile: Profile
    {
        public EmployeeDataMappingProfile()
        {
            CreateMap<EmployeeEntity, EmployeeDataModel>();
            CreateMap<EmployeeDataModel, EmployeeEntity>();                
        }
    }
}