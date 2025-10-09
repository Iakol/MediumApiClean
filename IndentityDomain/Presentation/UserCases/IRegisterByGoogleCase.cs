namespace IndentityDomain.Presentation.UserCases
{
    public interface IRegisterByGoogleCase
    {
        public Task<Result> Handle(string idToken);
    }
}
