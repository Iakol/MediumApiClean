namespace UserRealetionShipDomain.Application.UnitOfWorks.UserBlockUnitsOfWorks
{
    public interface IUnBlockUserUnitOfWork
    {
        public Task UnBlockuser(string UserId, string UserToBlock);

    }
}
