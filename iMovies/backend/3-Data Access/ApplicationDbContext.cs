using Microsoft.EntityFrameworkCore;
using OMDbProject.Models.DTOs;


namespace OMDbProject.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Follower> Followers { get; set; }
        
        // If using a view for movie ranking
        public DbSet<MovieRankDTO> MovieRanks { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations for User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserName).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Salt).IsRequired(); //Make Salt required
            });

            // Configurations for UserMovie entity
            modelBuilder.Entity<UserMovie>(entity =>
            {
                entity.HasKey(e => e.UserMovieId);

                // Specify precision and scale for UserRating
                entity.Property(e => e.UserRating)
               .HasColumnType("decimal(2, 1)"); // Precision 2, Scale 1

                //Foreign key configuration
                entity.HasOne(e => e.User)
                      .WithMany(u => u.UserMovies)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configurations for Like entity
            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => e.LikeId);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Likes)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict); //No cascade delete
                entity.HasOne(e => e.UserMovie)
                      .WithMany(um => um.Likes)
                      .HasForeignKey(e => e.UserMovieId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(l => new { l.UserId, l.UserMovieId })
                      .IsUnique();
                
            });

            // Configurations for Follower entity
            modelBuilder.Entity<Follower>(entity =>
            {
                entity.HasKey(e => e.FollowingRelationshipId);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Followers)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict); //No cascade delete
                entity.HasOne(e => e.FollowerUser)
                      .WithMany(u => u.Following)
                      .HasForeignKey(e => e.FollowerUserId)
                      .OnDelete(DeleteBehavior.Restrict); //No cascade delete
                
                // Define unique constraint for Follower table
                entity.HasIndex(f => new { f.UserId, f.FollowerUserId })
                      .IsUnique();
            });

            // If using a view for movie ranking
             modelBuilder.Entity<MovieRankDTO>(entity =>
             {
                 entity.HasNoKey();
                 entity.ToView("MovieRanks"); // Specify the view name in the database
                 
             });

             // Configurations for MovieRankDTO entity
                modelBuilder.Entity<MovieRankDTO>(entity =>
                {
                    entity.HasNoKey(); // Specify that this entity has no key
                    entity.ToView("MovieRanks"); // Map to the existing view in the database

                    // Specify the column types for decimal properties
                    entity.Property(e => e.AverageRating).HasColumnType("decimal(2, 1)"); //precision and scale
         });


        } //OnModelCreating method closing curly brace
    }

