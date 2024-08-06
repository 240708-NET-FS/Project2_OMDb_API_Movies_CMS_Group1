using Microsoft.EntityFrameworkCore;


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
        // public DbSet<MovieRank> MovieRanks { get; set; }
        
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
            });

            // Configurations for Follower entity
            modelBuilder.Entity<Follower>(entity =>
            {
                entity.HasKey(e => e.FollowerId);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Followers)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict); //No cascade delete
                entity.HasOne(e => e.FollowerUser)
                      .WithMany(u => u.Following)
                      .HasForeignKey(e => e.FollowerUserId)
                      .OnDelete(DeleteBehavior.Restrict); //No cascade delete
            });

            // If using a view for movie ranking
            // modelBuilder.Entity<MovieRank>(entity =>
            // {
            //     entity.HasNoKey();
            //     entity.ToView("MovieRanks"); // Specify the view name in the database
            // });
        }
    }

