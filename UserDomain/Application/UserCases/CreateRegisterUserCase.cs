using Microsoft.AspNetCore.Identity;
using UserDomain.Application.DTO;
using UserDomain.Application.Interfaces.UnitOfWork;
using UserDomain.Infrastructure.Database.Models;
using UserDomain.Presentation.UseCases;

namespace UserDomain.Application.UserCases
{
    public class CreateRegisterUserCase : ICreateRegisterUserCase
    {
        public readonly UserManager<UserModel> _userManager;
        public readonly ICreateRegisterUserUnit _createRegisterUserUnit;

        public CreateRegisterUserCase(UserManager<UserModel> userManager, ICreateRegisterUserUnit createRegisterUserUnit)
        {
            _userManager = userManager;
            _createRegisterUserUnit = createRegisterUserUnit;
        }

        public async Task<Result> Handle(string Email, string UserId)
        {
            try
            {
                if (_userManager.FindByIdAsync(UserId) == null)
                {
                    await _createRegisterUserUnit.RegisterUser(Email, UserId);
                    return Result.Success();
                }
                return Result.Failure("User Already Registered");
            }
            catch (Exception e) 
            {

                return Result.Failure(e.Message);
            }
        }
    }
}
