namespace UserRealetionShipDomain.Application.UnitOfWorks.UserMuteUnitsOfWorks
{
    public interface IMuteUserUnitOfWork
    {
        public Task MuteUser(string UserId, string MuteId);

    }
}
