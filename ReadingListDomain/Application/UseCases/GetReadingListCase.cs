using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Domain;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class GetReadingListCase : IGetReadingListCase
    {
        private readonly ILogger<GetReadingListCase> _logger;
        private readonly IReadingListRepository _readingListRepository;

        public GetReadingListCase(ILogger<GetReadingListCase> logger, IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _readingListRepository = readingListRepository;
        }
        public async Task<Result<ReadingList>> Handle(string readingListId)
        {
            if (string.IsNullOrWhiteSpace(readingListId))
            {
                _logger.LogError("Reading list id is empty", "Error whe get Reading list by Id");
                return Result<ReadingList>.Failure("Reading list id is empty");
            }


            try
            {
                ReadingList list = await _readingListRepository.GetEntityAsync(readingListId);

                if (list == null) 
                {
                    _logger.LogError("Reading list  not found", "Error whe get Reading list by Id");
                    return Result<ReadingList>.Failure("Reading list  not found");
                }
                return Result<ReadingList>.Success(list);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error whe get Reading list by  list by Id");
                return Result<ReadingList>.Failure(ex.Message);
            }
        }
    }
}
