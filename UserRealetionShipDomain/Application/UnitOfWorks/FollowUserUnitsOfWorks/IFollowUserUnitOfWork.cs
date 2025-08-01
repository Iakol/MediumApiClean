namespace UserRealetionShipDomain.Application.UnitOfWorks.FollowUserUnitsOfWorks
{
    public interface IFollowUserUnitOfWork
    {
        public Task Followuser(string UserId, string UserToFollow);

    }
}
