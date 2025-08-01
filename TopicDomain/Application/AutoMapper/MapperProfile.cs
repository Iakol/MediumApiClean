using AutoMapper;
using TopicDomain.Application.DTO;
using TopicDomain.Domain;
using TopicDomain.Infrastructure.Database.Models;

namespace TopicDomain.Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Topic, TopicDTO>().
                ForMember(dest => dest.Name, opt => opt.MapFrom(scr => scr.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id));

            CreateMap<Topic, TopicModel>()
                .PreserveReferences()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(scr => scr.Name))
                .ForMember(dest => dest.Sourse, opt => opt.MapFrom(scr => scr.Sourse))
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(scr => scr.Parent != null? scr.Parent.Id : null))
                .ForMember(dest => dest.SubTopic, opt => opt.MapFrom(scr => scr.SubTopic))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(scr => scr.CreatedAt));

            CreateMap<TopicModel, Topic>()
                .PreserveReferences()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(scr => scr.Name))
                .ForMember(dest => dest.Sourse, opt => opt.MapFrom(scr => scr.Sourse))
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(scr => scr.Parent != null? scr.Parent : null))
                .ForMember(dest => dest.SubTopic, opt => opt.MapFrom(scr => scr.SubTopic))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(scr => scr.CreatedAt));

        } 
    } 
}