using ABC.Learning.Resource.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.Learning.Resource.Persistence
{
    public class ABCLearningResourceContext : DbContext
    {
        public ABCLearningResourceContext(DbContextOptions<ABCLearningResourceContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<BookStock> BookStocks { get; set; }
        public DbSet<BookTransaction> BookTransactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure entity properties and relationships here if needed
            modelBuilder.Entity<Book>().HasKey(b => b.BookId);
            modelBuilder.Entity<BookPrice>().HasKey(bp => new { bp.BookId});
            modelBuilder.Entity<BookStock>().HasKey(bs => new { bs.BookId});
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<UserAccount>().HasKey(ua => ua.UserId);
        }
}
