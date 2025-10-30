namespace UserDomain.Application.Interfaces.UnitOfWork
{
    public interface ICreateRegisterUserUnit
    {
        public Task RegisterUser(string email, string UserId);
    }
}
