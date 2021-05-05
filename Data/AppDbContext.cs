using Currency.AreaIdentity;
using Currency.Models;
using Currency.Models.Currency;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Currency.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<ValCurs> ValCurses { get; set; }
        public DbSet<Valute> Valutes{ get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
