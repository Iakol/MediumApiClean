using AutoMapper;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.Models;

namespace ResponceDomain.Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Responce, ResponceModel>()
                .ForMember(dest => dest.UserId,opt => opt.MapFrom(o => o.UserId))
                .ForMember(dest => dest.ReadId,opt => opt.MapFrom(o => o.ReadId))
                .ForMember(dest => dest.ResponceId,opt => opt.MapFrom(o => o.ResponceId))
                .ForMember(dest => dest.TextOfReply, opt => opt.MapFrom(o => o.TextOfReply))
                .ForMember(dest => dest.CreateAt,opt => opt.MapFrom(o => o.CreateAt))
                .ForMember(dest => dest.ReaderTypeEnum, opt => opt.MapFrom(o => o.ReaderTypeEnum))
                .ForMember(dest => dest.BaseResponseId, opt => opt.MapFrom(o => o.BaseResponseId))
                .ForMember(dest => dest.ChildResponses, opt => opt.MapFrom(o => o.ChildResponses))
                .ForMember(dest => dest.BaseResponce, opt => opt.Ignore()).ReverseMap();
            
            CreateMap<ClapsToResponceOfUsers, ClapsToResponceOfUsersModel>()
                .ForMember(dest => dest.ResponceId, opt => opt.MapFrom(o => o.ResponceId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(o => o.UserId))
                .ForMember(dest => dest.ClapsCount, opt => opt.MapFrom(o => o.ClapsCount))
                .ForMember(dest => dest.Responce, opt => opt.Ignore())
                .ReverseMap();


        }
    }
}
