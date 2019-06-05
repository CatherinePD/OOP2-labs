namespace Auction.DataAccess.Factory
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}