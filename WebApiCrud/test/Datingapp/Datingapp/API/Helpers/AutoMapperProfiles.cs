using AutoMapper;
using Datingapp.API.DTO;
using Datingapp.API.Extensions;
using Datingapp.API.Models;

namespace Datingapp.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>().ForMember(dest => dest.Url, opt => opt.MapFrom(src =>
            src.Photos.FirstOrDefault(x => x.IsMain).Url)).ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotoDto>();
        }
    }
}
