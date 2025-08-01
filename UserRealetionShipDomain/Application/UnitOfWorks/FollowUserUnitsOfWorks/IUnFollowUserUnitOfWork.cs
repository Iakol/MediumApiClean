namespace UserRealetionShipDomain.Application.UnitOfWorks.FollowUserUnitsOfWorks
{
    public interface IUnFollowUserUnitOfWork
    {
        public Task UnFollowuser(string UserId, string UserToFollow);
    }
}
