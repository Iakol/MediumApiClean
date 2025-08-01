using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.ReadingListCases;

namespace UserRealetionShipDomain.Application.UseCases.SaveReadingListCases
{
    public class GetUserSaveReadingListCase(ISaveReadingListRepository _saveReadingListRepository,ILogger<GetUserSaveReadingListCase> logger) : IGetUserSaveReadingListCase
    {

        public async Task<Result<List<SaveReadingList>>> Handle(string userId) 
        {
            if (string.IsNullOrWhiteSpace(userId)) 
            {
                logger.LogError("The user id is Empty", "Error When get the save list of user");
                return Result<List<SaveReadingList>>.Failure("The user id is Empty");
            }

            try
            {
                return Result<List<SaveReadingList>>.Success(await _saveReadingListRepository.GetEntityListByFollowIdAsync(userId)); 
                
            } catch (Exception ex) 
            {
                logger.LogError(ex.Message, "Error When get the save list of user");
                return Result<List<SaveReadingList>>.Failure(ex.Message);
            }
        }
    }
}
