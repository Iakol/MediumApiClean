using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class UpdateReadingListPrivateCase : IUpdateReadingListPrivateCase
    {
        private ILogger<UpdateReadingListPrivateCase> _logger;
        private IUpdateReadingListPrivateUnit _updateReadingListPrivateUnit;
        private readonly IReadingListRepository _readingListRepository;

        public UpdateReadingListPrivateCase(ILogger<UpdateReadingListPrivateCase> logger, IUpdateReadingListPrivateUnit updateReadingListPrivateUnit, IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _updateReadingListPrivateUnit = updateReadingListPrivateUnit;
            _readingListRepository = readingListRepository;
        }

        public async Task<Result> Handle(bool isPrivate, string userId, string ReadingListId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User id us empty", "Error when update reading list private");
                return Result.Failure("User id is empty");
            }
            if (string.IsNullOrEmpty(ReadingListId))
            {
                _logger.LogError("ReadingList id us empty", "Error when update reading list private");
                return Result.Failure("ReadingList id is empty");
            }
            try
            {
                ReadingList readinglist = await _readingListRepository.GetEntityAsync(ReadingListId);
                if (readinglist != null)
                {
                    if (readinglist.ReadingListCreator.Equals(userId))
                    {
                        await _updateReadingListPrivateUnit.SetPrivate(isPrivate, ReadingListId);
                        return Result.Success();
                    }
                    else
                    {
                        _logger.LogError("ReadingList id us empty", "Error when update reading list private");
                        return Result.Failure("User is not owner of reading list");
                    }

                }
                else
                {
                    _logger.LogError("ReadingList is not exist", "Error when update reading list private");
                    return Result.Failure("ReadingList is not exist");
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error when update reading list private");
                return Result.Failure(ex.Message);
            }

        }
    }
}
