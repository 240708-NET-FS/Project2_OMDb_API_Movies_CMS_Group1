using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMDbProject.Models;

    public class UserMovie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserMovieId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string OMDBId { get; set; }

        public DateTime? WatchedOn { get; set; }

        public decimal? UserRating { get; set; }

        public string UserReview { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; } //User related to the UserMovie, based on the UserId
        
        public ICollection<Like> Likes { get; set; } //collection of Like entities related to the UserMovie, based on UserMovieId


    }

