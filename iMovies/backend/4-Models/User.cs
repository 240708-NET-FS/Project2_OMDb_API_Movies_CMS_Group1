using System;
using System.ComponentModel.DataAnnotations;

namespace OMDbProject.Models;

    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; }

         // Navigation properties
        public ICollection<UserMovie> UserMovies { get; set; }
        public ICollection<Like> Likes { get; set; } 

         // Navigation properties
        public ICollection<Follower> Followers { get; set; }
        public ICollection<Follower> Following { get; set; }
      

    }

