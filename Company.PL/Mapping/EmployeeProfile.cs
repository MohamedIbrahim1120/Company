using AutoMapper;
using Company.DAL.Models;
using Company.PL.Dtos;

namespace Company.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateDtoEmployee, Employee>()
                .ForMember(d => d.Name, o => o.MapFrom(s => $"{s.Name}"));

            CreateMap<Employee, CreateDtoEmployee>();
        }
    }
}
