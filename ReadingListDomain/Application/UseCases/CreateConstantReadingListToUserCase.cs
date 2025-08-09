using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Application.UseCases
{
    public class CreateConstantReadingListToUserCase : ICreateConstantReadingListToUserCase
    {
        private readonly ILogger<CreateConstantReadingListToUserCase> _logger;
        private readonly ICreateReadingListUnit _createReadingListUnit;

        public CreateConstantReadingListToUserCase(ILogger<CreateConstantReadingListToUserCase> logger, ICreateReadingListUnit createReadingListUnit) 
        {
            _logger = logger;
            _createReadingListUnit = createReadingListUnit;
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
                ReadingList newList = new ReadingList(userId);

                await _createReadingListUnit.CreateReadingList(newList);
                return Result.Success();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, "Error When Create ReadingList");
                return Result.Failure(ex.Message);
            }
            throw new NotImplementedException();


        }
    }
}
