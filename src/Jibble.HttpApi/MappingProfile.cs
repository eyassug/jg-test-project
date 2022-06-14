using AutoMapper;

namespace Jibble
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employees.Employee, Employees.EmployeeDto>().ReverseMap();
        }
    }
}
