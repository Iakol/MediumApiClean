namespace UserRealetionShipDomain.Application.UnitOfWorks.UserBlockUnitsOfWorks
{
    public interface IBlockUserUnitOfWork
    {
        public Task Blockuser(string UserId, string UserToBlock);
    }
}
