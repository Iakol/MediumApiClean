using Microsoft.AspNetCore.Identity;
using UserDomain.Application.Interfaces.Repository;
using UserDomain.Application.Interfaces.UnitOfWork;
using UserDomain.Infrastructure.Database.DBContext;
using UserDomain.Infrastructure.Database.Models;
using UserDomain.Infrastructure.Database.Repositories.UserRepositories;

namespace UserDomain.Infrastructure.UnitsOfWork
{
    public class DeleteUserUnit : IDeleteUserUnit
    {
        private readonly AppDbContext _db;
        private readonly IUserProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserWrapperRepository _userWrapperRepository;
        private readonly IUserMemberShipRepository _userMemberShipRepository;
        private readonly UserManager<UserModel> _userIndentityManager;

        public DeleteUserUnit(AppDbContext db, IUserProfileRepository profileRepository, IUserRepository userRepository, IUserWrapperRepository userWrapperRepository, IUserMemberShipRepository userMemberShipRepository, UserManager<UserModel> userIndentityManager)
        {
            _db = db;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _userWrapperRepository = userWrapperRepository;
            _userMemberShipRepository = userMemberShipRepository;
            _userIndentityManager = userIndentityManager;
        }

        public async Task DeleteUser(UserModel user)
        { 

            await _profileRepository.DeleteAsync(user.Id);
            await _userRepository.DeleteAsync(user.Id);
            await _userWrapperRepository.DeleteAsync(user.Id);
            await _userMemberShipRepository.DeleteAsync(user.Id);
            await _userIndentityManager.DeleteAsync(user);
            
            await _db.SaveChangesAsync();
            
            
        }
    }
}
