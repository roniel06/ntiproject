using AutoMapper;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Employee;
using NTI.Domain.Models;

namespace NTI.Application.Mappings.Employees
{
    public class EmployeeMapping : Profile
    {
        public EmployeeMapping()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeInputModel>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash)).ReverseMap();
            CreateMap<EmployeeDto, EmployeeInputModel>().ReverseMap();
        }

    }
}