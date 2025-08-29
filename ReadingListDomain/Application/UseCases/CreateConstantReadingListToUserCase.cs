using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.Repositories;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class CreateConstantReadingListToUserCase : ICreateConstantReadingListToUserCase
    {
        private readonly ILogger<CreateConstantReadingListToUserCase> _logger;
        private readonly ICreateReadingListUnit _createReadingListUnit;
        private readonly IReadingListRepository _readingListRepository;

        public CreateConstantReadingListToUserCase(ILogger<CreateConstantReadingListToUserCase> logger, ICreateReadingListUnit createReadingListUnit, IReadingListRepository readingListRepository) 
        {
            _logger = logger;
            _createReadingListUnit = createReadingListUnit;
            _readingListRepository = readingListRepository;

        }

        public async Task<Result> Handle(string userId)
        {
            if (string.IsNullOrEmpty(userId)) 
            {
                _logger.LogError("User id is empty", "Error When Create ReadingList");
                return Result.Failure("User id is empty");
            }

            try
            {
                bool isAlreadyCreated = (await _readingListRepository.GetListOfReadingList(userId)).Any(r => r.Immortal);
                if (isAlreadyCreated) 
                {
                    _logger.LogError("ReadingList is already created", "Error When Create ReadingList");
                    return Result.Failure("ReadingList is already created");
                }
                ReadingList newList = new ReadingList(userId);

                await _createReadingListUnit.CreateReadingList(newList);
                return Result.Success();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error When Create ReadingList");
                return Result.Failure(ex.Message);
            }

        }
    }
}
