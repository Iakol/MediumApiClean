using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class CreateReadingListCase : ICreateReadingListCase
    {
        private readonly ILogger<CreateReadingListCase> _logger;
        private readonly ICreateReadingListUnit _createReadingListUnit;

        public CreateReadingListCase(ILogger<CreateReadingListCase> logger, ICreateReadingListUnit createReadingListUnit)
        {
            _logger = logger;
            _createReadingListUnit = createReadingListUnit;
        }

        public async Task<Result> Handle(CreatePropsReadingListDTO newReadingList, string userId)
        {
            if (string.IsNullOrWhiteSpace(newReadingList.Name)) 
            {
                _logger.LogError("Reading List Name is empty", "Error When Create Reading List");
                return Result.Failure("Reading List Name is empty");
            }

            if (string.IsNullOrWhiteSpace(userId)) 
            {
                _logger.LogError("User id is empty", "Error When Create Reading List");
                return Result.Failure("User id is empty");
            }

            try
            {
                ReadingList list = new ReadingList(newReadingList, userId);
                await _createReadingListUnit.CreateReadingList(list);
                return Result.Success();

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error When Create Reading List");
                return Result.Failure(ex.Message);

            }

        }
    }
}
