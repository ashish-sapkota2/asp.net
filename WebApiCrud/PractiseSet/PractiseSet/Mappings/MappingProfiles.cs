using AutoMapper;
using PractiseSet.Models;
using PractiseSet.Models.DTO;

namespace PractiseSet.Mappings
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, PostEmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();
        }
    }
}
