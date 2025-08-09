using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Infrastructure.Database.UnitOfWorks;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class DeleteReadingListCase : IDeleteReadingListCase
    {

        private readonly ILogger<DeleteReadingListCase> _logger;
        private readonly IDeleteReadingListUnit _deleteReadingListUnit;

        public DeleteReadingListCase(ILogger<DeleteReadingListCase> logger, IDeleteReadingListUnit deleteReadingListUnit)
        {
            _logger = logger;
            _deleteReadingListUnit = deleteReadingListUnit;
        }


        public async Task<Result> Handle(string readlingListId)
        {
            if (string.IsNullOrWhiteSpace(readlingListId)) 
            {
                _logger.LogError("Reading List Id is empty", "Error whe delete ReadingList");
                return Result.Failure("Reading List Id is empty");
            }

            try
            {
                await _deleteReadingListUnit.DeleteReadingList(readlingListId);
                return Result.Success();

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error whe delete ReadingList");
                return Result.Failure(ex.Message);
            }
        }
    }
}
