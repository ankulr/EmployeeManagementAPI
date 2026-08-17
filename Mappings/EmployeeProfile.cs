using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;

namespace EmployeeManagement.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<UpdateEmployeeDTO, Employee>();
            CreateMap<Employee, EmployeeResponseDTO>().ForMember(dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department.DepartmentName));
        }

    }
}
