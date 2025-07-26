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
            CreateMap<EmployeeEntity, EmployeeDataModel>()
                .ForMember(dest => dest.first_name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.last_name, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.document_number, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.job_level, opt => opt.MapFrom(src => src.JobLevel))
                .ForMember(dest => dest.birth_date, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.created_at, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updated_at, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.phone_number, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.manager_id, opt => opt.MapFrom(src => src.ManagerId))
                .ForMember(dest => dest.password_hash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.password_salt, opt => opt.MapFrom(src => src.PasswordSalt))
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id));
            //.ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<EmployeeDataModel, EmployeeEntity>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.document_number))
                .ForMember(dest => dest.JobLevel, opt => opt.MapFrom(src => src.job_level))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.birth_date))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.created_at))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.updated_at))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.phone_number))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.active))
                .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.manager_id))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.password_hash))
                .ForMember(dest => dest.PasswordSalt, opt => opt.MapFrom(src => src.password_salt))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id));
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.id)));
        }
    }
}