using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class UpdateReadingListVisibleOfResponceCase : IUpdateReadingListVisibleOfResponceCase
    {
        private ILogger<UpdateReadingListVisibleOfResponceCase> _logger;
        private IUpdateReadingListVisibleOfResponceUnit _updateReadingListVisibleOfResponceUnit;
        private readonly IReadingListRepository _readingListRepository;

        public UpdateReadingListVisibleOfResponceCase(ILogger<UpdateReadingListVisibleOfResponceCase> logger, IUpdateReadingListVisibleOfResponceUnit updateReadingListVisibleOfResponceUnit, IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _updateReadingListVisibleOfResponceUnit = updateReadingListVisibleOfResponceUnit;
            _readingListRepository = readingListRepository;
        }

        public async Task<Result> Handle(bool isVisible, string userId, string ReadingListId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User id us empty", "Error when update reading list responce visible");
                return Result.Failure("User id is empty");
            }
            if (string.IsNullOrEmpty(ReadingListId))
            {
                _logger.LogError("ReadingList id us empty", "Error when update reading list responce visible");
                return Result.Failure("ReadingList id is empty");
            }

            try
            {
                ReadingList readinglist = await _readingListRepository.GetEntityAsync(ReadingListId);

                if (readinglist != null)
                {
                    if (readinglist.ReadingListCreator.Equals(userId))
                    {
                        _updateReadingListVisibleOfResponceUnit.SetVisibleOfResponce(isVisible, ReadingListId);
                        return Result.Success();
                    }
                    else
                    {
                        _logger.LogError("User is not owner of ReadingList", "Error when update reading list responce visible");
                        return Result.Failure("User is not owner of ReadingList");
                    }

                }
                else 
                {
                    _logger.LogError("ReadingList is not exist", "Error when update reading list responce visible");
                    return Result.Failure("ReadingList is not exist");
                }

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error when update reading list responce visible");
                return Result.Failure(ex.Message);
            }

        }
    }
}
