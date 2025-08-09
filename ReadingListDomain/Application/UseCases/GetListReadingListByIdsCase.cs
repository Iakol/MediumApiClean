using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Domain;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class GetListReadingListByIdsCase : IGetListReadingListByIdsCase
    {
        private readonly ILogger<GetListReadingListByIdsCase> _logger;
        private readonly IReadingListRepository _readingListRepository;

        public GetListReadingListByIdsCase(ILogger<GetListReadingListByIdsCase> logger, IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _readingListRepository = readingListRepository;
        }

        public async Task<Result<List<ReadingList>>> Handle(List<string> Ids)
        {
            if (!Ids.Any(i => string.IsNullOrWhiteSpace(i))) 
            {
                _logger.LogError("One or more id is empty", "Error whe get Reading list by List of Ids");
                return Result<List<ReadingList>>.Failure("One or more id is empty is empty");
            }


            try
            {
                return Result<List<ReadingList>>.Success(await _readingListRepository.GetListOfReadingList(Ids));

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error whe get Reading list by  List of Ids");
                return Result<List<ReadingList>>.Failure(ex.Message);
            }

        }
    }
}
