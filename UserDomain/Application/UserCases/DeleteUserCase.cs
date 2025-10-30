using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserDomain.Application.DTO;
using UserDomain.Application.Interfaces.UnitOfWork;
using UserDomain.Infrastructure.Database.Models;
using UserDomain.Presentation.UseCases;

namespace UserDomain.Application.UserCases
{
    public class DeleteUserCase : IDeleteUserCase
    {
        private readonly IDeleteUserUnit _deleteUserUnit;
        private readonly UserManager<UserModel> _userManager;
        private readonly ILogger<DeleteUserCase> _logger;

        public DeleteUserCase(IDeleteUserUnit deleteUserUnit, UserManager<UserModel> userManager, ILogger<DeleteUserCase> logger) 
        {
            _deleteUserUnit = deleteUserUnit;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<Result> Handle(string UserId)
        {

            if (string.IsNullOrWhiteSpace(UserId))
            {
                _logger.LogError("User id is null");
                return Result.Failure("User id is null");
            }

            try
            {
                UserModel UserToDelete = await _userManager.FindByIdAsync(UserId);

                if (UserToDelete == null)
                {
                    await _userManager.DeleteAsync(UserToDelete);
                    _logger.LogError("User id is null");
                    return Result.Failure("User id is null");
                }

                await _deleteUserUnit.DeleteUser(UserToDelete);
                return Result.Success();

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return Result.Failure(ex.Message);
            }

        }
    }
}
