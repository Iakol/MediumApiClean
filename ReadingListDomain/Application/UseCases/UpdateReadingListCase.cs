using Microsoft.Extensions.Logging;
using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class UpdateReadingListCase : IUpdateReadingListCase
    {
        private ILogger<IUpdateReadingListCase> _logger;
        private IUpdateReadingListUnit _updateReadingListUnit;
        private readonly IReadingListRepository _readingListRepository;

        public UpdateReadingListCase(ILogger<IUpdateReadingListCase> logger, IUpdateReadingListUnit updateReadingListUnit, IReadingListRepository readingListRepository)
        {
            _logger = logger;
            _updateReadingListUnit = updateReadingListUnit;
            _readingListRepository = readingListRepository;
        }

        public async Task<Result> Handle(CreatePropsReadingListDTO UpdateCredReadingList,string userId)
        {
            if (string.IsNullOrEmpty(UpdateCredReadingList.Id))
            {
                _logger.LogError("Reading List id is empty", "Error when update reading list");
                return Result.Failure("Reading List id is empty");
            }
            if (string.IsNullOrEmpty(UpdateCredReadingList.Name))
            {
                _logger.LogError("Reading List name is empty", "Error when update reading list");
                return Result.Failure("Reading List name is empty");
            }
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("user id is empty", "Error when update reading list");
                return Result.Failure("user id is empty");
            }

            try
            {
                ReadingList readinglist = await _readingListRepository.GetEntityAsync(UpdateCredReadingList.Id);
                if (!readinglist.ReadingListCreator.Equals(userId)) 
                {
                    _logger.LogError("user is not owner of reading list", "Error when update reading list");
                    return Result.Failure("user is not owner of reading list");
                }
                if (readinglist != null) 
                {
                    readinglist.Name = UpdateCredReadingList.Name;
                    readinglist.Description = UpdateCredReadingList.Description;
                    readinglist.IsPrivate = UpdateCredReadingList.IsPrivate;
                }

                await _updateReadingListUnit.UpdateReadingList(readinglist);
                return Result.Success();

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error when update reading list");
                return Result.Failure(ex.Message);

            }
        }
    }
}
