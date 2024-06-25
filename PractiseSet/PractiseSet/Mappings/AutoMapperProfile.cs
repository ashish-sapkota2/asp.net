using AutoMapper;
using PractiseSet.Models;
using PractiseSet.Models.DTO;

namespace PractiseSet.Mappings
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
