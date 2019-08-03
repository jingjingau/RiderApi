using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RiderApi.Models
{
    public class RiderContext : DbContext
    {
        public RiderContext(DbContextOptions<RiderContext> options)
            : base(options)
        {
        }

        public DbSet<Rider> Riders { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rider>().ToTable("Riders"); 
            modelBuilder.Entity<Job>().ToTable("Jobs");

            base.OnModelCreating(modelBuilder);
        }
    }
}
