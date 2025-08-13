using AutoMapper;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.Models;

namespace ReadingListDomain.Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ReadingList, ReadingListModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
                .ForMember(dest => dest.ReadingListCreator, opt => opt.MapFrom(scr => scr.ReadingListCreator))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(scr => scr.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(scr => scr.Description))
                .ForMember(dest => dest.Immortal, opt => opt.MapFrom(scr => scr.Immortal))
                .ForMember(dest => dest.IsPrivate, opt => opt.MapFrom(scr => scr.IsPrivate))
                .ForMember(dest => dest.IsOpenResponces, opt => opt.MapFrom(scr => scr.IsOpenResponces))
                .ForMember(dest => dest.SaveStories, opt => opt.MapFrom(scr => scr.SaveStories))
                .ReverseMap();

            CreateMap<StoryInReadingList, StoryInReadingListModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
                .ForMember(dest => dest.StoryId, opt => opt.MapFrom(scr => scr.StoryId))
                .ForMember(dest => dest.ReadingListId, opt => opt.MapFrom(scr => scr.ReadingListId))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(scr => scr.Note))
                .ForMember(dest => dest.ReadingList, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<StoryInReadingListModel, StoryInReadingList>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id))
                .ForMember(dest => dest.StoryId, opt => opt.MapFrom(scr => scr.StoryId))
                .ForMember(dest => dest.ReadingListId, opt => opt.MapFrom(scr => scr.ReadingListId))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(scr => scr.Note));

        }
    }
}
