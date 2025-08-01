using ApiGateway.Application.DTO;

namespace ApiGateway.Application.Interface
{
    public interface IUserService
    {
        public Task<UserCredUserDomainDTO> GetUserCredAsync(string id);
        public Task<List<UserCredUserDomainDTO>> GetUserCredAsync(List<string> id);
        public Task<UserHeaderDTO> GetHeaderCredAsync(string id);
        public Task<List<UserHeaderDTO>> GetHeaderCredAsync(List<string> id);


    }
}
