using Auction.DataAccess.Entities;
using System.Data.Entity;

namespace Auction.DataAccess
{
    public class AuctionContext : DbContext
    {
        public AuctionContext(string connectionString)
            :base(connectionString)
        {
            
        }
        public AuctionContext()
            : this("name=Watchman")
        {
        }

        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<ProductContent> ProductContents { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Bid
            modelBuilder.Entity<Bid>()
                .Property(e => e.Amount)
                .HasPrecision(9, 3);
            #endregion

            #region Lot
            modelBuilder.Entity<Lot>()
                .Property(e => e.StartBid)
                .HasPrecision(9, 3);

            modelBuilder.Entity<Lot>()
                .Property(e => e.CurrentBid)
                .HasPrecision(9, 3);

            modelBuilder.Entity<Lot>()
                .Property(e => e.MinBidStep)
                .HasPrecision(9, 3);

            modelBuilder.Entity<Lot>()
                .HasMany(l => l.Bids)
                .WithRequired(b => b.Lot)
                .HasForeignKey(e => e.LotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lot>()
                .HasMany(l => l.Users)
                .WithMany(u => u.Lots)
                .Map(m =>
                {
                    m.ToTable("UserLots");
                    m.MapLeftKey("LotId");
                    m.MapRightKey("UserId");
                });
            #endregion

            #region Product
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductContents)
                .WithRequired(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .WillCascadeOnDelete(false);
            #endregion

            #region User
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bids)
                .WithRequired(b => b.User)
                .HasForeignKey(b => b.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Products)
                .WithRequired(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .WillCascadeOnDelete(false);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
