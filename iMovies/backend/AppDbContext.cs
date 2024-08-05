using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OMDBProject.DTO;

namespace OMDBProject.Data;

public class AppDbContext : DbContext
{
   public DbSet<Users> Users { get; set; }
    public DbSet<UserMovies> UserMovies { get; set; }
    public DbSet<Likes> Likes { get; set; }
    public DbSet<Followers> Followers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json")
                                                .Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);
        
    }
}