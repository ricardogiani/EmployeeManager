using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManager.Application.Dtos;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Application.Mappers
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<EmployeeEntity, EmployeeDto>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.FirstName));

            CreateMap<EmployeeDto, EmployeeEntity>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) 
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}