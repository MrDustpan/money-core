using Microsoft.EntityFrameworkCore;
using Money.Accounts.DomainModel;

namespace Money.Infrastructure
{
  public class ApplicationDbContext : DbContext
  {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {
      }

      public DbSet<Account> Accounts { get; set; }
  }
}