using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class GetListReadingListByCreatorIdCase : IGetListReadingListByCreatorIdCase
    {
        private readonly ILogger<GetListReadingListByCreatorIdCase> _logger;
        private readonly IReadingListRepository _readingListRepository;

        public GetListReadingListByCreatorIdCase(ILogger<GetListReadingListByCreatorIdCase> logger, IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _readingListRepository = readingListRepository;
        }

        public async Task<Result<List<ReadingList>>> Handle(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) 
            {
                _logger.LogError("User id is empty","Error whe get Reading list by user");
                return Result<List<ReadingList>>.Failure("User id is empty");
            }

            try 
            {
                return Result<List<ReadingList>>.Success( await _readingListRepository.GetListOfReadingList(userId));
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error whe get Reading list by user");
                return Result<List<ReadingList>>.Failure(ex.Message);
            }

        }
    }
}
