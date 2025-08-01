using ApiGateway.Application.DTO;

namespace ApiGateway.Application.Interface
{
    public interface IUserRealtionShipService
    {
        public Task<int> GetUserFollowersCountAsync(string UserId);
        public Task<List<string>> GetUserFollowersAsync(string UserId);
        public Task<List<string>> GetUserBlockedUserAsync(string UserId);
        public Task<List<string>> GetUserMutedUserAsync(string UserId);

        public Task<List<string>> GetSavedReadingListAsync(string UserId);


        public Task<httpMessage> MuteUserAsync(string UserId);
        public Task<httpMessage> UnMuteUserAsync(string UserId);
        public Task<httpMessage> BlockUserAsync(string UserId);
        public Task<httpMessage> UnBlockUserAsync(string UserId);
        public Task<httpMessage> FollowUserAsync(string UserId);
        public Task<httpMessage> UnFollowUserAsync(string UserId);
        public Task<httpMessage> SaveReadingListAsync(string readingListId);
        public Task<httpMessage> UnSaveReadingListAsync(string readingListId);



    }
}
