using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.UnitOfWorks;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class DeleteReadingListCase : IDeleteReadingListCase
    {

        private readonly ILogger<DeleteReadingListCase> _logger;
        private readonly IDeleteReadingListUnit _deleteReadingListUnit;
        private readonly IReadingListRepository _readingListRepository;

        public DeleteReadingListCase(ILogger<DeleteReadingListCase> logger, IDeleteReadingListUnit deleteReadingListUnit, IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _deleteReadingListUnit = deleteReadingListUnit;
            _readingListRepository = readingListRepository;
        }


        public async Task<Result> Handle(string readlingListId)
        {
            if (string.IsNullOrWhiteSpace(readlingListId)) 
            {
                _logger.LogError("Reading List Id is empty", "Error when delete ReadingList");
                return Result.Failure("Reading List Id is empty");
            }

            try
            {
                ReadingList readingList = await _readingListRepository.GetEntityAsync(readlingListId);
                if (readingList == null) 
                {
                    _logger.LogError("Reading List not exist", "Error when delete ReadingList");
                    return Result.Failure("Reading List not exist");
                }
                if (readingList.Immortal) 
                {
                    _logger.LogError("Reading List is Immortal", "Error when delete ReadingList");
                    return Result.Failure("Reading List is Immortal");
                }
                await _deleteReadingListUnit.DeleteReadingList(readlingListId);
                return Result.Success();

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error when delete ReadingList");
                return Result.Failure(ex.Message);
            }
        }
    }
}
