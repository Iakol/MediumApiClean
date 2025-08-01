using AutoMapper;
using UserDomain.Application.DTO;
using UserDomain.Domain;
using UserDomain.Infrastructure.Database.Models;

namespace UserDomain.Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<UserWrapper, UserWrapperModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(scr => scr.UserId))
                 .ForMember(dest => dest.Tag, opt => opt.MapFrom(scr => scr.Tag)).ReverseMap();

            CreateMap<UserProfile, UserCredDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(scr => scr.UserWrapperId))
                 .ForMember(dest => dest.AboutUser, opt => opt.MapFrom(scr => scr.About));



        }
    }
}
