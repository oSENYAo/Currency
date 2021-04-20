using Currency.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Currency.Data
{
    // Подкючение SQLite здесь а не в appsettings.json, т.к. я пару дней возился с этой СУБД (я с ней не знаком) и только этот вариант заработал.
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}
        public DbSet<EntityCurrency> EntityCurrencies{ get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.Sqliteproject.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
