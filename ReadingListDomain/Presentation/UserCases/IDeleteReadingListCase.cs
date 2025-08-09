using ReadingListDomain.Application.DTO;
using System.Reflection.Metadata;

namespace ReadingListDomain.Presentation.UserCases
{
    public interface IDeleteReadingListCase
    {
        public Task<Result> Handle(string readlingListId);
    }
}
