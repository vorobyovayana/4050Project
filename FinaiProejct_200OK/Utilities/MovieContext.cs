using FinaiProejct_200OK.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Utilities
{
    class MovieContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Director> Director { get; set; }
        public DbSet<Detail> Detail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FinalProject_200DB");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>()
                .HasKey(c => new { c.MemberId, c.MovieId });

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.MovieDirector)
                .WithMany(d => d.Movies);
        }
    }
}
