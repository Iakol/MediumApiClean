namespace UserDomain.Presentation.UseCases
{
    public interface IUpdateUserLogoCase
    {
        public Task<string> Handle(); // return new url
    }
}
