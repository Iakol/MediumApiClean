using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.Model;

namespace UserRealetionShipDomain.Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SaveReadingList, SaveReadingListModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(scr => scr.UserId))
                .ForMember(dest => dest.SaveReadingListId, opt => opt.MapFrom(scr => scr.SaveReadingListId))
                .ForMember(dest => dest.SaveAt, opt => opt.MapFrom(scr => scr.SaveAt)).ReverseMap();

            CreateMap<UserBlock, UserBlockModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(scr => scr.UserId))
                .ForMember(dest => dest.BlockId, opt => opt.MapFrom(scr => scr.BlockId))
                .ForMember(dest => dest.BlockAt, opt => opt.MapFrom(scr => scr.BlockAt)).ReverseMap();

            CreateMap<UserFollow, UserFollowModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(scr => scr.UserId))
                .ForMember(dest => dest.FollowId, opt => opt.MapFrom(scr => scr.FollowId))
                .ForMember(dest => dest.FollowAt, opt => opt.MapFrom(scr => scr.FollowAt)).ReverseMap();

            CreateMap<UserMute, UserMuteModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(scr => scr.UserId))
                .ForMember(dest => dest.MuteId, opt => opt.MapFrom(scr => scr.MuteId))
                .ForMember(dest => dest.MuteAt, opt => opt.MapFrom(scr => scr.MuteAt)).ReverseMap();

        }
    }
}
