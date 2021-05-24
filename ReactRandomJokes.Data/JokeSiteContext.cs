using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactRandomJokes.Data
{
    public class JokeSiteContext : DbContext
    {
        private string _connectionString;

        public JokeSiteContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
    
            modelBuilder.Entity<UserLikedJoke>()
                .HasKey(lj => new { lj.JokeId, lj.UserId });

            modelBuilder.Entity<UserLikedJoke>()
                .HasOne(lj => lj.Joke)
                .WithMany(j => j.Likes)
                .HasForeignKey(j => j.JokeId);

            modelBuilder.Entity<UserLikedJoke>()
                .HasOne(lj => lj.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(j => j.UserId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Joke> Jokes { get; set; }
        public DbSet<UserLikedJoke> UserLikedJokes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

