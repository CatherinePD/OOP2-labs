using System;
using System.Configuration;
using System.Data.Entity;
using Auction.DataAccess.Entities;
using Auction.DataAccess.Factory;

namespace Auction.DataAccess.UnitOfWork
{
    public class AuctionUnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Watchman"].ConnectionString;
        private readonly AuctionContext _context;
        private readonly DbContextTransaction _transaction = null;

        private IRepository<Bid> _bidRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Contact> _contactRepository;
        private IRepository<Lot> _lotRepository;
        private IRepository<Product> _productRepository;
        private IRepository<ProductContent> _productContentRepository;
        private IRepository<User> _userRepository;

        public AuctionUnitOfWork(bool useTransaction = false)
        {
            _context = new AuctionContext(_connectionString);

            if (useTransaction)
                _transaction = _context.Database.BeginTransaction();
        }

        public IRepository<Bid> BidRepository =>
            _bidRepository ?? (_bidRepository = new AuctionRepository<Bid>(_context));

        public IRepository<Category> CategoryRepository =>
            _categoryRepository ?? (_categoryRepository = new AuctionRepository<Category>(_context));

        public IRepository<Contact> ContactRepository =>
            _contactRepository ?? (_contactRepository = new AuctionRepository<Contact>(_context));

        public IRepository<Lot> LotRepository =>
            _lotRepository ?? (_lotRepository = new AuctionRepository<Lot>(_context));

        public IRepository<Product> ProductRepository =>
            _productRepository ?? (_productRepository = new AuctionRepository<Product>(_context));

        public IRepository<ProductContent> ProductContentRepository =>
            _productContentRepository ?? (_productContentRepository = new AuctionRepository<ProductContent>(_context));

        public IRepository<User> UserRepository =>
            _userRepository ?? (_userRepository = new AuctionRepository<User>(_context));

        public void Dispose()
        {
            _context.Dispose();
            _transaction?.Dispose();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
            }
            catch (Exception e)
            {
                _transaction?.Rollback();
                throw e;
            }
        }
    }
}
