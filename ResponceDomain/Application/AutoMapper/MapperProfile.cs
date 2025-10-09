using AutoMapper;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.Models;

namespace ResponceDomain.Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Responce, ResponceModel>();
            CreateMap<ClapsToResponceOfUsers, ClapsToResponceOfUsersModel>();


        }
    }
}
