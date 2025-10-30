using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserDomain.Application.Interfaces.Repository;
using UserDomain.Domain;
using UserDomain.Infrastructure.Database.DBContext;
using UserDomain.Infrastructure.Database.Models;

namespace UserDomain.Infrastructure.Database.Repositories.UserRepositories
{
    public class UserRepository : CommonDbIteraction<UserModel, User, string>, IUserRepository
    {
        private readonly UserManager<UserModel> userManager;
        public UserRepository(AppDbContext _db, IMapper _mappper, UserManager<UserModel> userManager) : base(_db, _mappper)
        {
            this.userManager = userManager;
        }

        public override async Task  AddAsync(User entity)
        {
            await userManager.CreateAsync(new UserModel() { Email = entity.Email, Id = entity.Id, UserWrapperId = entity.UserWrapperId });
        }

    }
}
