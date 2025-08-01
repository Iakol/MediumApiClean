namespace UserRealetionShipDomain.Application.UnitOfWorks.UserMuteUnitsOfWorks
{
    public interface IUnMuteUserUnitOfWork
    {
        public Task UnMuteUser(string UserId, string MuteId);
    }
}
