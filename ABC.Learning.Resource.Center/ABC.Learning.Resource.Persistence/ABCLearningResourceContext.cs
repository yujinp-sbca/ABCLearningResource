using ABC.Learning.Resource.Domain.Common;
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
        public DbSet<BookRequestTransaction> BookReservationTransactions { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {                    
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
