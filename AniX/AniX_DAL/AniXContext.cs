using Anix_Shared.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniX_DAL
{
    public class AniXContext : DbContext
    {
        public AniXContext(DbContextOptions<AniXContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property<string>("_password").HasColumnName("Password");
            modelBuilder.Entity<User>().Property<string>("_salt").HasColumnName("Salt");
        }
    }
}
