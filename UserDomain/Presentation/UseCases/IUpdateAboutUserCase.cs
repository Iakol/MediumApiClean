namespace UserDomain.Presentation.UseCases
{
    public interface IUpdateAboutUserCase
    {
        public Task Handle(string UserId, string newAboutUser);
    }
}
