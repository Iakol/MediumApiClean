namespace IndentityDomain.Infrastructure.RabbitMQ.Sages
{
    public enum CreateUserComandEnum
    {
        ReadingListCreationSucsess,
        ReadingListMessageDead,
        ReadingListCreationServiceFail,

        UserDataCreationSucsess,
        UserDataMessageDead,
        UserDataCreationServiceFail
    }
}
