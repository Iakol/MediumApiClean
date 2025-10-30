using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UserDomain.Application.Interfaces.Repository;
using UserDomain.Application.Interfaces.UnitOfWork;
using UserDomain.Domain;
using UserDomain.Infrastructure.Database.DBContext;
using UserDomain.Infrastructure.Database.Models;

namespace UserDomain.Infrastructure.UnitsOfWork
{
    public class CreateRegisterUserUnit : ICreateRegisterUserUnit
    {
        private readonly AppDbContext _db;
        private readonly IUserProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserWrapperRepository _userWrapperRepository;
        private readonly IUserMemberShipRepository _userMemberShipRepository;

        public CreateRegisterUserUnit(AppDbContext db, UserManager<UserModel> userManager, IUserProfileRepository profileRepository, IUserRepository userRepository, IUserWrapperRepository userWrapperRepository, IUserMemberShipRepository userMemberShipRepository)
        {
            _db = db;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _userWrapperRepository = userWrapperRepository;
            _userMemberShipRepository = userMemberShipRepository;
        }

        public async Task RegisterUser(string email, string UserId)
        {
            await using var tx = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                User userToregister = new User() { Email = email, UserWrapperId = UserId, Id = UserId };
                await _userRepository.AddAsync(userToregister);
                await _profileRepository.AddAsync(new UserProfile(UserId, string.Empty, string.Empty));
                await _userWrapperRepository.AddAsync(new UserWrapper(UserId, UserId));
                await _userMemberShipRepository.AddAsync(UserMemberShip.Register(UserId));
                await _db.SaveChangesAsync();
                tx.Commit();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw;
            }
        }
    }
}
