using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsersWallets.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Wallet>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            //modelBuilder.Entity<Wallet>().HasData(
            //    new Wallet() { Id = Guid.NewGuid(), Type = "BitCoin", Amount = 2 },
            //    new Wallet() { Id = Guid.NewGuid(), Type = "Crypto", Amount = 3 },
            //    new Wallet() { Id = Guid.NewGuid(), Type = "FaceCoin", Amount = 5 }
            //);
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<UserWallets> UserWallets { get; set; }
    }
}
