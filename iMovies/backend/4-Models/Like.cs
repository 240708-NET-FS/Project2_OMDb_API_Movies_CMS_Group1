using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMDbProject.Models;

    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int UserMovieId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(UserMovieId))]
        public UserMovie UserMovie { get; set; }
    }
